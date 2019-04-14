using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

    public int starsNum = 0;
    private bool isSelect;
    private int mapNum;
    public GameObject stars;
    public GameObject locks;
    public GameObject map;
    public GameObject panel;
    private Text starNeed;
    private Text starGet;

    private void Awake()
    {
        isSelect = false;
        mapNum = int.Parse(gameObject.name.Substring(3));
        starNeed = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
        starGet = transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        starNeed.text = starsNum.ToString();
        starGet.text = PlayerPrefs.GetInt("totalNum" + mapNum.ToString()).ToString() + "/30";
        if (mapNum == 1)
        {
            isSelect = true;
        }
        else if(PlayerPrefs.GetInt("totalNum" + (mapNum - 1).ToString(), 0) >= starsNum)
        {
            isSelect = true;
        }

        if (isSelect == true)
        {
            stars.SetActive(true);
            locks.SetActive(false);
        }
        else
        {
            stars.SetActive(false);
            locks.SetActive(true);
        }
    }

    public void Select()
    {
        if (isSelect == true)
        {
            map.SetActive(false);
            panel.SetActive(true);
        }
    }

    public void Back()
    {
        map.SetActive(true);
        panel.SetActive(false);
    }
}
