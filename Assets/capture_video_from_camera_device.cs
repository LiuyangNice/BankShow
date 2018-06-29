using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

public class capture_video_from_camera_device : MonoBehaviour
{
    public string deviceName;
    //接收返回的图片数据  
    WebCamTexture tex;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 20, 100, 40), "start"))
        {
            // 调用摄像头  
            StartCoroutine(start());
        }

        if (GUI.Button(new Rect(10, 70, 100, 40), "Pause"))
        {
            //捕获照片  
            tex.Pause();
            StartCoroutine(getTexture());
        }

        if (GUI.Button(new Rect(10, 120, 100, 40), "replay"))
        {
            //重新开始  
            tex.Play();
        }

        if (GUI.Button(new Rect(120, 20, 80, 40), "record"))
        {
            //录像  
            StartCoroutine(SeriousPhotoes());
        }

        if (GUI.Button(new Rect(10, 170, 100, 40), "stop"))
        {
            //停止捕获镜头  
            tex.Stop();
            StopAllCoroutines();
        }

        if (tex != null)
        {
            
            // 捕获截图大小               —距X左屏距离   |   距Y上屏距离   
            //GUI.DrawTexture(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 190, 280, 200), tex);
        }

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
            tex = new WebCamTexture(deviceName, 300, 300, 12);
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
        t.ReadPixels(new Rect(1151,390,200,300), 0, 0, false);
        //距X左的距离        距Y屏上的距离  
         //t.ReadPixels(new Rect(220, 180, 200, 180), 0, 0, false);  
        t.Apply();
        byte[] byt = t.EncodeToPNG();
        yield return new WaitUntil(()=> SaveLocalFile("aaa.jpg", byt));
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
    /// <summary>  
    /// 连续捕获照片  
    /// </summary>  
    /// <returns>The photoes.</returns>  
    public IEnumerator SeriousPhotoes()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            Texture2D t = new Texture2D(400, 300, TextureFormat.RGB24, true);
            t.ReadPixels(new Rect(Screen.width / 2 - 180, Screen.height / 2 - 50, 360, 300), 0, 0, false);
            t.Apply();
            print(t);
            byte[] byt = t.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "/MulPhotoes/" + Time.time.ToString().Split('.')[0] + "_" + Time.time.ToString().Split('.')[1] + ".png", byt);
            Thread.Sleep(300);
        }
    }
}
