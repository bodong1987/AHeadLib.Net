using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
// ReSharper disable MemberCanBePrivate.Global

namespace AHeadLib.Net;

public class VsProjectGenerator
{
    public readonly string Directory;
    public readonly string Name;
    public readonly IEnumerable<string> Methods;

    // ReSharper disable once ConvertToPrimaryConstructor
    public VsProjectGenerator(string directory, string name, IEnumerable<string> methods)
    {
        Directory = directory;
        Name = name;
        Methods = methods;
    }

    public void Write()
    {
        WriteCpp();
        WriteCppTemplates();
        WriteCommonFiles();
        WriteAsm();
        WriteCppProject();
    }

    #region C++ Libs
    private void WriteCppTemplates()
    {
        WriteCommonUtilFile(Path.Combine(Directory, "Utils/MiniTools.h"), Properties.Resources.MiniTools_h);
        WriteCommonUtilFile(Path.Combine(Directory, "Utils/MiniTools.cpp"), Properties.Resources.MiniTools_cpp);
        WriteCommonUtilFile(Path.Combine(Directory, "Utils/MemoryPatchConfig.h"), Properties.Resources.MemoryPatchConfig_h);
        WriteCommonUtilFile(Path.Combine(Directory, "Utils/MemoryPatchConfig.cpp"), Properties.Resources.MemoryPatchConfig_cpp);

        WriteCommonUtilFile(Path.Combine(Directory, "GeneratedFiles/BuiltinImplementations.cpp"), Properties.Resources.BuiltinImplementations);
        WriteCommonUtilFile(Path.Combine(Directory, "UserFiles/UserImplementations.cpp"), Properties.Resources.UserImplementations);
    }
    #endregion

    #region C++        

    private void WriteCpp()
    {
        var writer = new CodeWriter(Path.Combine(Directory, "GeneratedFiles", Path.GetFileNameWithoutExtension(Name) + ".cpp"));

        var exportedPointers = string.Join(Environment.NewLine, Methods.Select(x => $"extern \"C\" void* {x}Ptr = nullptr;"));
            
        // ReSharper disable once StringLiteralTypo
        var exportedFunctions = string.Join(Environment.NewLine, Methods.Select(x => $"extern \"C\" void WINAPI ASM_{x}();"));
        var exportedLinker = string.Join(Environment.NewLine, Methods.Select(x => $"#if AHEAD_LIB_DOT_NET_X64_BUILD\n" +
            $"#pragma comment(linker, \"/EXPORT:{x}=ASM_{x}\")\n" +
            $"#else\n" +
            $"#pragma comment(linker, \"/EXPORT:{x}=_ASM_{x}@8\")\n" +
            $"#endif"));
            
        var bindPointers = string.Join(Environment.NewLine + "    ", Methods.Select(x => $"AHEAD_LIB_DOT_NET_BIND_FUNCTION({x});"));

        var cppCode = Properties.Resources.CppHelper;
        cppCode = cppCode.Replace("// ${EXPORTED_POINTERS}", exportedPointers);
        cppCode = cppCode.Replace("// ${EXPORTED_FUNCTIONS}", exportedFunctions);
        cppCode = cppCode.Replace("${LIBRARY_NAME}", Name);
        cppCode = cppCode.Replace("// ${BIND_POINTERS}", bindPointers);
        cppCode = cppCode.Replace("// ${EXPORTED_LINKERS}", exportedLinker);

        writer.Write(cppCode);
        writer.WriteNewLine();
        writer.Save();

        var mainWriter = new CodeWriter(Path.Combine(Directory, "GeneratedFiles", Path.GetFileNameWithoutExtension(Name) + "_DllMain.cpp"));

        mainWriter.Write(Properties.Resources.DllMain);
        mainWriter.Save();
    }
    #endregion

