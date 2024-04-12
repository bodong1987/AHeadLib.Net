# AHeadLib.Net
The C# version of AHeadLib solves various problems such as instability, incompatibility with x64, and stuck file selection of the C++ version provided by many previous enthusiasts.    
This version supports both x86 and x64. The generated C++ project also supports camouflage of x86 and x64 DLLs. Just select the corresponding compilation configuration.  
    
C#版本的AHeadLib，解决了之前的很多爱好者提供的C++版本的不稳定、不兼容x64、选择文件卡死等各种问题。  
此版本同时支持x86和x64，生成的C++项目也同时支持对x86和x64的dll的伪装，选择对应的编译配置即可。  

# Overview
This project is based on .NET Framework 4.7.2 and DevExpress v22.1.4. If you want to use it, you can directly download the compressed package below [Release](https://github.com/bodong1987/AHeadLib.Net/releases). To compile you need to install DevExpress.  
  
此项目基于.NET Framework 4.7.2和DevExpress v22.1.4。如果想使用可以直接下载[Release](https://github.com/bodong1987/AHeadLib.Net/releases)下面的压缩包。要编译，您需要安装 DevExpress。  

**Note that this tool only supports exporting pure C interfaces. Some complex symbols such as C++ symbols are not supported for export. So there is a possibility that the new dll lacks necessary symbols. If your dll is a dll with only pure C API, such as winmm.dll, you can use it freely without any problem.**  
  
**注意，这个工具只支持导出纯C的接口，一些C++符号之类的复杂符号是不支持导出的。所以新的dll存在缺少必要符号的可能性。如果你的dll是一些只有纯C API的dll，诸如winmm.dll，那可以随意使用，不会有问题的。**

# How to Use
Open the tool and select the dll you want to disguise in the first line.
The second line selects a directory. Then click Generate, it will generate a complete Visual Studio 2022 C++ project for you. If nothing unexpected happens, open the corresponding sln and you can compile it.  
If you want to obtain other versions of the project, you can refer to the corresponding configuration and create a new project, or you can directly modify the vcxproj file to achieve your goal.   
  
打开工具，在第一行选择你要伪装的dll。
第二行选择一个目录。然后点击Generate，它会为你生成一个完整的Visual Studio 2022 C++项目。如果不出意外，打开对应的sln就可以编译了。  
如果你希望获取其它版本的项目，可以参考对应的配置，新建一个项目，也可以直接修改vcxproj文件达到目的。  

# Change the code
There are two places that require your attention:  
1. For the case DLL_PROCESS_ATTACH branch in $(DllName)_DllMain.c, you need to add your own code after __CheckedLoad() to execute it.
2. You need to check the __CheckedLoad() function in the $(DllName).c file. The original Dll will be automatically loaded here for you, but the default path is the system path, which is C:\Windows\System32. If necessary, you may need to modify the code here to ensure that the path points to the original Dll file.  

有两个地方需要你关注：
1. $(Dll名称)_DllMain.c 中的case DLL_PROCESS_ATTACH分支，你需要将你自己的代码添加到 __CheckedLoad()后面去执行
2. 你需要检查$(Dll名称).c文件中的__CheckedLoad()函数。这里会自动帮你加载原始Dll，但是默认路径是系统路径，即C:\Windows\System32。如有必要，你可能需要修改这里的代码确保路径指向原始Dll文件。  
