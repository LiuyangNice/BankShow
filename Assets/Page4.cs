using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Page4 : MonoBehaviour {
    public Button returnBtn;
    public Image newPicture;
	// Use this for initialization
	void Start () {

        returnBtn.onClick.AddListener(PageSwitch);
        newPicture.sprite = Manager.Inst.currentUser.NewTexture;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void PageSwitch()
    {
        Manager.Inst.pages[3].gameObject.SetActive(false);
        Manager.Inst.pages[1].gameObject.SetActive(true);
    }
}
