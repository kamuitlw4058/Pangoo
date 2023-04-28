using System.Collections;
using System.Collections.Generic;
using System.IO;
public static class DirectoryUtility
{
    public static void ExistsOrCreate(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
