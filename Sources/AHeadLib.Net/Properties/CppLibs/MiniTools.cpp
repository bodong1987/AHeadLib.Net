#include "MiniTools.h"
#include <algorithm>
#include <cassert>

G_BEGIN_DECLS

bool ReplaceMemory(void* dest, const void* source, int length)
{
    void* TargetAddress = dest;

    DWORD oldProtect;
    if (!VirtualProtect(TargetAddress, length, PAGE_EXECUTE_READWRITE, &oldProtect))
    {
        MessageBox(0, TEXT("Failed to obtain write permission for target address"), TEXT("Error"), 0);

        return false;
    }

    memcpy(TargetAddress, source, length);

    if (!VirtualProtect(TargetAddress, length, oldProtect, &oldProtect))
    {
        // error
        MessageBox(0, TEXT("Failed write code."), TEXT("Error"), 0);
        return false;
    }

    // clear
    FlushInstructionCache(GetCurrentProcess(), TargetAddress, length);

    return true;
}

bool FindModuleSection(HMODULE module, const char* segmentName, void** outSectionStart, LONGLONG* outSize)
{
    *outSectionStart = nullptr;
    *outSize = 0;

    MODULEINFO module_info;
    GetModuleInformation(GetCurrentProcess(), module, &module_info, sizeof(module_info));
    void* Address = module_info.lpBaseOfDll;

    // get moudle pe
    PIMAGE_DOS_HEADER dosHeader = reinterpret_cast<PIMAGE_DOS_HEADER>(Address);
    PIMAGE_NT_HEADERS ntHeaders = reinterpret_cast<PIMAGE_NT_HEADERS>((BYTE*)Address + dosHeader->e_lfanew);

    PIMAGE_SECTION_HEADER sectionHeader = IMAGE_FIRST_SECTION(ntHeaders);
    for (int i = 0; i < ntHeaders->FileHeader.NumberOfSections; i++, sectionHeader++)
    {
        LONGLONG sectionBase = (LONGLONG)Address + sectionHeader->VirtualAddress;
        LONGLONG sectionSize = sectionHeader->Misc.VirtualSize;

        if (strcmp((char*)sectionHeader->Name, segmentName) == 0)
        {
             *outSectionStart = (void*)sectionBase; 
             *outSize = sectionSize;

             return true;
        }        
    }

    return false;
}

void* SearchInSection(HMODULE module, const char* segmentName, const void* signature, int length)
{
    void* SectionStart = nullptr;
    LONGLONG SectionSize = 0;

    if (!FindModuleSection(module, segmentName, &SectionStart, &SectionSize))
    {
        return nullptr;
    }

    return SearchInMemory(SectionStart, (BYTE*)SectionStart + SectionSize, signature, length);
}

void* SearchInMemory(const void* startPos, const void* endPos, const void* signature, int length)
{
    const BYTE* position = std::search((BYTE*)(startPos), (BYTE*)endPos, (BYTE*)signature, (BYTE*)signature + length);

    if (position == nullptr || position == (BYTE*)endPos)
    {
        return nullptr;
    }

    return (void*)position;
}

bool PatchMemory(HMODULE module, const char* segmentName, const void* signature, const void* newBytes, int length)
{
    void* pos = SearchInSection(module, segmentName, signature, length);

    assert(pos != nullptr);

    if (pos == nullptr)
    {
        return false;
    }

    ReplaceMemory(pos, newBytes, length);

    return true;
}

bool PatchMultipleMemories(HMODULE module, const char* segmentName, const void** signaturePtr, const void** newBytesPtr, int* lengthPtr, int count)
{
    void* SectionStart = nullptr;
    LONGLONG SectionSize = 0;

    if (!FindModuleSection(module, segmentName, &SectionStart, &SectionSize))
    {
        return false;
    }

    for (int i = 0; i < count; ++i)
    {
        const void* signature = signaturePtr[i];
        const void* newBytes = newBytesPtr[i];
        const int length = lengthPtr[i];

        void* position = SearchInMemory(SectionStart, (BYTE*)SectionStart + SectionSize, signature, length);

        if (!ReplaceMemory(position, newBytes, length))
        {
            return false;
        }
    }

    return true;
}

G_END_DECLS