using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour {

    private Animator anim;
    public GameObject pauseButton;
    private bool controlTemp;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controlTemp = false;
    }

    public void PauseButton(){
        if(GameManager._instant.birds.Count>0){
            if(GameManager._instant.birds[0].controlable == true){
                controlTemp = true;
                GameManager._instant.birds[0].controlable = false;
            }
        }
        pauseButton.SetActive(false);
        anim.SetBool("isPause", true);
    }

    public void ResumeButton(){
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    public void Pause(){
        Time.timeScale = 0;
    }

    public void Resume(){
        pauseButton.SetActive(true);
        if (controlTemp == true)
        {
            GameManager._instant.birds[0].controlable = true;
            controlTemp = false;
        }

    }

    public void RetryButton(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void HomeButton(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

}
