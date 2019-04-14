using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    private bool isSelect = false;
    public Sprite levelBG;
    private Image image;
    public GameObject[] stars;
    private int mapNum;
    private int levelNum;
    private Text text;

    private void Awake()
    {
        image = GetComponent<Image>();
        mapNum = int.Parse(transform.parent.parent.name.Substring(5));
        levelNum = int.Parse(gameObject.name);
        GameObject left = transform.GetChild(0).GetChild(0).gameObject;
        GameObject center = transform.GetChild(0).GetChild(1).gameObject;
        GameObject right = transform.GetChild(0).GetChild(2).gameObject;
        stars = new GameObject[3];
        stars.SetValue(left, 0 );
        stars.SetValue(center, 1);
        stars.SetValue(right, 2);
        text = transform.GetChild(0).GetComponent<Text>();
        text.text = levelNum.ToString();
    }

    private void Start()
    {
        if(levelNum == 1)
        {
            isSelect = true;
        }else{
            int beforeNum = levelNum - 1;
            if (PlayerPrefs.GetInt("level" + mapNum.ToString() + beforeNum.ToString()) > 0) {
                isSelect = true;
            }
        }

        if(isSelect == true)
        {
            image.overrideSprite = levelBG;
            //TODO "num" text
            transform.Find("num").gameObject.SetActive(true);
            int starNum = PlayerPrefs.GetInt("level" + mapNum.ToString() + levelNum.ToString(), 0);
            if (starNum > 0) {
                for (int i = 0; i < starNum; i++){
                    stars[i].SetActive(true);
                }
            }

        }
        else{
            GetComponent<Button>().enabled = false;
        }
    }

    public void Selected()
    {
        if(isSelect == true)
        {
            PlayerPrefs.SetString("nowLevel", "level" + mapNum.ToString() + levelNum.ToString());
            SceneManager.LoadScene(2);
        }
    }
}
