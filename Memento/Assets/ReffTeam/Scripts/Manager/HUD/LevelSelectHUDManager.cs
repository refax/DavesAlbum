using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectHUDManager : HUDManager
{
    //Delegates

    public HUDEvent OnEnterEvent;
    public HUDEvent OnBackEvent;

    public HUDEvent OnStartLeftMovementEvent;
    public HUDEvent OnStopLeftMovementEvent;

    public HUDEvent OnStartRightMovementEvent;
    public HUDEvent OnStopRightMovementEvent;
    public HUDEvent OnJumpEvent;

    //Members

    private Button m_Enter;
    private Button m_MobileEnter;

    //Interface

    public void ChangeEnterVisibility(bool i_Visibility)
    {
        #if (UNITY_IOS || UNITY_ANDROID)
            ChangeObjectVisibility(m_MobileEnter.gameObject, i_Visibility);
        #else
            ChangeObjectVisibility(m_Enter.gameObject, i_Visibility);
        #endif
    }

    //Abstract Implementation

    protected override void RetrieveHUDObjects()
    {
        m_Enter = RetrieveHUDComponent<Button>("Elements/Enter");
        m_MobileEnter = RetrieveHUDComponent<Button>("Elements/Mobile/Enter");

        HoldingButton m_LeftMovementButton = RetrieveHUDComponent<HoldingButton>("Elements/Mobile/Movement/Left");
        HoldingButton m_RightMovementButton = RetrieveHUDComponent<HoldingButton>("Elements/Mobile/Movement/Right");
        HoldingButton m_Jump = RetrieveHUDComponent<HoldingButton>("Elements/Mobile/Movement/Jump");

        Transform m_MobileCanvas = RetrieveHUDComponent<Transform>("Elements/Mobile");

        #if (UNITY_IOS || UNITY_ANDROID)
                m_MobileCanvas.gameObject.SetActive(true);
        
                m_Enter.gameObject.SetActive(false);

                m_LeftMovementButton.OnButtonDown += OnStartLeftMovement;
                m_LeftMovementButton.OnButtonUp += OnStoptLeftMovement;
                m_RightMovementButton.OnButtonDown += OnStartRightMovement;
                m_RightMovementButton.OnButtonUp += OnStopRightMovement;
                m_Jump.OnButtonDown += OnJumpClicked;
        #else        
                m_MobileCanvas.gameObject.SetActive(false);
                m_Enter.gameObject.SetActive(true);
        #endif
    }

    protected override List<ListenedButton> GetListenedButtons()
    {
        List<ListenedButton> listenedButtons = new List<ListenedButton>();

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Enter"), OnEnterClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Mobile/Enter"), OnEnterClicked));

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Back"), OnBackClicked));

        return listenedButtons;
    }

    // Event

    private void OnEnterClicked()
    {
        BroadCastHUDEvent(OnEnterEvent);
    }

    private void OnBackClicked()
    {
        BroadCastHUDEvent(OnBackEvent);
    }


    private void OnStartLeftMovement()
    {
        BroadCastHUDEvent(OnStartLeftMovementEvent);
    }

    private void OnStoptLeftMovement()
    {
        BroadCastHUDEvent(OnStopLeftMovementEvent);
    }

    private void OnStartRightMovement()
    {
        BroadCastHUDEvent(OnStartRightMovementEvent);
    }

    private void OnStopRightMovement()
    {
        BroadCastHUDEvent(OnStopRightMovementEvent);
    }

    private void OnJumpClicked()
    {
        BroadCastHUDEvent(OnJumpEvent);
    }
}
