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
            WriteAsm();
            WriteCPPProject();
        }

        #region C++        

        private void WriteCpp()
        {
            CodeWriter writer = new CodeWriter(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + ".c"));

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

            CodeWriter mainWriter = new CodeWriter(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + "_DllMain.c"));

            mainWriter.Write(Properties.Resources.DllMain);
            mainWriter.Save();
        }
        #endregion

        #region Asm
        private void WriteAsm()
        {
            {
                CodeWriter x86Writer = new CodeWriter(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + "_x86.asm"));

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
                CodeWriter x64Writer = new CodeWriter(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + "_x64.asm"));

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

        #region Projects
        private void WriteCPPProject()
        {
            {
                CodeWriter vcxProjWriter = new CodeWriter(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + ".vcxproj"));

                string vcxCode = Encoding.UTF8.GetString(Properties.Resources.vcxprojTemplate);
                vcxCode = vcxCode.Replace("${LIB_NAME}", Path.GetFileNameWithoutExtension(Name));

                vcxProjWriter.Write(vcxCode);
                vcxProjWriter.Save();
            }

            {
                CodeWriter vcxProjWriter = new CodeWriter(Path.Combine(Directory, Path.GetFileNameWithoutExtension(Name) + ".sln"));

                string slnCode = Encoding.UTF8.GetString(Properties.Resources.solution);
                slnCode = slnCode.Replace("${LIB_NAME}", Path.GetFileNameWithoutExtension(Name));

                vcxProjWriter.Write(slnCode);
                vcxProjWriter.Save();
            }
        }
        #endregion
    }
}
