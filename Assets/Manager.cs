using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour {
    public static Manager Inst;
    public Page currentPage;
    public User currentUser;
    public Transform mainPage;
    public List<Transform> pages=new List<Transform>();
    public StateMachine<Manager> myMachine;

    public Dictionary<string, string[]> modolDic =new Dictionary<string, string[]>();

    /// <summary>
    /// page1
    /// </summary>
    public Button man, woman;
    /// <summary>
    /// page2
    /// </summary>
    public RawImage PImage;
    public Image TargetImage;
    public Image timer;
    public Sprite[] timerSps;
    public Button TakePhotos;
    public int waitTime = 10;
    /// <summary>
    /// page3
    /// </summary>
    public Button[] btns;
    
    /// <summary>
    /// page4
    /// </summary>
    public Button returnBtn;
    public Image newPicture;

    private void Awake()
    {
        Inst = this;
        myMachine = new StateMachine<Manager>(this);
    }
    // Use this for initialization
    void Start () {
        InstManager();
        myMachine.SetCourrentState(new Page01());

    }
	public void InstManager()
    {
        currentPage = Page.page1;
        currentUser = new User();
        for (int i = 0; i < mainPage.childCount; i++)
        {
            pages.Add(mainPage.GetChild(i));
        }
        string[] mans = new string[4] { "youtu_76171_20180626155321_1433", "cf_lover_libai", "cf_lover_wuque", "cf_movie_yehua" };
        string[] womans = new string[4] { "cf_fuwa_yasuiqian", "cf_lover_sunshang", "cf_lover_yuhuan", "cf_movie_fengjiu" };
        modolDic.Add("man", mans);
        modolDic.Add("woman", womans);
    }
	// Update is called once per frame
	void Update () {
        RectTransform rect = pages[1].Find("Target") as RectTransform;
        Debug.Log(rect.position);
        myMachine.UpdateFsm();
	}
    void PageUpdate(Page currentPage)
    {
        switch (currentPage)
        {
            case Page.page1:
                break;
            case Page.page2:
                break;
            case Page.page3:
                break;
            case Page.page4:
                break;
            default:
                break;
        }
    }
}
public enum Page
{
    page1,
    page2,
    page3,
    page4
}
public class User
{
    string uName;
    string gender;
    Sprite personTexture;
    Sprite newTexture;
    Sprite cameraTexture;
    public string Gender
    {
        get
        {
            return gender;
        }

        set
        {
            gender = value;
        }
    }

    public Sprite PersonTexture
    {
        get
        {
            return personTexture;
        }

        set
        {
            personTexture = value;
        }
    }

    public Sprite NewTexture
    {
        get
        {
            return newTexture;
        }

        set
        {
            newTexture = value;
        }
    }

    public string UName
    {
        get
        {
            return uName;
        }

        set
        {
            uName = value;
        }
    }

    public Sprite CameraTexture
    {
        get
        {
            return cameraTexture;
        }

        set
        {
            cameraTexture = value;
        }
    }
}