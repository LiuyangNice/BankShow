using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class Page3 : MonoBehaviour {
    public Image TargetImage;
    public Image timer;
    public Sprite[] timerSps;
    public Button TakePhotos;
    public int waitTime = 10;
    public string deviceName;
    WebCamTexture tex;
    // Use this for initialization
    void Start () {
        TargetImage.sprite = Manager.Inst.currentUser.PersonTexture;
        TakePhotos.onClick.AddListener(TakePhoto);
        StartCoroutine(start());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void TakePhoto()
    {
        StartCoroutine(timerIe());
    }
    IEnumerator timerIe()
    {
        int a = waitTime;
        while (a>=0)
        {
            a--;
            if (a>=0)
            {
                timer.sprite = timerSps[a];
            }
            yield return new WaitForSeconds(1);
        }
        yield return getTexture();
        PageSwitch();
    }
    void Take()
    {
        StartCoroutine(getTexture());
        
    }
    void PageSwitch()
    {
        Manager.Inst.pages[2].gameObject.SetActive(false);
        Manager.Inst.pages[3].gameObject.SetActive(true);
    }
    /// <summary>  
    /// 捕获窗口位置  
    /// </summary>  
    public IEnumerator start()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;
            tex = new WebCamTexture(deviceName, 600, 900, 12);
            tex.Play();
            Manager.Inst.PImage.texture = tex;
        }
    }

    /// <summary>  
    /// 获取截图  
    /// </summary>  
    /// <returns>The texture.</returns>  
    public IEnumerator getTexture()
    {
        yield return new WaitForEndOfFrame();
        Texture2D t = new Texture2D(200, 300);
        t.ReadPixels(new Rect(1151, 390, 200, 300), 0, 0, false);
        //距X左的距离        距Y屏上的距离  
        //t.ReadPixels(new Rect(220, 180, 200, 180), 0, 0, false);  
        t.Apply();
        byte[] byt = t.EncodeToPNG();
        yield return new WaitUntil(() => SaveLocalFile("aaa.jpg", byt));
        tex.Play();
    }
    bool SaveLocalFile(string fileName, byte[] data)

    {

        string path = Application.dataPath + "/data/" + fileName;

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
