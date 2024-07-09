# AHeadLib.Net
The C# version of AHeadLib solves various problems such as instability, incompatibility with x64, and stuck file selection of the C++ version provided by many previous enthusiasts.    
This version supports both x86 and x64. The generated C++ project also supports camouflage of x86 and x64 DLLs. Just select the corresponding compilation configuration.  

---  
    
C#版本的AHeadLib，解决了之前的很多爱好者提供的C++版本的不稳定、不兼容x64、选择文件卡死等各种问题。  
此版本同时支持x86和x64，生成的C++项目也同时支持对x86和x64的dll的伪装，选择对应的编译配置即可。  

# Overview 概览
This project is based on .NET Framework 4.7.2 and DevExpress v22.1.4. If you want to use it, you can directly download the compressed package below [Release](https://github.com/bodong1987/AHeadLib.Net/releases). To compile you need to install DevExpress.  

---  
  
此项目基于.NET Framework 4.7.2和DevExpress v22.1.4。如果想使用可以直接下载[Release](https://github.com/bodong1987/AHeadLib.Net/releases)下面的压缩包。要编译，您需要安装 DevExpress。  

**Note that this tool only supports exporting pure C interfaces. Some complex symbols such as C++ symbols are not supported for export. So there is a possibility that the new dll lacks necessary symbols. If your dll is a dll with only pure C API, such as winmm.dll, you can use it freely without any problem.**  

---  
  
  
**注意，这个工具只支持导出纯C的接口，一些C++符号之类的复杂符号是不支持导出的。所以新的dll存在缺少必要符号的可能性。如果你的dll是一些只有纯C API的dll，诸如winmm.dll，那可以随意使用，不会有问题的。**

# How to Use 如何使用
Open the tool and select the dll you want to disguise in the first line.
The second line selects a directory. Then click Generate, it will generate a complete Visual Studio 2022 C++ project for you. If nothing unexpected happens, open the corresponding sln and you can compile it.  
If you want to obtain other versions of the project, you can refer to the corresponding configuration and create a new project, or you can directly modify the vcxproj file to achieve your goal.   

---  
  
打开工具，在第一行选择你要伪装的dll。
第二行选择一个目录。然后点击Generate，它会为你生成一个完整的Visual Studio 2022 C++项目。如果不出意外，打开对应的sln就可以编译了。  
如果你希望获取其它版本的项目，可以参考对应的配置，新建一个项目，也可以直接修改vcxproj文件达到目的。  

# Custom Process 自定义处理
Open the `UserImplementations.cpp` file. There are some user-defined methods where you can customize the original dll file path, decide whether to execute Patch code, and append additional custom code.

---  

打开`UserImplementations.cpp`文件，这里有一些用户自定义方法，你可以在这些方法中定制原始dll文件路径、决定是否执行Patch代码、以及追加额外的自定义代码。  

# Patch template 补丁模板
The tool provides a very simple Patch framework, which will search for segments according to certain rules when the hijacking dll is loaded, and dynamically replace the bytes that meet the requirements with the bytes you provide. There are two methods. One is to directly compile the `${LIB_NAME}_patch.txt` file in the Resource directory and configure it according to the rules inside;   
the other is to create a new `${LIB_NAME}.patchconfig` file and place it with the hijacked dll. Together, configuration strategies and resources are the same.  
for example:  
- Your App Path
    - dbghelp.dll
    - dbghelp.patchconfig ; add your patch config here

Through this method, you can dynamically modify the code without writing code such as `VirtualProtect`, which can provide you with certain conveniences.  
Configuration rules :  
```
; auto patch ${LIB_NAME}.dll's segments, use this format:
;
; use ; start a comment line
;library : dll name with .dll extension or 0, 0 is the main module
;segment : segment name, eg: .text|.data
;signature : hex byte value sequence, split by space
;newBytes : hex byte value sequence, split by space

; for example:
;library    : 0
;segment    : .text
;signature  : 0x1D 0x2D 0x3D
;newBytes   : 0x55 0x6B 0x7D

; for example2 : 
;library    : proxy.dll
;segment    : .text
;signature  : 2B 3C 4F 33
;newBytes   : 1B 2F 3E 23
```

---

工具提供了一个非常简单的Patch框架，它会在劫持dll被加载的时候按照一定的规则搜索Segment，并将符合要求的字节动态替换成你提供的字节。有两种方法，其一是你直接编译Resource目录下的`${LIB_NAME}_patch.txt`文件，按照里面的规则配置即可；  
其二是新建一个`${LIB_NAME}.patchconfig`文件，与劫持dll放在一起，配置策略和资源一样。  
举个例子:  
- 你的应用程序路径
    - dbghelp.dll
    - dbghelp.patchconfig ; 将配置添加到这个文件中

通过这种方法，你可以不用去写`VirtualProtect`之类的代码即可实现动态修改代码，可以为你提供一定的便利。  
配置规则 :  
```
; auto patch ${LIB_NAME}.dll's segments, use this format:
;
; use ; start a comment line
;library : dll name with .dll extension or 0, 0 is the main module
;segment : segment name, eg: .text|.data
;signature : hex byte value sequence, split by space
;newBytes : hex byte value sequence, split by space

; for example:
;library    : 0
;segment    : .text
;signature  : 0x1D 0x2D 0x3D
;newBytes   : 0x55 0x6B 0x7D

; for example2 : 
;library    : proxy.dll
;segment    : .text
;signature  : 2B 3C 4F 33
;newBytes   : 1B 2F 3E 23
```

