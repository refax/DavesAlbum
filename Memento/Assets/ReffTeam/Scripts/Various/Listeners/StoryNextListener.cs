using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryNextListener : MonoBehaviour {

    public void GoToLevelSelect()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        PreferencesManager.FirstTimePlayed();
        SceneManager.LoadScene("Level_Select", LoadSceneMode.Single);
    }

}
