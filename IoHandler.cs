using System.Collections;

namespace App;

public static class IoHandler
{

    public static void CleanFilesDir()
    {
        var di = new DirectoryInfo("./files");
        foreach (var fi in di.GetFiles())
            fi.Delete();
    }

    public static int GetFilesNumber()
    {
        var di = new DirectoryInfo("./files");
        return di.GetFiles().Length;
    }

    public static FileInfo[] ReadAllFiles()
    {
        var di = new DirectoryInfo("./files");
        return di.GetFiles();
    }
}