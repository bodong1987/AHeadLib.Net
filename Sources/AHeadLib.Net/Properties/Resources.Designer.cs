﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AHeadLib.Net.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AHeadLib.Net.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ; generated by tools
        ///; AHeadLib.Net
        ///; https://github.com/bodong1987/AHeadLib.Net
        ///; Powered by bodong
        ///
        ///.DATA
        ///    ${EXTERNDEF_POINTERS}
        ///
        ///.CODE
        ///
        ///${FUNCTIONS}
        ///
        ///END
        ///.
        /// </summary>
        internal static string Asm_x64 {
            get {
                return ResourceManager.GetString("Asm_x64", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ; generated by tools
        ///; AHeadLib.Net
        ///; https://github.com/bodong1987/AHeadLib.Net
        ///; Powered by bodong
        ///
        ///.MODEL FLAT
        ///
        ///.DATA
        ///    ${EXTERNDEF_POINTERS}
        ///
        ///.CODE
        ///
        ///${FUNCTIONS}
        ///
        ///END
        ///.
        /// </summary>
        internal static string Asm_x86 {
            get {
                return ResourceManager.GetString("Asm_x86", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // generated by tools
        /// // AHeadLib.Net
        /// // https://github.com/bodong1987/AHeadLib.Net
        /// // Powered by bodong
        ///
        ///#include &quot;MemoryPatchConfig.h&quot;
        ///#include &quot;resource.h&quot;
        ///#include &lt;cassert&gt;
        ///#include &lt;string&gt;
        ///
        ///extern &quot;C&quot;
        ///{
        ///    static void __ApplyResourceBasedPatches()
        ///    {
        ///        auto configs = LoadConfigurations(IDR_TXT2);
        ///
        ///        if (!configs.empty())
        ///        {
        ///            PatchMemoryWithConfig(configs);
        ///        }
        ///    }
        ///
        ///    static void __ApplyFileBasedPatches()
        ///    {
        ///        auto configs =  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string BuiltinImplementations {
            get {
                return ResourceManager.GetString("BuiltinImplementations", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // generated by tools
        /// // AHeadLib.Net
        /// // https://github.com/bodong1987/AHeadLib.Net
        /// // Powered by bodong
        ///
        ///#include &lt;windows.h&gt;
        ///#include &quot;MiniTools.h&quot;
        ///
        ///#if defined(_WIN64) || defined(_X64) || defined(WIN64) || defined( __LP64__ )
        ///#define AHEAD_LIB_DOT_NET_X64_BUILD 1
        ///#else
        ///#define AHEAD_LIB_DOT_NET_X64_BUILD 0
        ///#endif
        ///
        /// // checked get function in native dll
        ///static void* CheckedGetFunction(HMODULE module, const char* methodName)
        ///{
        ///    void* Result = GetProcAddress(module, methodName);
        ///
        ///    if (Result == NULL)
        ///    {
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CppHelper {
            get {
                return ResourceManager.GetString("CppHelper", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // generated by tools
        /// // AHeadLib.Net
        /// // https://github.com/bodong1987/AHeadLib.Net
        /// // Powered by bodong
        ///
        ///#include &lt;windows.h&gt;
        ///
        ///extern void CheckedLoad();
        ///extern void ApplyBuiltinPatches();
        ///extern void ExecuteUserCustomCodes();
        ///extern int ShouldExecuteAttachCode();
        ///
        ///BOOL WINAPI DllMain(
        ///    HINSTANCE hinstDLL,  // handle to DLL module
        ///    DWORD fdwReason,     // reason for calling function
        ///    LPVOID lpvReserved)  // reserved
        ///{
        ///    // Perform actions based on the reason for call [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DllMain {
            get {
                return ResourceManager.GetString("DllMain", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // generated by tools
        /// // AHeadLib.Net
        /// // https://github.com/bodong1987/AHeadLib.Net
        /// // Powered by bodong
        ///
        ///#include &quot;MemoryPatchConfig.h&quot;
        ///#include &lt;cassert&gt;
        ///#include &lt;string&gt;
        ///#include &quot;MiniTools.h&quot;
        ///
        ///template &lt;typename TCharType&gt;
        ///inline bool IsTrimable(TCharType ch)
        ///{
        ///    return ch == &apos; &apos; || ch == &apos;\t&apos; || ch == &apos;\n&apos; || ch == &apos;\r&apos;;
        ///}
        ///
        ///inline static int CompareN(const char* first, const char* second, std::size_t n)
        ///{
        ///    return strncmp(first, second, n);
        ///}
        ///
        ///inline static bool StartWith(cons [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MemoryPatchConfig_cpp {
            get {
                return ResourceManager.GetString("MemoryPatchConfig_cpp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // generated by tools
        /// // AHeadLib.Net
        /// // https://github.com/bodong1987/AHeadLib.Net
        /// // Powered by bodong
        ///
        ///#pragma once
        ///
        ///#ifndef  __cplusplus
        ///#error &quot;this header i created for C++ only.&quot;
        ///#endif
        ///
        ///#include &lt;windows.h&gt;
        ///
        ///#include &lt;vector&gt;
        ///#include &lt;string&gt;
        ///
        ///struct MemoryPatchConfig
        ///{
        ///    std::string ModuleName;
        ///    std::string SegmentName;
        ///    std::vector&lt;BYTE&gt; Signature;
        ///    std::vector&lt;BYTE&gt; NewBytes;
        ///
        ///    void Reset()
        ///    {
        ///        ModuleName.clear();
        ///        SegmentName.clear();
        ///   [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MemoryPatchConfig_h {
            get {
                return ResourceManager.GetString("MemoryPatchConfig_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // generated by tools
        /// // AHeadLib.Net
        /// // https://github.com/bodong1987/AHeadLib.Net
        /// // Powered by bodong
        ///
        ///#include &quot;MiniTools.h&quot;
        ///#include &lt;algorithm&gt;
        ///#include &lt;cassert&gt;
        ///
        ///G_BEGIN_DECLS
        ///
        ///BOOL ReplaceMemory(void* dest, const void* source, int length)
        ///{
        ///    void* TargetAddress = dest;
        ///
        ///    DWORD oldProtect;
        ///    if (!VirtualProtect(TargetAddress, length, PAGE_EXECUTE_READWRITE, &amp;oldProtect))
        ///    {
        ///        AHEAD_LIB_SHOW_MESSAGE_BOX(0, TEXT(&quot;Failed to obtain write permission for target address&quot;), TEXT(&quot;Erro [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MiniTools_cpp {
            get {
                return ResourceManager.GetString("MiniTools_cpp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // generated by tools
        /// // AHeadLib.Net
        /// // https://github.com/bodong1987/AHeadLib.Net
        /// // Powered by bodong
        ///
        ///#pragma once
        ///
        ///#include &lt;windows.h&gt;
        ///#include &lt;Psapi.h&gt;
        ///
        ///#ifdef  __cplusplus
        ///
        ///#ifndef G_BEGIN_DECLS
        ///# define G_BEGIN_DECLS  extern &quot;C&quot; {
        ///#endif
        ///
        ///#ifndef G_END_DECLS
        ///# define G_END_DECLS    }
        ///#endif
        ///
        ///#else
        ///
        ///#ifndef G_BEGIN_DECLS
        ///# define G_BEGIN_DECLS
        ///#endif
        ///
        ///#ifndef G_END_DECLS
        ///# define G_END_DECLS
        ///#endif
        ///
        ///#endif
        ///
        /// // if you want to disable error report by message box
        /// // ch [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MiniTools_h {
            get {
                return ResourceManager.GetString("MiniTools_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ; auto patch ${LIB_NAME}.dll&apos;s segments, use this format:
        ///;
        ///; use ; start a comment line
        ///;library : dll name with .dll extension or 0, 0 is the main module
        ///;segenemnt : segment name, eg: .text|.data
        ///;signature : hex byte value sequence, splitted by space
        ///;newBytes : hex byte value sequence, splitted by space
        ///
        ///; for example:
        ///;library    : 0
        ///;segment    : .text
        ///;signature  : 0x1D 0x2D 0x3D
        ///;newBytes   : 0x55 0x6B 0x7D
        ///
        ///; for example2 : 
        ///;library    : proxy.dll
        ///;segment    : .text
        ///;signature   [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string patch {
            get {
                return ResourceManager.GetString("patch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to //{{NO_DEPENDENCIES}}
        /// // Microsoft Visual C++ generated file
        /// // for ${LIB_NAME} use
        /// //
        ///#define IDR_TXT2                        102
        ///
        /// // Next default values for new objects
        /// // 
        ///#ifdef APSTUDIO_INVOKED
        ///#ifndef APSTUDIO_READONLY_SYMBOLS
        ///#define _APS_NEXT_RESOURCE_VALUE        103
        ///#define _APS_NEXT_COMMAND_VALUE         40001
        ///#define _APS_NEXT_CONTROL_VALUE         1001
        ///#define _APS_NEXT_SYMED_VALUE           101
        ///#endif
        ///#endif
        ///.
        /// </summary>
        internal static string resource {
            get {
                return ResourceManager.GetString("resource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // Microsoft Visual C++ generated resource script.
        /// //
        ///#include &quot;resource.h&quot;
        ///
        ///#define APSTUDIO_READONLY_SYMBOLS
        /// /////////////////////////////////////////////////////////////////////////////
        /// //
        /// // Generated from the TEXTINCLUDE 2 resource.
        /// //
        ///#include &quot;winres.h&quot;
        ///
        /// /////////////////////////////////////////////////////////////////////////////
        ///#undef APSTUDIO_READONLY_SYMBOLS
        ///
        /// /////////////////////////////////////////////////////////////////////////////
        /// // 中文(简体，中国) resources
        ///
        ///#if !defined(AFX_RE [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string resource_rc {
            get {
                return ResourceManager.GetString("resource_rc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] solution {
            get {
                object obj = ResourceManager.GetObject("solution", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // generated by tools
        /// // AHeadLib.Net
        /// // https://github.com/bodong1987/AHeadLib.Net
        /// // Powered by bodong
        ///
        ///#include &quot;MemoryPatchConfig.h&quot;
        ///#include &lt;cassert&gt;
        ///#include &lt;tchar.h&gt;
        ///
        ///#define STRINGIFY(x) #x
        ///#define TOSTRING(x) STRINGIFY(x)
        ///
        ///#define MESSAGE_RAISE_LOCATION __FILE__ &quot;(&quot; TOSTRING(__LINE__) &quot;): &quot;
        ///
        ///extern &quot;C&quot;
        ///{
        ///    // fill original library path here
        ///    // use absolute file path
        ///    void GetOriginalLibraryPath(TCHAR* bufferPtr, int bufferLength, const TCHAR* libName)
        ///    {
        ///        ass [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string UserImplementations {
            get {
                return ResourceManager.GetString("UserImplementations", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] vcxprojTemplate {
            get {
                object obj = ResourceManager.GetObject("vcxprojTemplate", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] vcxprojTemplate_filters {
            get {
                object obj = ResourceManager.GetObject("vcxprojTemplate_filters", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
