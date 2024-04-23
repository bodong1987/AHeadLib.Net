#pragma once

#ifndef  __cplusplus
#error "this header i created for C++ only."
#endif

#include <windows.h>

#include <vector>
#include <string>

struct MemoryPatchConfig
{
    std::string SegmentName;
    std::vector<BYTE> Signature;
    std::vector<BYTE> NewBytes;

    void Reset()
    {
        SegmentName.clear();
        Signature.clear();
        NewBytes.clear();
    }

    bool IsValid() const
    {
        return !SegmentName.empty() && !Signature.empty() && NewBytes.size() == Signature.size();
    }
};

class MemoryPatchConfigParser
{
public:
    MemoryPatchConfigParser(const char* text);

    const std::vector<MemoryPatchConfig>& GetConfigurations() const{ return Configurations; }

private:
    std::string GetName(const std::string& text);
    std::vector<BYTE> GetBytes(const std::string& text);
private:
    std::vector<MemoryPatchConfig>  Configurations;
};

void PatchMemoryWithConfig(HMODULE module, const std::vector<MemoryPatchConfig>& configs);



