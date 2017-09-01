using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuHUDManager : HUDManager
{
    //Delegates

    public HUDEvent OnPlayEvent;
    public HUDEvent OnSettingsEvent;
    public HUDEvent OnCreditsEvent;
    public HUDEvent OnTutorialEvent;
    public HUDEvent OnExitEvent;

    public HUDEvent OnEnableMusicEvent;
    public HUDEvent OnDisableMusicEvent;
    public HUDEvent OnResetEvent;
    public HUDEvent OnBackFromSettingsEvent;

    public HUDEvent OnBackFromCreditsEvent;

    public HUDEvent OnResetYesEvent;
    public HUDEvent OnResetNoEvent;

    //Members

    private CanvasGroup m_MainElements;
    private CanvasGroup m_CreditsElements;
    private CanvasGroup m_SettingsElements;
    private CanvasGroup m_ResetElements;
    private Button m_EnableMusic;
    private Button m_DisableMusic;

    //Interface

    public void ChangeMainVisibility(bool i_Visibility)
    {
        ChangeObjectVisibility(m_MainElements.gameObject, i_Visibility);
    }

    public void ChangeSettingsVisibility(bool i_Visibility)
    {
        ChangeObjectVisibility(m_SettingsElements.gameObject, i_Visibility);
        ChangeObjectVisibility(m_MainElements.gameObject, !i_Visibility);
    }

    public void ChangeCreditsVisibility(bool i_Visibility)
    {
        ChangeObjectVisibility(m_CreditsElements.gameObject, i_Visibility);
        ChangeObjectVisibility(m_MainElements.gameObject, !i_Visibility);
    }

    public void ChangeResetVisibility(bool i_Visibility)
    {
        ChangeObjectVisibility(m_ResetElements.gameObject, i_Visibility);
        ChangeObjectVisibility(m_MainElements.gameObject, !i_Visibility);
    }

    public void ChangeEnableMusicVisibility(bool i_Visibility)
    {
        ChangeObjectVisibility(m_EnableMusic.gameObject, i_Visibility);
        ChangeObjectVisibility(m_DisableMusic.gameObject, !i_Visibility);
    }

    public void DisableExit()
    {
        Button exit = RetrieveHUDComponent<Button>("Elements/Main_Elements/Exit");
        if(exit != null)
        {
            exit.gameObject.SetActive(false);
        }
    }

    //Abstract Implementation

    protected override void RetrieveHUDObjects()
    {
        m_MainElements = RetrieveHUDComponent<CanvasGroup>("Elements/Main_Elements");
        m_CreditsElements = RetrieveHUDComponent<CanvasGroup>("Elements/Credits_Elements");
        m_SettingsElements = RetrieveHUDComponent<CanvasGroup>("Elements/Settings_Elements");
        m_ResetElements = RetrieveHUDComponent<CanvasGroup>("Elements/Reset_Elements");
        m_EnableMusic = RetrieveHUDComponent<Button>("Elements/Settings_Elements/Enable_Music");
        m_DisableMusic = RetrieveHUDComponent<Button>("Elements/Settings_Elements/Disable_Music");
    }

    protected override List<ListenedButton> GetListenedButtons()
    {
        List<ListenedButton> listenedButtons = new List<ListenedButton>();

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Main_Elements/Play"), OnPlayClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Main_Elements/Settings"), OnSettingsClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Main_Elements/Credits"), OnCreditsClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Main_Elements/Tutorial"), OnTutorialClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Main_Elements/Exit"), OnExitClicked));

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Settings_Elements/Enable_Music"), OnEnableMusicClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Settings_Elements/Disable_Music"), OnDisableMusicClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Settings_Elements/Reset"), OnResetClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Settings_Elements/Back"), OnBackFromSettingsClicked));

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Credits_Elements/Back"), OnBackFromCreditsClicked));

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Reset_Elements/Yes"), OnResetYesClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Reset_Elements/No"), OnResetNoClicked));

        return listenedButtons;
    }

    //Event

    private void OnPlayClicked()
    {
        BroadCastHUDEvent(OnPlayEvent);
    }

    private void OnSettingsClicked()
    {
        BroadCastHUDEvent(OnSettingsEvent);
    }

    private void OnCreditsClicked()
    {
        BroadCastHUDEvent(OnCreditsEvent);
    }

    private void OnExitClicked()
    {
        BroadCastHUDEvent(OnExitEvent);
    }

    private void OnEnableMusicClicked()
    {
        BroadCastHUDEvent(OnEnableMusicEvent);
    }

    private void OnDisableMusicClicked()
    {
        BroadCastHUDEvent(OnDisableMusicEvent);
    }

    private void OnBackFromSettingsClicked()
    {
        BroadCastHUDEvent(OnBackFromSettingsEvent);
    }

    private void OnBackFromCreditsClicked()
    {
        BroadCastHUDEvent(OnBackFromCreditsEvent);
    }

    private void OnResetClicked()
    {
        BroadCastHUDEvent(OnResetEvent);
    }

    private void OnResetYesClicked()
    {
        BroadCastHUDEvent(OnResetYesEvent);
    }
    
    private void OnResetNoClicked()
    {
        BroadCastHUDEvent(OnResetNoEvent);
    }
    private void OnTutorialClicked()
    {
        BroadCastHUDEvent(OnTutorialEvent);
    }
}
