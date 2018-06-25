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
    private void Awake()
    {
        Inst = this;
    }
    // Use this for initialization
    void Start () {
        
	}
	public void InstManager()
    {
        currentPage = Page.page1;
        currentUser = new User();
        for (int i = 0; i < mainPage.childCount; i++)
        {
            pages.Add(mainPage.GetChild(i));
        }
    }
	// Update is called once per frame
	void Update () {
		
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