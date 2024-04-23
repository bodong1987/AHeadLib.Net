#include "MemoryPatchConfig.h"
#include <cassert>
#include <string>
#include "MiniTools.h"

template <typename TCharType>
inline bool IsTrimable(TCharType ch)
{
    return ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r';
}

inline static int CompareN(const char* first, const char* second, std::size_t n)
{
    return strncmp(first, second, n);
}

inline static bool StartWith(const std::string& str, const std::string& start)
{
    return str.size() >= start.size() && CompareN(str.c_str(), start.c_str(), start.size()) == 0;
}

template<typename Predicate>
inline static std::string& TrimLeft(std::string& str, Predicate predicate)
{
    for (SIZE_T i = 0; i < str.size(); ++i)
    {
        if (!predicate(str[i]))
        {
            if (i > 0)
            {
                str.erase(0, i);
            }

            break;
        }
    }

    return str;
}

template<typename Predicate>
inline static std::string TrimLeftCopy(const std::string& str, Predicate predicate)
{
    auto result = str;
    TrimLeft<Predicate>(result, predicate);
    return result;
}

inline static std::string& TrimLeft(std::string& str)
{
    return TrimLeft(str, IsTrimable<char>);
}

inline static std::string TrimLeftCopy(const std::string& str)
{
    auto result = str;
    TrimLeft(result);
    return result;
}

template<typename Predicate>
inline static std::string& TrimRight(std::string& str, Predicate predicate)
{
    if (str.empty())
    {
        return str;
    }

    for (auto i = str.size() - 1; i >= 0 && i < str.size(); --i)
    {
        if (!predicate(str[i]))
        {
            if (i < str.size() - 1)
            {
                str.erase(i + 1, str.size() - i - 1);
            }

            break;
        }
    }

    return str;
}

template <typename Predicate>
inline static std::string TrimRightCopy(const std::string& str, Predicate predicate)
{
    auto result = str;
    TrimRight<Predicate>(result, predicate);
    return result;
}

inline static std::string& TrimRight(std::string& str)
{
    return TrimRight(str, IsTrimable<char>);
}

inline static std::string TrimRightCopy(const std::string& str)
{
    auto result = str;
    TrimRight(result);
    return result;
}

template<typename Predicate>
inline static std::string& Trim(std::string& str, Predicate predicate)
{
    if (str.empty())
    {
        return str;
    }

    TrimLeft<Predicate>(str, predicate);
    TrimRight<Predicate>(str, predicate);
    return str;
}

template<typename Predicate>
inline static std::string TrimCopy(const std::string& str, Predicate predicate)
{
    auto result = str;
    Trim<Predicate>(result, predicate);
    return result;
}

inline static std::string& Trim(std::string& str)
{
    TrimLeft(str);
    TrimRight(str);
    return str;
}

inline static std::string TrimCopy(const std::string& str)
{
    auto result = str;
    Trim(result);
    return result;
}

template <typename Predicate>
inline static typename std::string::size_type Find(const std::string& str, Predicate predicate, typename std::string::size_type startPos = 0)
{
    for (auto i = startPos; i < str.size(); ++i)
    {
        if (predicate(str[i]))
        {
            return i;
        }
    }

    return std::string::npos;
}

template <typename TSequenceType, typename Predicate>
inline static TSequenceType& Split(TSequenceType& sequence, const std::string& str, Predicate predicate)
{
    typedef typename std::string::size_type size_type;

    size_type startPos = 0;
    size_type pos = std::string::npos;
    while ((pos = Find<Predicate>(str, predicate, startPos)) != std::string::npos)
    {
        if (pos == startPos)
        {
            startPos = pos + 1;
            continue;
        }

        std::string token = str.substr(startPos, pos - startPos);
        sequence.emplace_back(std::move(token));
        startPos = pos + 1;
    }

    if (startPos < str.size())
    {
        sequence.emplace_back(str.substr(startPos));
    }

    return sequence;
}


MemoryPatchConfigParser::MemoryPatchConfigParser(const char* text)
{
    assert(text != nullptr);

    std::vector<std::string> lines;
    Split(lines, text, [](char ch)
        {
            return ch == '\n';
        });

    MemoryPatchConfig config;
    
    for (auto& str : lines)
    {
        Trim(str);

        if (StartWith(str, ";"))
        {
            continue;
        }

        if (StartWith(str, "segment"))
        {
            if (config.IsValid())
            {
                Configurations.emplace_back(std::move(config));
            }

            config.SegmentName = GetName(str);
        }
        else if (StartWith(str, "signature"))
        {
            config.Signature = GetBytes(str);
        }
        else if (StartWith(str, "newBytes"))
        {
            config.NewBytes = GetBytes(str);
        }
    }

    if (config.IsValid())
    {
        Configurations.emplace_back(std::move(config));
    }
}

std::string MemoryPatchConfigParser::GetName(const std::string& text)
{
    auto pos = text.find_first_of(':', 0);

    if (pos == std::string::npos)
    {
        return std::string();
    }

    std::string name = text.substr(pos+1);
    
    return TrimCopy(name);
}

BYTE ConvertHexStringToByte(const std::string& str)
{
    if (str.size() > 2 && str[0] == '0' && toupper(str[1]) == 'X')
    {
        return (BYTE)strtoul(str.c_str()+2, nullptr, 16);
    }

    return (BYTE)strtoul(str.c_str(), nullptr, 16);
}

std::vector<BYTE> MemoryPatchConfigParser::GetBytes(const std::string& text)
{
    auto pos = text.find_first_of(':', 0);

    if (pos == std::string::npos)
    {
        return std::vector<BYTE>();
    }

    std::string values = TrimCopy(text.substr(pos + 1));

    std::vector<std::string> hexValues;
    Split(hexValues, values, [](char ch){ return ch == ' ';});

    std::vector<BYTE> bytes;

    for (auto& hexText : hexValues)
    {
        BYTE v = ConvertHexStringToByte(TrimCopy(hexText));

        bytes.push_back(v);
    }

    return bytes;
}

void PatchMemoryWithConfig(HMODULE module, const std::vector<MemoryPatchConfig>& configs)
{
    for (auto& config : configs)
    {
        if (config.IsValid())
        {
            PatchMemory(module, config.SegmentName.c_str(), (const void*)config.Signature.data(), (const void*)config.NewBytes.data(), (int)config.NewBytes.size());
        }
    }
}
