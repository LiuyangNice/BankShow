using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using TencentYoutuYun.SDK.Csharp;

public class Test : MonoBehaviour {
    public string qq = "1411636691";
    public string AppID = "10138488";
    public string SecretID= "AKID30mOAaSIfrrkbJrCMNDJGOQRNznYbdw4";
    public string SecretKey= "6DE4DIF54yiHeqrkjaHnCRWRBnHbnpgM";
    const double EXPIRED_SECONDS = 2592000;
    const int HTTP_BAD_REQUEST = 400;
    const int HTTP_SERVER_ERROR = 500;
    public string Authorization;
    public long timere;
    public Image rawImage;
    string  picUrl ;
    byte[] tex;
    //string aaa = "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";
    // Use this for initialization
    void Start () {
       
        Conf.Instance().setAppInfo(AppID, SecretID, SecretKey, qq, Conf.Instance().YOUTU_END_POINT);
        picUrl = Application.dataPath + "/Resources/test1.png";
        string y= doFaceMerge(picUrl, "cf_lover_libai");
        returnObj returnObj = JsonConvert.DeserializeObject<returnObj>(y);
        Debug.Log(returnObj.img_base64);
        Base64ToImg(rawImage, returnObj.img_base64);
    }
	
	// Update is called once per frame
	void Update () {
		
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

    public void Base64ToImg(Image imgComponent,string base64)
    {   
        byte[] bytes = Convert.FromBase64String(base64);
        Texture2D tex2D = new Texture2D(100, 100);
        tex2D.LoadImage(bytes);
        Sprite s = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f));
        imgComponent.sprite = s;
        Resources.UnloadUnusedAssets();
    }
    public static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalMilliseconds).ToString();
    }
}

//class returnObj {
//    public int ret;
//    public string msg;
//    public string img_base64;

//}


