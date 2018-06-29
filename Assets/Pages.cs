using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Page01 : State<Manager> {
    public override void EnterState(Manager target)
    {
        Manager.Inst.currentUser.UName = MyYouTU.GetTimeStamp();
        Manager.Inst.pages[0].gameObject.SetActive(true);
        target.man.onClick.AddListener(Man);
        target.man.onClick.AddListener(PageSwitch);
        target.woman.onClick.AddListener(Woman);
       target.woman.onClick.AddListener(PageSwitch);
    }
    public override void UpdateState(Manager target)
    {
        
    }
    public override void ExitState(Manager target)
    {
        target.man.onClick.RemoveAllListeners();
        target.woman.onClick.RemoveAllListeners();
        Manager.Inst.pages[0].gameObject.SetActive(false);
    }
    void Man()
    {
        Manager.Inst.currentUser.Gender = "man";
    }
    void Woman()
    {
        Manager.Inst.currentUser.Gender = "woman";
    }
    void PageSwitch()
    {

        target.myMachine.ChangeState(new Page02());
        
    }
}
public class Page02 : State<Manager>
{
    public string deviceName;
    WebCamTexture tex;
    public override void EnterState(Manager target)
    {
        Manager.Inst.pages[1].gameObject.SetActive(true);
        target.TakePhotos.onClick.AddListener(TakePhoto);
        target.StartCoroutine(start());
        target.timer.enabled = false;
    }
    public override void UpdateState(Manager target)
    {

    }
    public override void ExitState(Manager target)
    {
        tex.Stop();
        target.TakePhotos.onClick.RemoveAllListeners();
        Manager.Inst.pages[1].gameObject.SetActive(false);
    }
    void TakePhoto()
    {
        target.StartCoroutine(timerIe());
    }
    IEnumerator timerIe()
    {
        target.timer.enabled = true;
        int a = target.waitTime;
        while (a >= 0)
        {
            a--;
            if (a >= 0)
            {
                target.timer.sprite = target.timerSps[a];
            }
            yield return new WaitForSeconds(1);
        }
        yield return getTexture();
        PageSwitch();
    }
    void Take()
    {
        target.StartCoroutine(getTexture());

    }
    void PageSwitch()
    {
        target.myMachine.ChangeState(new Page03());
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
            tex = new WebCamTexture(deviceName, 300, 350, 12);
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
        RectTransform rect = target.pages[1].Find("Target") as RectTransform;
        yield return new WaitForEndOfFrame();
        Texture2D t = new Texture2D((int)rect.rect.width, (int)rect.rect.height);
        t.ReadPixels(new Rect(rect.position.x, rect.position.y, (int)rect.rect.width, (int)rect.rect.height), 0, 0, false);
        //距X左的距离        距Y屏上的距离  
        //t.ReadPixels(new Rect(220, 180, 200, 180), 0, 0, false);  
        t.Apply();
        byte[] byt = t.EncodeToPNG();
        yield return new WaitUntil(() => SaveLocalFile(Manager.Inst.currentUser.UName+".png", byt));
        tex.Play();
    }
    bool SaveLocalFile(string fileName, byte[] data)

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
public class Page03 : State<Manager>
{
    public override void EnterState(Manager target)
    {
        Manager.Inst.pages[2].gameObject.SetActive(true);

        MyYoutu();
        target.btns[0].onClick.AddListener(TexttureClick1);
        target.btns[1].onClick.AddListener(TexttureClick2);
        target.btns[2].onClick.AddListener(TexttureClick3);
        target.btns[3].onClick.AddListener(TexttureClick4);
        for (int i = 0; i < target.btns.Length; i++)
        {
            target.btns[i].onClick.AddListener(PageSwitch);
        }
        InstPage2(target.currentUser.Gender);
    }
    public override void UpdateState(Manager target)
    {

    }
    public override void ExitState(Manager target)
    {
        foreach (Button item in target.btns)
        {
            item.onClick.RemoveAllListeners();
        }
        Manager.Inst.pages[2].gameObject.SetActive(false);
    }
    public void MyYoutu()
    {
        MyYouTU.Inst();
        for (int i = 0; i < 4; i++)
        {
            Sprite sprite= MyYouTU.GetFaceMerge(Application.persistentDataPath  + "/" +  target.currentUser.UName+".png", target.modolDic[target.currentUser.Gender][i]);
            target.btns[i].transform.GetComponent<Image>().sprite = sprite;
            Debug.Log(i);
        }
    }
    public void InstPage2(string gender)
    {

    }
    void TexttureClick1()
    {
        Manager.Inst.currentUser.PersonTexture = target.btns[0].transform.GetComponent<Image>().sprite;
    }
    void TexttureClick2()
    {
        Manager.Inst.currentUser.PersonTexture = target.btns[1].transform.GetComponent<Image>().sprite;
    }
    void TexttureClick3()
    {
        Manager.Inst.currentUser.PersonTexture = target.btns[2].transform.GetComponent<Image>().sprite;
    }
    void TexttureClick4()
    {
        Manager.Inst.currentUser.PersonTexture = target.btns[3].transform.GetComponent<Image>().sprite;
    }
    void PageSwitch()
    {
        target.myMachine.ChangeState(new Page04());
    }
}

public class Page04 : State<Manager>
{
    public override void EnterState(Manager target)
    {
        Manager.Inst.pages[3].gameObject.SetActive(true);
        target.returnBtn.onClick.AddListener(PageSwitch);
        target.newPicture.sprite = Manager.Inst.currentUser.PersonTexture;
    }
    public override void UpdateState(Manager target)
    {

    }
    public override void ExitState(Manager target)
    {
        target.returnBtn.onClick.RemoveAllListeners();
        Manager.Inst.pages[3].gameObject.SetActive(false);
    }
    void PageSwitch()
    {
        target.currentUser = new User();
        target.myMachine.ChangeState(new Page01());
    }
}
