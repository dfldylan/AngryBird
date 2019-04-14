using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(800, 450, false);
        Invoke("Load", 2);
	}
	
	// Update is called once per frame
	void Load () {
        SceneManager.LoadSceneAsync(1);
	}
}
