using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class CreatQR 
{

    /// <summary>
    /// 定义方法生成二维码 
    /// </summary>
    /// <param name="textForEncoding">需要生产二维码的字符串</param>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    /// <returns></returns>       
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }


    /// <summary>  
    /// 生成二维码  
    /// </summary>  
    public static Texture2D Btn_CreatQr(string url)
    {
        Texture2D encoded = new Texture2D(256, 256);
        if (url!=null)
        {
            //二维码写入图片    
            var color32 = Encode(url, 256, 256);
            encoded.SetPixels32(color32);
            encoded.Apply();
            //生成的二维码图片附给RawImage    
            return encoded;
        }
        else
        {
            GameObject.Find("Text_1").GetComponent<Text>().text = "没有生成信息";
            return encoded;
        }
    }
}
