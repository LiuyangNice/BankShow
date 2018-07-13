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

    public RawImage rawImage;
    string url = "https://www.baidu.com/";
    void Start () {
        rawImage.texture = CreatQR.Btn_CreatQr(url);


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    


   
}



