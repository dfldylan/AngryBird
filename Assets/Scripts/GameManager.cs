using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager _instant;
    private Vector3 originPos;
    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;
    private int starNum;
    private const int allLevel = 10;

    private void Awake()
    {
        _instant = this;
        starNum = 0;
        if(birds.Count>0){
            originPos = birds[0].transform.position;
        }
    }

    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++){
            if(i == 0){
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sj.enabled = true;
                birds[i].controlable = true;
            }
            else{
                birds[i].enabled = false;
                birds[i].sj.enabled = false;
            }
        }
    }

    public void Next(){
        if(pigs.Count>0){
            if(birds.Count>0){
                Initialized();
            }
            else{//failed
                lose.SetActive(true);
            }
        }
        else{//success
            win.SetActive(true);

        }
    }

    public void StarControl(){
        StartCoroutine("Show");
    }

    IEnumerator Show(){
        for (; starNum < birds.Count + 1; starNum++)
        {
            if (starNum >= stars.Length) break;
            yield return new WaitForSeconds(0.5f);
            stars[starNum].SetActive(true);
        }
        SaveData();
    }


    public void Home(){
        SceneManager.LoadScene(1);
    }

    public void Retry(){
        SceneManager.LoadScene(2);
    }

    private void SaveData(){
        string nowLevel = PlayerPrefs.GetString("nowLevel");
        print(nowLevel);
        int mapNum = int.Parse(nowLevel.Substring(5, 1));
        //int levelNum = int.Parse(nowLevel.Substring(1));
        //int nowStars = PlayerPrefs.GetInt(nowLevel, 0);
        //if (starNum >nowStars){
            PlayerPrefs.SetInt(nowLevel, starNum);
        //}

        int sum = 0;
        for (int i = 1; i <= allLevel; i++) {
            sum += PlayerPrefs.GetInt("level" + mapNum.ToString() + i.ToString(), 0);
        }
        PlayerPrefs.SetInt("totalNum" + mapNum.ToString(), sum);
    }
}
