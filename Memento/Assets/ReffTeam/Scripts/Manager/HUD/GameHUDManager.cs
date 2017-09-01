using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameHUDManager : HUDManager
{
    //Delegates

    public HUDEvent OnAlbumOpenEvent;
    public HUDEvent OnAlbumCloseEvent;

    public HUDEvent OnTravelEvent;
    public HUDEvent OnPolaroidEvent;

    public HUDEvent OnLeftPhotoEvent;
    public HUDEvent OnRightPhotoEvent;
    public HUDEvent OnPhotoEvent;

    public HUDEvent OnPauseEvent;

    public HUDEvent OnPlayEvent;
    public HUDEvent OnRestartEvent;
    public HUDEvent OnExitEvent;

    public HUDEvent OnCheckpointEvent;

    public HUDEvent OnStartLeftMovementEvent;
    public HUDEvent OnStopLeftMovementEvent;

    public HUDEvent OnStartRightMovementEvent;
    public HUDEvent OnStopRightMovementEvent;
    public HUDEvent OnJumpEvent;

    //Members

    private Button m_AlbumOpen;
    private Button m_AlbumClose;
    private CanvasGroup m_AlbumElements;
    private CanvasGroup m_PauseElements;

    private Image m_Photo;
    private Image m_PaperNumber;

    //Interface

    public void ChangePhotoImage(Sprite i_PhotoSprite)
    {
        if(i_PhotoSprite == null)
        {
            m_Photo.color = new Color(1, 1, 1, 0);
        }
        else
        {
            m_Photo.color = new Color(1, 1, 1, 1);
        }
        m_Photo.sprite = i_PhotoSprite;
    }

    public void ChangePaperLeft(Sprite i_PaperLeft)
    {
        if (m_PaperNumber != null && i_PaperLeft != null)
        {
            m_PaperNumber.sprite = i_PaperLeft;
        }
    }

    public void ChangeAlbumVisibility(bool i_Visibility)
    {
        if(m_AlbumElements != null)
        {
            ChangeObjectVisibility(m_AlbumElements.gameObject, i_Visibility);
            ChangeObjectVisibility(m_AlbumOpen.gameObject, i_Visibility);
            ChangeObjectVisibility(m_AlbumClose.gameObject, !i_Visibility);
        }
    }

    public void ChangePauseVisibility(bool i_Visibility)
    {
        ChangeObjectVisibility(m_PauseElements.gameObject, i_Visibility);
    }

    //Abstract Implementation

    protected override void RetrieveHUDObjects()
    {
        m_AlbumOpen = RetrieveHUDComponent<Button>("Elements/Album_Open");
        m_AlbumClose = RetrieveHUDComponent<Button>("Elements/Album_Close");
        m_AlbumElements = RetrieveHUDComponent<CanvasGroup>("Elements/Album_Elements");
        m_PauseElements = RetrieveHUDComponent<CanvasGroup>("Elements/Pause_Elements");
        m_Photo = RetrieveHUDComponent<Image>("Elements/Album_Elements/Photos/Photo/Image");
        m_PaperNumber = RetrieveHUDComponent<Image>("Elements/Paper_Left/Paper_Number");

        HoldingButton m_LeftMovementButton = RetrieveHUDComponent<HoldingButton>("Elements/Mobile/Movement/Left");
        HoldingButton m_RightMovementButton = RetrieveHUDComponent<HoldingButton>("Elements/Mobile/Movement/Right");
        HoldingButton m_Jump = RetrieveHUDComponent<HoldingButton>("Elements/Mobile/Movement/Jump");

        Button m_Pause = RetrieveHUDComponent<Button>("Elements/Pause");
        Button m_Checkpoint = RetrieveHUDComponent<Button>("Elements/Checkpoint");

        Transform m_MobileCanvas = RetrieveHUDComponent<Transform>("Elements/Mobile");

        #if (UNITY_IOS || UNITY_ANDROID)

                m_MobileCanvas.gameObject.SetActive(true);

                m_Pause.gameObject.SetActive(false);
                m_Checkpoint.gameObject.SetActive(false);

                m_LeftMovementButton.OnButtonDown += OnStartLeftMovement;
                m_LeftMovementButton.OnButtonUp += OnStoptLeftMovement;
                m_RightMovementButton.OnButtonDown += OnStartRightMovement;
                m_RightMovementButton.OnButtonUp += OnStopRightMovement;
                m_Jump.OnButtonDown += OnJumpClicked;
        #else

                m_MobileCanvas.gameObject.SetActive(false);

                m_Pause.gameObject.SetActive(true);
                m_Checkpoint.gameObject.SetActive(true);
        #endif
    }

    protected override List<ListenedButton> GetListenedButtons()
    {
        List<ListenedButton> listenedButtons = new List<ListenedButton>();

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Album_Open"), OnAlbumOpenClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Album_Close"), OnAlbumCloseClicked));

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Album_Elements/Travel"), OnTravelClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Album_Elements/Polaroid"), OnPolaroidClicked));

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Album_Elements/Photos/Left"), OnLeftPhotoClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Album_Elements/Photos/Right"), OnRightPhotoClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Album_Elements/Photos/Photo"), OnPhotoClicked));

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Pause"), OnPauseClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Mobile/Pause"), OnPauseClicked));

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Pause_Elements/Play"), OnPlayClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Pause_Elements/Restart"), OnRestartClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Pause_Elements/Exit"), OnExitClicked));

        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Checkpoint"), OnCheckpointClicked));
        listenedButtons.Add(new ListenedButton(RetrieveHUDComponent<Button>("Elements/Mobile/Checkpoint"), OnCheckpointClicked));

        return listenedButtons;
    }

    // Event

    private void OnAlbumOpenClicked()
    {
        BroadCastHUDEvent(OnAlbumOpenEvent);
    }

    private void OnAlbumCloseClicked()
    {
        BroadCastHUDEvent(OnAlbumCloseEvent);
    }

    private void OnTravelClicked()
    {
        BroadCastHUDEvent(OnTravelEvent);
    }

    private void OnPolaroidClicked()
    {
        BroadCastHUDEvent(OnPolaroidEvent);
    }

    private void OnLeftPhotoClicked()
    {
        BroadCastHUDEvent(OnLeftPhotoEvent);
    }

    private void OnRightPhotoClicked()
    {
        BroadCastHUDEvent(OnRightPhotoEvent);
    }

    private void OnPhotoClicked()
    {
        BroadCastHUDEvent(OnPhotoEvent);
    }

    private void OnPauseClicked()
    {
        BroadCastHUDEvent(OnPauseEvent);
    }

    private void OnPlayClicked()
    {
        BroadCastHUDEvent(OnPlayEvent);
    }

    private void OnRestartClicked()
    {
        BroadCastHUDEvent(OnRestartEvent);
    }

    private void OnExitClicked()
    {
        BroadCastHUDEvent(OnExitEvent);
    }

    private void OnCheckpointClicked()
    {
        BroadCastHUDEvent(OnCheckpointEvent);
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
