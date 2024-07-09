using System.Linq;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

namespace AHeadLib.Net;

// Resharper disable ALL
// reference : https://gist.github.com/reigningshells/d4d9d97a59a2aea8890e75c203230e92
public class DllExportInfo
{
    // Can't use sizeof for IMAGE_SECTION_HEADER because of unmanaged type
    public const int SizeOfImageSectionHeader = 40;

    #region Imports
    [System.Flags]
    enum LoadLibraryFlags : uint
    {
        None = 0,
        DontResolveDllReferences = 0x00000001,
        LoadIgnoreCodeAuthzLevel = 0x00000010,
        LoadLibraryAsDatafile = 0x00000002,
        LoadLibraryAsDatafileExclusive = 0x00000040,
        LoadLibraryAsImageResource = 0x00000020,
        LoadLibrarySearchApplicationDir = 0x00000200,
        LoadLibrarySearchDefaultDirs = 0x00001000,
        LoadLibrarySearchDllLoadDir = 0x00000100,
        LoadLibrarySearchSystem32 = 0x00000800,
        LoadLibrarySearchUserDirs = 0x00000400,
        LoadWithAlteredSearchPath = 0x00000008
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public unsafe struct ImageDosHeader
    {
        public UInt16 e_magic;              // Magic number
        public UInt16 e_cblp;               // Bytes on last page of file
        public UInt16 e_cp;                 // Pages in file
        public UInt16 e_crlc;               // Relocations
        public UInt16 e_cparhdr;            // Size of header in paragraphs
        public UInt16 e_minalloc;           // Minimum extra paragraphs needed
        public UInt16 e_maxalloc;           // Maximum extra paragraphs needed
        public UInt16 e_ss;                 // Initial (relative) SS value
        public UInt16 e_sp;                 // Initial SP value
        public UInt16 e_csum;               // Checksum
        public UInt16 e_ip;                 // Initial IP value
        public UInt16 e_cs;                 // Initial (relative) CS value
        public UInt16 e_lfarlc;             // File address of relocation table
        public UInt16 e_ovno;               // Overlay number
        public UInt16 e_res_0;              // Reserved words
        public UInt16 e_res_1;              // Reserved words
        public UInt16 e_res_2;              // Reserved words
        public UInt16 e_res_3;              // Reserved words
        public UInt16 e_oemid;              // OEM identifier (for e_oeminfo)
        public UInt16 e_oeminfo;            // OEM information; e_oemid specific
        public UInt16 e_res2_0;             // Reserved words
        public UInt16 e_res2_1;             // Reserved words
        public UInt16 e_res2_2;             // Reserved words
        public UInt16 e_res2_3;             // Reserved words
        public UInt16 e_res2_4;             // Reserved words
        public UInt16 e_res2_5;             // Reserved words
        public UInt16 e_res2_6;             // Reserved words
        public UInt16 e_res2_7;             // Reserved words
        public UInt16 e_res2_8;             // Reserved words
        public UInt16 e_res2_9;             // Reserved words
        public Int32 e_lfanew;             // File address of new exe header
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public unsafe struct ImageDataDirectory
    {
        public UInt32 VirtualAddress;
        public UInt32 Size;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public unsafe struct ImageFileHeader
    {
        public UInt16 Machine;
        public UInt16 NumberOfSections;
        public UInt32 TimeDateStamp;
        public UInt32 PointerToSymbolTable;
        public UInt32 NumberOfSymbols;
        public UInt16 SizeOfOptionalHeader;
        public UInt16 Characteristics;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageOptionalHeader64
    {
        public UInt16 Magic;
        public byte MajorLinkerVersion;
        public byte MinorLinkerVersion;
        public uint SizeOfCode;
        public uint SizeOfInitializedData;
        public uint SizeOfUninitializedData;
        public uint AddressOfEntryPoint;
        public uint BaseOfCode;
        public ulong ImageBaseLong;
        public uint SectionAlignment;
        public uint FileAlignment;
        public ushort MajorOperatingSystemVersion;
        public ushort MinorOperatingSystemVersion;
        public ushort MajorImageVersion;
        public ushort MinorImageVersion;
        public ushort MajorSubsystemVersion;
        public ushort MinorSubsystemVersion;
        public uint Win32VersionValue;
        public uint SizeOfImage;
        public uint SizeOfHeaders;
        public uint CheckSum;
        public UInt16 Subsystem;
        public UInt16 DllCharacteristics;
        public ulong SizeOfStackReserve;
        public ulong SizeOfStackCommit;
        public ulong SizeOfHeapReserve;
        public ulong SizeOfHeapCommit;
        public uint LoaderFlags;
        public uint NumberOfRvaAndSizes;
        public ImageDataDirectory ExportTable;
        public ImageDataDirectory ImportTable;
        public ImageDataDirectory ResourceTable;
        public ImageDataDirectory ExceptionTable;
        public ImageDataDirectory CertificateTable;
        public ImageDataDirectory BaseRelocationTable;
        public ImageDataDirectory Debug;
        public ImageDataDirectory Architecture;
        public ImageDataDirectory GlobalPtr;
        public ImageDataDirectory TLSTable;
        public ImageDataDirectory LoadConfigTable;
        public ImageDataDirectory BoundImport;
        public ImageDataDirectory IAT;
        public ImageDataDirectory DelayImportDescriptor;
        public ImageDataDirectory CLRRuntimeHeader;
        public ImageDataDirectory Reserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ImageOptionalHeader32
    {
        public UInt16 Magic;
        public Byte MajorLinkerVersion;
        public Byte MinorLinkerVersion;
        public UInt32 SizeOfCode;
        public UInt32 SizeOfInitializedData;
        public UInt32 SizeOfUninitializedData;
        public UInt32 AddressOfEntryPoint;
        public UInt32 BaseOfCode;
        public UInt32 BaseOfData;
        public UInt32 ImageBase;
        public UInt32 SectionAlignment;
        public UInt32 FileAlignment;
        public UInt16 MajorOperatingSystemVersion;
        public UInt16 MinorOperatingSystemVersion;
        public UInt16 MajorImageVersion;
        public UInt16 MinorImageVersion;
        public UInt16 MajorSubsystemVersion;
        public UInt16 MinorSubsystemVersion;
        public UInt32 Win32VersionValue;
        public UInt32 SizeOfImage;
        public UInt32 SizeOfHeaders;
        public UInt32 CheckSum;
        public UInt16 Subsystem;
        public UInt16 DllCharacteristics;
        public UInt32 SizeOfStackReserve;
        public UInt32 SizeOfStackCommit;
        public UInt32 SizeOfHeapReserve;
        public UInt32 SizeOfHeapCommit;
        public UInt32 LoaderFlags;
        public UInt32 NumberOfRvaAndSizes;

        public ImageDataDirectory ExportTable;
        public ImageDataDirectory ImportTable;
        public ImageDataDirectory ResourceTable;
        public ImageDataDirectory ExceptionTable;
        public ImageDataDirectory CertificateTable;
        public ImageDataDirectory BaseRelocationTable;
        public ImageDataDirectory Debug;
        public ImageDataDirectory Architecture;
        public ImageDataDirectory GlobalPtr;
        public ImageDataDirectory TLSTable;
        public ImageDataDirectory LoadConfigTable;
        public ImageDataDirectory BoundImport;
        public ImageDataDirectory IAT;
        public ImageDataDirectory DelayImportDescriptor;
        public ImageDataDirectory CLRRuntimeHeader;
        public ImageDataDirectory Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageNtHeaders32
    {
        public UInt32 Signature;
        public ImageFileHeader FileHeader;
        public ImageOptionalHeader32 OptionalHeader;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageNtHeaders64
    {
        public UInt32 Signature;
        public ImageFileHeader FileHeader;
        public ImageOptionalHeader64 OptionalHeader;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageExportDirectory
    {
        public UInt32 Characteristics;
        public UInt32 TimeDateStamp;
        public UInt16 MajorVersion;
        public UInt16 MinorVersion;
        public UInt32 Name;
        public UInt32 Base;
        public UInt32 NumberOfFunctions;
        public UInt32 NumberOfNames;
        public UInt32 AddressOfFunctions; // RVA from base of image
        public UInt32 AddressOfNames; // RVA from base of image
        public UInt32 AddressOfNameOrdinals; // RVA from base of image
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ImageSectionHeader
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] Name;
        [FieldOffset(8)]
        public UInt32 VirtualSize;
        [FieldOffset(12)]
        public UInt32 VirtualAddress;
        [FieldOffset(16)]
        public UInt32 SizeOfRawData;
        [FieldOffset(20)]
        public UInt32 PointerToRawData;
        [FieldOffset(24)]
        public UInt32 PointerToRelocations;
        [FieldOffset(28)]
        public UInt32 PointerToLinenumbers;
        [FieldOffset(32)]
        public UInt16 NumberOfRelocations;
        [FieldOffset(34)]
        public UInt16 NumberOfLinenumbers;
        [FieldOffset(36)]
        uint Characteristics;

        public string Section
        {
            get { return new string(Name); }
        }
    }
    #endregion

        
    public static T CastBytesToType<T>(byte[] bytes)
    {
        GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        T theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        handle.Free();

        return theStructure;
    }

    public static unsafe IEnumerable<string> ReadFromFile(string path)
    {
        // Read bytes of DLL
        byte[] sourceFileByteBuffer = System.IO.File.ReadAllBytes(path);

        // Get IMAGE_DOS_HEADER because we need e_lfanew for offset to IMAGE_NT_HEADER
        ImageDosHeader imageDosHeader = CastBytesToType<ImageDosHeader>(sourceFileByteBuffer.Skip(0).Take(sizeof(ImageDosHeader)).ToArray());

        // Get IMAGE_FILE_HEADER to check if the DLL is 32 or 64 bit
        ImageFileHeader imageFileHeader = CastBytesToType<ImageFileHeader>(sourceFileByteBuffer.Skip(imageDosHeader.e_lfanew + 4).Take(sizeof(ImageFileHeader)).ToArray());

        int sectionOffset = 0;
        int exportOffset = 0;

        if (imageFileHeader.Machine == 0x8664) // 64bit 0x8664
        {
            // Get IMAGE_NT_HEADER because we need it for address of export table
            ImageNtHeaders64 imageNtHeader = CastBytesToType<ImageNtHeaders64>(sourceFileByteBuffer.Skip(imageDosHeader.e_lfanew).Take(sizeof(ImageNtHeaders64)).ToArray());

            // Loop through sections to find where export table resides for section offset (VA - PTRToRawData) use to calculate appropriate file offset

            int sectionHeadersOffset = imageDosHeader.e_lfanew + sizeof(ImageNtHeaders64);

            for (int i = 0; i < imageNtHeader.FileHeader.NumberOfSections; i++)
            {
                // Parse IMAGE_SECTION_HEADER
                ImageSectionHeader imageSectionHeader = CastBytesToType<ImageSectionHeader>(sourceFileByteBuffer.Skip(sectionHeadersOffset).Take(SizeOfImageSectionHeader).ToArray());
                if (imageNtHeader.OptionalHeader.ExportTable.VirtualAddress > imageSectionHeader.VirtualAddress && imageNtHeader.OptionalHeader.ExportTable.VirtualAddress < (imageSectionHeader.VirtualAddress + imageSectionHeader.VirtualSize))
                {
                    sectionOffset = (int)((imageSectionHeader.VirtualAddress - imageSectionHeader.PointerToRawData));
                    break;
                }
                sectionHeadersOffset += SizeOfImageSectionHeader;
            }

            // Now that we have offset to turn RVA into a file offset, can get IMAGE_EXPORT_DIRECTORY and start looping through arrays to get exports

            exportOffset = (int)(imageNtHeader.OptionalHeader.ExportTable.VirtualAddress - sectionOffset);
        }
        else // 32bit
        {
            ImageNtHeaders32 imageNtHeader = CastBytesToType<ImageNtHeaders32>(sourceFileByteBuffer.Skip(imageDosHeader.e_lfanew).Take(sizeof(ImageNtHeaders32)).ToArray());

            int sectionHeadersOffset = imageDosHeader.e_lfanew + sizeof(ImageNtHeaders32);

            for (int i = 0; i < imageNtHeader.FileHeader.NumberOfSections; i++)
            {
                // Parse IMAGE_SECTION_HEADER
                ImageSectionHeader imageSectionHeader = CastBytesToType<ImageSectionHeader>(sourceFileByteBuffer.Skip(sectionHeadersOffset).Take(SizeOfImageSectionHeader).ToArray());
                if (imageNtHeader.OptionalHeader.ExportTable.VirtualAddress > imageSectionHeader.VirtualAddress && imageNtHeader.OptionalHeader.ExportTable.VirtualAddress < (imageSectionHeader.VirtualAddress + imageSectionHeader.VirtualSize))
                {
                    sectionOffset = (int)((imageSectionHeader.VirtualAddress - imageSectionHeader.PointerToRawData));
                    break;
                }
                sectionHeadersOffset += SizeOfImageSectionHeader;
            }

            // Now that we have offset to turn RVA into a file offset, can get IMAGE_EXPORT_DIRECTORY and start looping through arrays to get exports

            exportOffset = (int)(imageNtHeader.OptionalHeader.ExportTable.VirtualAddress - sectionOffset);
        }

        ImageExportDirectory exportTable = CastBytesToType<ImageExportDirectory>(sourceFileByteBuffer.Skip(exportOffset).Take(sizeof(ImageExportDirectory)).ToArray());

        // Loop through function names

        int addressOfNamesOffset = (int)(exportTable.AddressOfNames - sectionOffset);
        string[] functionNames = new string[exportTable.NumberOfNames];

        for (int i = 0; i < exportTable.NumberOfNames; i++)
        {
            // Get file offset for function name
            int nameOffset = BitConverter.ToInt32(sourceFileByteBuffer.Skip(addressOfNamesOffset).Take(4).ToArray(), 0) - sectionOffset;

            // Get length of the name
            byte b = 0x01;
            int nameLength = 0;
            while (b != 0x00)
            {
                b = sourceFileByteBuffer[nameOffset + nameLength];
                nameLength++;
            }

            // Get function name string
            functionNames[i] = System.Text.Encoding.ASCII.GetString(sourceFileByteBuffer.Skip(nameOffset).Take(nameLength-1).ToArray()).Trim();
            addressOfNamesOffset += 4;
        }

        return functionNames;
    }
}