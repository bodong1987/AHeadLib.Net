using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AHeadLib.Net
{
    public class VSProjectGenerator
    {
        public readonly string Directory;
        public readonly string Name;
        public readonly IEnumerable<string> Methods;

        public VSProjectGenerator(string directory, string name, IEnumerable<string> methods)
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
            WriteCPPProject();
        }

        #region C++ Libs
        private void WriteCppTemplates()
        {
            WriteComonUtilFile(Path.Combine(Directory, "Utils/MiniTools.h"), Properties.Resources.MiniTools_h);
            WriteComonUtilFile(Path.Combine(Directory, "Utils/MiniTools.cpp"), Properties.Resources.MiniTools_cpp);
            WriteComonUtilFile(Path.Combine(Directory, "Utils/MemoryPatchConfig.h"), Properties.Resources.MemoryPatchConfig_h);
            WriteComonUtilFile(Path.Combine(Directory, "Utils/MemoryPatchConfig.cpp"), Properties.Resources.MemoryPatchConfig_cpp);

            WriteComonUtilFile(Path.Combine(Directory, "GeneratedFiles/BuiltinImplementations.cpp"), Properties.Resources.BuiltinImplementations);
            WriteComonUtilFile(Path.Combine(Directory, "UserFiles/UserImplementations.cpp"), Properties.Resources.UserImplementations);
        }
        #endregion

        #region C++        

        private void WriteCpp()
        {
            CodeWriter writer = new CodeWriter(Path.Combine(Directory, "GeneratedFiles", Path.GetFileNameWithoutExtension(Name) + ".c"));

            string exportedPointers = string.Join(Environment.NewLine, Methods.Select(x => $"void* {x}Ptr = NULL;"));
            string exportedFunctions = string.Join(Environment.NewLine, Methods.Select(x => $"extern void WINAPI ASM_{x}();"));
            string exportedLinker = string.Join(Environment.NewLine, Methods.Select(x =>
            {
                return 
                $"#if __X64_BUILD__\n" +
                $"#pragma comment(linker, \"/EXPORT:{x}=ASM_{x}\")\n" +
                $"#else\n" +
                $"#pragma comment(linker, \"/EXPORT:{x}=_ASM_{x}@8\")\n" +
                $"#endif";
            }));
            string bindPointers = string.Join(Environment.NewLine + "    ", Methods.Select(x => $"{x}Ptr = __CheckedGetFunction(module, \"{x}\");"));

            string cppCode = Properties.Resources.CppHelper;
            cppCode = cppCode.Replace("// ${EXPORTED_POINTERS}", exportedPointers);
            cppCode = cppCode.Replace("// ${EXPORTED_FUNCTIONS}", exportedFunctions);
            cppCode = cppCode.Replace("${LIBRARY_NAME}", Name);
            cppCode = cppCode.Replace("// ${BIND_POINTERS}", bindPointers);
            cppCode = cppCode.Replace("// ${EXPORTED_LINKERS}", exportedLinker);

            writer.Write(cppCode);
            writer.WriteNewLine();
            writer.Save();

            CodeWriter mainWriter = new CodeWriter(Path.Combine(Directory, "GeneratedFiles", Path.GetFileNameWithoutExtension(Name) + "_DllMain.c"));

            mainWriter.Write(Properties.Resources.DllMain);
            mainWriter.Save();
        }
        #endregion

        #region Asm
        private void WriteAsm()
        {
            {
                CodeWriter x86Writer = new CodeWriter(Path.Combine(Directory, "GeneratedFiles", Path.GetFileNameWithoutExtension(Name) + "_x86.asm"));

                StringBuilder builder = new StringBuilder();
                string externDefs = string.Join(Environment.NewLine + "    ", Methods.Select(x => $"EXTERNDEF _{x}Ptr:DWORD"));
                string funcs = string.Join(Environment.NewLine, Methods.Select(x =>
                {
                    builder.Clear();
                    builder.AppendLine($"_ASM_{x}@8 PROC");
                    builder.AppendLine($"    jmp DWORD PTR [_{x}Ptr]");
                    builder.AppendLine($"_ASM_{x}@8 ENDP");

                    return builder.ToString();
                }));

                string asmCode = Properties.Resources.Asm_x86;
                asmCode = asmCode.Replace("${EXTERNDEF_POINTERS}", externDefs);
                asmCode = asmCode.Replace("${FUNCTIONS}", funcs);

                x86Writer.Write(asmCode);
                x86Writer.Save();
            }

            {
                CodeWriter x64Writer = new CodeWriter(Path.Combine(Directory, "GeneratedFiles", Path.GetFileNameWithoutExtension(Name) + "_x64.asm"));

                StringBuilder builder = new StringBuilder();
                string externDefs = string.Join(Environment.NewLine + "    ", Methods.Select(x => $"EXTERNDEF {x}Ptr:QWORD"));
                string funcs = string.Join(Environment.NewLine, Methods.Select(x =>
                {
                    builder.Clear();
                    builder.AppendLine($"ASM_{x} PROC");
                    builder.AppendLine($"    jmp [{x}Ptr]");
                    builder.AppendLine($"ASM_{x} ENDP");

                    return builder.ToString();
                }));

                string asmCode = Properties.Resources.Asm_x64;
                asmCode = asmCode.Replace("${EXTERNDEF_POINTERS}", externDefs);
                asmCode = asmCode.Replace("${FUNCTIONS}", funcs);

                x64Writer.Write(asmCode);
                x64Writer.Save();
            }
        }
        #endregion

        #region Common Files
        private void WriteBinaryCodeFile(string name, byte[] bytes)
        {
            CodeWriter wirter = new CodeWriter(name);

            string codes = Encoding.UTF8.GetString(bytes);
            codes = codes.Replace("${LIB_NAME}", Path.GetFileNameWithoutExtension(Name));

            wirter.Write(codes);
            wirter.Save();
        }

        private void WriteTextCodeFile(string name, string text)
        {
            CodeWriter writer = new CodeWriter(name);

            text = text.Replace("${LIB_NAME}", Path.GetFileNameWithoutExtension(Name));

            writer.Write(text);
            writer.Save();
        }

        private void WriteComonUtilFile(string name, string text)
        {
            CodeWriter writer = new CodeWriter(name);

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
        private void WriteCPPProject()
        {
            WriteBinaryCodeFile(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + ".vcxproj"), Properties.Resources.vcxprojTemplate);
            WriteBinaryCodeFile(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + ".vcxproj.filters"), Properties.Resources.vcxprojTemplate_filters);
            WriteBinaryCodeFile(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + ".sln"), Properties.Resources.solution);
        }
        #endregion
    }
}
