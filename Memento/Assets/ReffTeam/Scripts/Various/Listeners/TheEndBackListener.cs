using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEndBackListener : MonoBehaviour {

    public void Awake()
    {
        SoundManager.GetInstance().PlaySFX("Victory");
    }

    public void BackToMenu()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
	}

}
