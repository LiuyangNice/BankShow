using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Qiniu.RS;
using Qiniu.Conf;
using Qiniu.IO.Resumable;
using Qiniu.RPC;
using Newtonsoft.Json;
public class QiniuManager  {
    private static readonly string qiniuBucket = "eastbank";
    public static string qiniufileName;
    
    public static void InstManager()
    {
        Qiniu.Conf.Config.ACCESS_KEY = "G_hN0uEXi1zW0_Z0HTJZ6x1lDjjRxdjBFnXltaiM";
        Qiniu.Conf.Config.SECRET_KEY = "rf4qSYltxSmRzdUENnW3pkmujRIYzcilg2GrGDYU";
    }   
    public static void MyUpload(string path)
    {
        Debug.Log(path);
        if (string.IsNullOrEmpty(path))
        {
            return;
        }
        PutPolicy policy = new PutPolicy(qiniuBucket, 3600);
        string upToken = policy.Token();
        string key = SystemInfo.deviceUniqueIdentifier + "_" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + Path.GetFileName(path);
        Settings setting = new Settings();

        ResumablePutExtra extra = new ResumablePutExtra();

        ResumablePut client = new ResumablePut(setting, extra);

        CallRet callRet = client.PutFile(upToken, path, key);

        ReturnBody returnBody = JsonConvert.DeserializeObject<ReturnBody>(callRet.Response);
        
        qiniufileName = returnBody.key;
        Debug.Log(qiniufileName);
        Debug.Log(callRet.Response);
        Debug.Log(callRet.Exception);
        Debug.Log(callRet.StatusCode);

    }
}
public class ReturnBody
{
    public string key;
    public string hash;
}