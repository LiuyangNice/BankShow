using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using TencentYoutuYun.SDK.Csharp;
public class MyYouTU {

    public static string qq = "1411636691";
    public static string AppID = "10138488";
    public static string SecretID = "AKID30mOAaSIfrrkbJrCMNDJGOQRNznYbdw4";
    public static string SecretKey = "6DE4DIF54yiHeqrkjaHnCRWRBnHbnpgM";
    const double EXPIRED_SECONDS = 2592000;
    const int HTTP_BAD_REQUEST = 400;
    const int HTTP_SERVER_ERROR = 500;
    string picUrl;

    public static void Inst()
    {
        Conf.Instance().setAppInfo(AppID, SecretID, SecretKey, qq, Conf.Instance().YOUTU_END_POINT);
    }
    public static Sprite GetFaceMerge(string picUrl,string modolID)
    {
        Sprite s;
        string y = doFaceMerge(picUrl, modolID);

        returnObj returnObj = JsonConvert.DeserializeObject<returnObj>(y);
        if (returnObj.img_base64!=null)
        {
            s = Base64ToImg(returnObj.img_base64);
        }
        else
        {
            s = new Sprite();
            Debug.Log(y);
        }
        return s;
    }
    public static string doFaceMerge(string path, string ModleId)
    {
        //string result;
        string expired = TencentYoutuYun.SDK.Csharp.Common.Utility.UnixTime(EXPIRED_SECONDS);
        string methodName = "cgi-bin/pitu_open_access_for_youtu.fcg";
        StringBuilder postData = new StringBuilder();
        string pars = "\"img_data\":\"" + TencentYoutuYun.SDK.Csharp.Common.Utility.ImgBase64(path) + "\", \"rsp_img_type\": \"base64\",\"opdata\": [{\"cmd\": \"doFaceMerge\",\"params\": {\"model_id\":\"" + ModleId + "\" }}]";
        //string pars = "\"app_id\":\"{0}\",\"image\":\"{1}\",\"mode\":{2}";
        //pars = string.Format(pars, Conf.Instance().APPID, TencentYoutuYun.SDK.Csharp.Common.Utility.ImgBase64(image_path), isbigface);
        postData.Append("{");
        postData.Append(pars);
        postData.Append("}");
        string result = Http.HttpPost(methodName, postData.ToString(), Auth.appSign(expired, Conf.Instance().USER_ID));
        //return result;
        return result;

    }
    public static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalMilliseconds).ToString();
    }
    string recordBase64String;
    /// <summary>
    /// 图片转换成base64编码文本
    /// </summary>
    public void ImgToBase64String(string path)
    {
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            string base64String = Convert.ToBase64String(buffer);
            Debug.Log("获取当前图片base64为---" + base64String);
            recordBase64String = base64String;
        }
        catch (Exception e)
        {
            Debug.Log("ImgToBase64String 转换失败:" + e.Message);
        }
    }


    /// <summary>
    /// base64编码文本转换成图片
    /// </summary>

    static Sprite Base64ToImg(string base64)
    {
        byte[] bytes = Convert.FromBase64String(base64);
        Texture2D tex2D = new Texture2D(100, 100);
        tex2D.LoadImage(bytes);
        Sprite s = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f));
        return s;
    }

}

class returnObj
{
    public int ret;
    public string msg;
    public string img_base64;

}