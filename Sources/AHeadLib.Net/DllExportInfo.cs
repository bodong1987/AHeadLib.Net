using System.Collections.Generic;
using System.Linq;

namespace AHeadLib.Net;

public static class DllExportInfo
{
    public static IEnumerable<string> ReadFromFile(string path)
    {
        var peFile = new PeNet.PeFile(path);
        return peFile.ExportedFunctions?.Select(x => x.Name);
    }
}