using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Page1 : MonoBehaviour {
    public Button man;
    public Button woman;

	// Use this for initialization
	void Start () {
        man.onClick.AddListener(Man);
        man.onClick.AddListener(PageSwitch);
        woman.onClick.AddListener(Woman);
        woman.onClick.AddListener(PageSwitch);
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    // Update is called once per frame
    void Update () {
		
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
        Manager.Inst.pages[0].gameObject.SetActive(false);
        Manager.Inst.pages[1].gameObject.SetActive(true);
        
    }
}
