using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : SceneController<MainMenuHUDManager>
{

    private void Start()
    {
        SoundManager.GetInstance().PlayMusic("MainMusic", false);
    }

    // Abstract Implementation

    protected override void HUDInitialization()
    {
        hudManager.ChangeMainVisibility(true);
        hudManager.ChangeSettingsVisibility(false);
        hudManager.ChangeCreditsVisibility(false);
        hudManager.ChangeResetVisibility(false);

        #if !UNITY_STANDALONE
            hudManager.DisableExit();
        #endif

        hudManager.ChangeEnableMusicVisibility(!PreferencesManager.IsMusicEnabled());
    }

    protected override void RegisterToHUDDelegate()
    {
        hudManager.OnPlayEvent += LevelSelect;
        hudManager.OnSettingsEvent += DisplaySettings;
        hudManager.OnCreditsEvent += DisplayCredits;
        hudManager.OnTutorialEvent += DisplayTutorial;
        hudManager.OnExitEvent += Exit;

        hudManager.OnEnableMusicEvent += EnableMusic;
        hudManager.OnDisableMusicEvent += DisableMusic;
        hudManager.OnResetEvent += DisplayReset;
        hudManager.OnBackFromSettingsEvent += BackFromSettings;

        hudManager.OnBackFromCreditsEvent += BackFromCredits;

        hudManager.OnResetYesEvent += ResetData;
        hudManager.OnResetNoEvent += BackToSettings;
    }

    //Event

    private void LevelSelect()
    {
        SoundManager.GetInstance().PlaySFX("Button");

        GlobalDataManager.LastLevelPlayed = GlobalDataManager.NO_LEVEL_PLAYED;
        TutorialController.OpenedFromMainMenu = false;
        if (PreferencesManager.IsFirstPlay())
        {
            SceneManager.LoadScene("Story", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Level_Select", LoadSceneMode.Single);
        }
    }

    private void DisplaySettings()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        hudManager.ChangeSettingsVisibility(true);
    }

    private void DisplayCredits()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        hudManager.ChangeCreditsVisibility(true);
    }

    private void Exit()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        Application.Quit();
    }

    private void EnableMusic()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        AudioListener.volume = 1;
        PreferencesManager.EnableMusic();
        hudManager.ChangeEnableMusicVisibility(false);
        SoundManager.GetInstance().PlayMusic("MainMusic", true);
    }

    private void DisableMusic()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        AudioListener.volume = 0;
        PreferencesManager.DisableMusic();
        hudManager.ChangeEnableMusicVisibility(true);
    }

    private void BackFromSettings()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        hudManager.ChangeSettingsVisibility(false);
    }

    private void BackFromCredits()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        hudManager.ChangeCreditsVisibility(false);
    }


    private void DisplayReset()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        hudManager.ChangeSettingsVisibility(false);
        hudManager.ChangeResetVisibility(true);
    }

    private void ResetData()
    {
        PreferencesManager.ResetData();
        SoundManager.GetInstance().PlaySFX("Teleport");
        hudManager.ChangeResetVisibility(false);
        hudManager.ChangeSettingsVisibility(true);
    }

    private void BackToSettings()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        hudManager.ChangeResetVisibility(false);
        hudManager.ChangeSettingsVisibility(true);
    }

    private void DisplayTutorial()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        TutorialController.OpenedFromMainMenu = true;
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

}
