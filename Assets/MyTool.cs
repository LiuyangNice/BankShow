using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public static class MyTool  {
    public static bool SaveLocalFile(string fileName, byte[] data)

    {

        string path = Application.persistentDataPath + "/" + fileName;
        Debug.Log(path);
        if (File.Exists(path))

            File.Delete(path);

        FileStream fs = new FileStream(path, FileMode.CreateNew);

        if (fs == null)

            return false;

        fs.Write(data, 0, data.Length);

        fs.Close();

        return true;



    }

}