    #region Asm
    private void WriteAsm()
    {
        {
            var x86Writer = new CodeWriter(Path.Combine(Directory, "GeneratedFiles", Path.GetFileNameWithoutExtension(Name) + "_x86.asm"));

            var builder = new StringBuilder();
            // ReSharper disable once StringLiteralTypo
            var externDefs = string.Join(Environment.NewLine + "    ", Methods.Select(x => $"EXTERNDEF _{x}Ptr:DWORD"));
            var functionCodes = string.Join(Environment.NewLine, Methods.Select(x =>
            {
                builder.Clear();
                builder.AppendLine($"_ASM_{x}@8 PROC");
                builder.AppendLine($"    jmp DWORD PTR [_{x}Ptr]");
                // ReSharper disable once StringLiteralTypo
                builder.AppendLine($"_ASM_{x}@8 ENDP");

                return builder.ToString();
            }));

            var asmCode = Properties.Resources.Asm_x86;
            // ReSharper disable once StringLiteralTypo
            asmCode = asmCode.Replace("${EXTERNDEF_POINTERS}", externDefs);
            asmCode = asmCode.Replace("${FUNCTIONS}", functionCodes);

            x86Writer.Write(asmCode);
            x86Writer.Save();
        }

        {
            var x64Writer = new CodeWriter(Path.Combine(Directory, "GeneratedFiles", Path.GetFileNameWithoutExtension(Name) + "_x64.asm"));

            var builder = new StringBuilder();
            // ReSharper disable once StringLiteralTypo
            var externDefs = string.Join(Environment.NewLine + "    ", Methods.Select(x => $"EXTERNDEF {x}Ptr:QWORD"));
            var functionCodes = string.Join(Environment.NewLine, Methods.Select(x =>
            {
                builder.Clear();
                builder.AppendLine($"ASM_{x} PROC");
                builder.AppendLine($"    jmp [{x}Ptr]");
                // ReSharper disable once StringLiteralTypo
                builder.AppendLine($"ASM_{x} ENDP");

                return builder.ToString();
            }));

            var asmCode = Properties.Resources.Asm_x64;
            // ReSharper disable once StringLiteralTypo
            asmCode = asmCode.Replace("${EXTERNDEF_POINTERS}", externDefs);
            asmCode = asmCode.Replace("${FUNCTIONS}", functionCodes);

            x64Writer.Write(asmCode);
            x64Writer.Save();
        }
    }
    #endregion

    #region Common Files
    private void WriteBinaryCodeFile(string name, byte[] bytes)
    {
        var writer = new CodeWriter(name);

        var codes = Encoding.UTF8.GetString(bytes);
        codes = codes.Replace("${LIB_NAME}", Path.GetFileNameWithoutExtension(Name));

        writer.Write(codes);
        writer.Save();
    }

    private void WriteTextCodeFile(string name, string text)
    {
        var writer = new CodeWriter(name);

        text = text.Replace("${LIB_NAME}", Path.GetFileNameWithoutExtension(Name));

        writer.Write(text);
        writer.Save();
    }

    private static void WriteCommonUtilFile(string name, string text)
    {
        var writer = new CodeWriter(name);

        writer.Write(text);
        writer.Save();
    }

    private void WriteCommonFiles()
    {
        WriteTextCodeFile(Path.Combine(Directory, "Resources/resource.h"), Properties.Resources.resource);
        WriteTextCodeFile(Path.Combine(Directory, "Resources", Path.GetFileNameWithoutExtension(Name) + ".rc"), Properties.Resources.resource_rc);
        WriteTextCodeFile(Path.Combine(Directory, "Resources", Path.GetFileNameWithoutExtension(Name) + "_patch.txt"), Properties.Resources.patch);
    }
    #endregion

    #region Projects
    private void WriteCppProject()
    {
        WriteBinaryCodeFile(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + ".vcxproj"), Properties.Resources.vcxprojTemplate);
        WriteBinaryCodeFile(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + ".vcxproj.filters"), Properties.Resources.vcxprojTemplate_filters);
        WriteBinaryCodeFile(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + ".sln"), Properties.Resources.solution);
    }
    #endregion
}