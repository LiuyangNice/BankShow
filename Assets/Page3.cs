using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Page3 : MonoBehaviour {
    public Image TargetImage;
    public Image timer;
    public Sprite[] timerSps;
    public Button TakePhotos;
    public int waitTime = 10;
	// Use this for initialization
	void Start () {
        TargetImage.sprite = Manager.Inst.currentUser.PersonTexture;
        TakePhotos.onClick.AddListener(TakePhoto);
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
            timer.sprite = timerSps[a];
            a--;
            yield return new WaitForSeconds(1);
        }
        Take();
    }
    void Take()
    {

    }
    void PageSwitch()
    {
        Manager.Inst.pages[2].gameObject.SetActive(false);
        Manager.Inst.pages[3].gameObject.SetActive(true);
    }
}
