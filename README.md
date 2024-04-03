# AHeadLib.Net
The C# version of AHeadLib, it solves various problems such as instability and incompatibility with x64 of previous versions.This version supports both x86 and x64. The generated C++ project also supports camouflage of x86 and x64 DLLs. Just select the corresponding compilation configuration.  
    
C#版本的AHeadLib，解决了之前版本不稳定、不兼容x64等各种问题。此版本同时支持x86和x64，生成的C++项目也同时支持对x86和x64的dll的伪装，选择对应的编译配置即可。  

# Overview
This project is based on .NET Framework 4.7.2 and DevExpress v22.1.4. If you want to use it, you can directly download the compressed package below [Release](https://github.com/bodong1987/AHeadLib.Net/releases). To compile you need to install DevExpress.  
  
此项目基于.NET Framework 4.7.2和DevExpress v22.1.4。如果想使用可以直接下载[Release](https://github.com/bodong1987/AHeadLib.Net/releases)下面的压缩包。要编译，您需要安装 DevExpress。  

**Note that this tool only supports exporting dlls with pure C interfaces. Some dlls with C++ export symbols are not supported. You may get a C++ project that cannot be compiled.**  
  
**注意，这个工具只支持导出纯C接口的dll，一些拥有C++导出符号的dll是不被支持的，你可能会得到一个无法编译通过的C++ 项目。**

# How to Use
Open the tool and select the dll you want to disguise in the first line.
The second line selects a directory. Then click Generate, it will generate a complete Visual Studio 2022 C++ project for you. If nothing unexpected happens, open the corresponding sln and you can compile it.  
If you want to obtain other versions of the project, you can refer to the corresponding configuration and create a new project, or you can directly modify the vcxproj file to achieve your goal.   
  
打开工具，在第一行选择你要伪装的dll。
第二行选择一个目录。然后点击Generate，它会为你生成一个完整的Visual Studio 2022 C++项目。如果不出意外，打开对应的sln就可以编译了。  
如果你希望获取其它版本的项目，可以参考对应的配置，新建一个项目，也可以直接修改vcxproj文件达到目的。