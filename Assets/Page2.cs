using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Page2 : MonoBehaviour {
    public Sprite[] mans;
    public Sprite[] womans;
    public Button[] btns;
    Dictionary<string, Sprite[]> dic = new Dictionary<string, Sprite[]>();
	// Use this for initialization
	void Start () {
       
        btns[0].onClick.AddListener(TexttureClick1);
        btns[1].onClick.AddListener(TexttureClick2);
        btns[2].onClick.AddListener(TexttureClick3);
        btns[3].onClick.AddListener(TexttureClick4);
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].onClick.AddListener(PageSwitch);
        }
    }
    private void Awake()
    {
        InstPage2(Manager.Inst.currentUser.Gender);
    }
    public void InstPage2(string gender)
    {
        dic.Add("man", mans);
        dic.Add("woman", womans);
        for (int i = 0; i < btns.Length; i++)
        {
            try
            {
                btns[i].transform.GetComponent<Image>().sprite = dic[gender][i];
            }
            catch (System.Exception)
            {
                Debug.Log(gender);
                Debug.Log(dic.Count);
                throw;
            }
            
        }
       
    }
	// Update is called once per frame
	void Update () {
		
	}
    void TexttureClick1()
    {
        Manager.Inst.currentUser.PersonTexture = dic[Manager.Inst.currentUser.Gender][0];
        Debug.Log("aaa");
    }
    void TexttureClick2()
    {
        Manager.Inst.currentUser.PersonTexture = dic[Manager.Inst.currentUser.Gender][1];
    }
    void TexttureClick3()
    {
        Manager.Inst.currentUser.PersonTexture = dic[Manager.Inst.currentUser.Gender][2];
    }
    void TexttureClick4()
    {
        Manager.Inst.currentUser.PersonTexture = dic[Manager.Inst.currentUser.Gender][3];
    }
    void PageSwitch()
    {
        Manager.Inst.pages[1].gameObject.SetActive(false);
        Manager.Inst.pages[2].gameObject.SetActive(true);
    }
}
