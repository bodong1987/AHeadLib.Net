// generated by tools
// AHeadLib.Net
// https://github.com/bodong1987/AHeadLib.Net
// Powered by bodong

#include "MemoryPatchConfig.h"
#include <cassert>

#define STRINGIFY(x) #x
#define TOSTRING(x) STRINGIFY(x)

#define MESSAGE_RAISE_LOCATION __FILE__ "(" TOSTRING(__LINE__) "): "

extern "C"
{
    // fill original library path here
    // use absolute file path
    void GetOrignalLibraryPath(TCHAR* bufferPtr, int bufferLength, const TCHAR* libName)
    {
        assert(bufferPtr != nullptr);
        assert(libName != nullptr);

        // By default, the path of the original Library is considered to be the system directory. 
        // If you need another path, be sure to modify the code here and replace it with the correct location.
        #pragma message(MESSAGE_RAISE_LOCATION "If you need to adjust the absolute path of the original library file, please modify this.")

        GetSystemDirectory(bufferPtr, bufferLength);
        lstrcat(bufferPtr, TEXT("\\"));
        lstrcat(bufferPtr, libName);
    }


    // apply use custom operation
    void __ExecuteUserCutomCodes()
    {
        #pragma message(MESSAGE_RAISE_LOCATION "If you want to append custom code that is executed when the Dll is loaded, please add it here.")
    }
}