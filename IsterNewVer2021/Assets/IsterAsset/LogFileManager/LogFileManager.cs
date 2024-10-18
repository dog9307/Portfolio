using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LogFileManager
{
    private static void CreateLogFile(string fileName)
    {
        string path = Application.dataPath + "/LogFiles";
        DirectoryInfo di = new DirectoryInfo(path);
        if (!di.Exists)
            di.Create();

        string fullPath = path + "/" + fileName + ".txt";
        if (!File.Exists(fullPath))
        {
            FileStream fs = File.Create(fullPath);
            fs.Flush();
            fs.Close();
        }
    }

    public static void WriteLine(string fileName, string content, bool deletePrev = false)
    {
        CreateLogFile(fileName);

        string path = Application.dataPath + "/LogFiles/";
        string fullPath = path + fileName + ".txt";

        StreamWriter sr = null;
        if (deletePrev)
            sr = File.CreateText(fullPath);
        else
            sr = File.AppendText(fullPath);
        
        sr.WriteLine(System.DateTime.UtcNow.ToLocalTime());
        sr.WriteLine(content);

        sr.Flush();
        sr.Close();
    }
}
