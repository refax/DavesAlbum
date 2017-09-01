using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : SceneController<GameHUDManager>
{
    [SerializeField]
    private int m_LevelIndex;

    [SerializeField]
    private GameObject m_OldLevel;
    [SerializeField]
    private GameObject m_YoungLevel;

    private PlayerPlatformerController m_OldPlayer; 
    private PlayerPlatformerController m_YoungPlayer;

    private ParalaxEffect m_OldLevelBackground;
    private ParalaxEffect m_YoungLevelBackground;

    [SerializeField]
    private int m_InitialPaperNumber;

    [SerializeField]
    private WinGate m_WinGate;

    [SerializeField]
    private Checkpoint[] m_CheckpointsOld;
    [SerializeField]
    private Checkpoint[] m_CheckpointsYoung;

    [SerializeField]
    private Checkpoint m_OldCheckpointOpen;
    [SerializeField]
    private Checkpoint m_YoungCheckpointOpen;

    [SerializeField]
    private Sprite[] m_Numbers;

    private PhotosManager m_PhotosManager;
    private PolaroidManager m_PolaroidManager;
    private TimeTravelManager m_TimeTravelManager;
    private PhotographablesManager m_PhotographablesManager;
    private CheckpointsManager m_CheckpointsManager;


    public new void Awake()
    {
        base.Awake();

        GlobalDataManager.LastLevelPlayed = m_LevelIndex;

        m_OldPlayer = m_OldLevel.GetComponentInChildren<PlayerPlatformerController>(true);
        m_YoungPlayer = m_YoungLevel.GetComponentInChildren<PlayerPlatformerController>(true);

        m_OldLevelBackground = m_OldLevel.GetComponentInChildren<ParalaxEffect>();
        m_YoungLevelBackground = m_YoungLevel.GetComponentInChildren<ParalaxEffect>();

        if (m_OldPlayer == null)
        {
            Debug.LogError("OldPlayer in GameManager is null");
        }
        else
        {
            m_OldPlayer.OnPlayerDeath += OnPlayerDeathHandler;
        }

        if (m_YoungPlayer == null)
        {
            Debug.LogError("YoungPlayer in GameManager is null");
        }
        else
        {
            m_YoungPlayer.OnPlayerDeath += OnPlayerDeathHandler;
        }

        if(m_OldLevelBackground == null)
        {
            Debug.LogError("Background script missin in OldLevel");
        }

        if (m_YoungLevelBackground == null)
        {
            Debug.LogError("Background script missin in Younglevel");
        }

        if (m_WinGate != null)
        {
            m_WinGate.OnWinEvent += LevelWon;
        }

        CreateManager();
    }    

    private void Start()
    {
        InitializeManager();

        if (m_OldLevel != null)
        {
            m_OldLevel.gameObject.SetActive(true);
        }
        if (m_YoungLevel != null)
        {
            m_YoungLevel.gameObject.SetActive(false);
        }

        UpdatePhotoImage(m_PhotosManager.GetCurrentSelectedPhoto());
        UpdatePaper();
        m_CheckpointsManager.RespawnOld(m_OldPlayer);
        m_CheckpointsManager.RespawnYoung(m_YoungPlayer);

    }
    

    //Initialization

    private void CreateManager()
    {
        m_PhotosManager = gameObject.AddComponent<PhotosManager>();

        m_PolaroidManager = gameObject.AddComponent<PolaroidManager>();

        m_TimeTravelManager = gameObject.AddComponent<TimeTravelManager>();

        m_PhotographablesManager = gameObject.AddComponent<PhotographablesManager>();

        m_CheckpointsManager = gameObject.AddComponent<CheckpointsManager>();
    }

    private void InitializeManager()
    {

        m_PolaroidManager.Initialize(m_OldPlayer.gameObject, m_YoungPlayer.gameObject, m_InitialPaperNumber);
        m_PolaroidManager.OnPaperIncreasedEvent += UpdatePaper;

        m_PhotographablesManager.Initialize(m_OldPlayer.gameObject, m_OldLevel);

        m_TimeTravelManager.Initialize(m_OldLevel, m_YoungLevel);

        m_CheckpointsManager.Initialize(m_CheckpointsOld, m_CheckpointsYoung, m_OldCheckpointOpen, m_YoungCheckpointOpen);

    }

    // Abstract Implementation

    protected override void HUDInitialization()
    {
        hudManager.ChangeAlbumVisibility(true);
        hudManager.ChangePauseVisibility(false);
    }

    protected override void RegisterToHUDDelegate()
    {
        hudManager.OnAlbumOpenEvent += CloseAlbum;
        hudManager.OnAlbumCloseEvent += OpenAlbum;

        hudManager.OnTravelEvent += TimeTravel;
        hudManager.OnPolaroidEvent += UsePolaroid;

        hudManager.OnLeftPhotoEvent += PreviousPhoto;
        hudManager.OnRightPhotoEvent += NextPhoto;
        hudManager.OnPhotoEvent += UsePhoto;

        hudManager.OnPauseEvent += ShowPause;

        hudManager.OnPlayEvent += HidePause;
        hudManager.OnRestartEvent += RestartLevel;
        hudManager.OnExitEvent += ExitLevel;

        hudManager.OnCheckpointEvent += RespawnPlayer;

        hudManager.OnStartLeftMovementEvent += StartForcePlayerLeftMovement;
        hudManager.OnStopLeftMovementEvent += StopForcePlayerLeftMovement;
        hudManager.OnStartRightMovementEvent += StartForcePlayerRightMovement;
        hudManager.OnStopRightMovementEvent += StopForcePlayerRightMovement;
        hudManager.OnJumpEvent += ForcePlayerJump;

    }

    //Method

    public void LevelWon()
    {
        PreferencesManager.LevelCompleted(m_LevelIndex);
        if(m_LevelIndex == GlobalDataManager.MaxLevelIndex)
        {
            SceneManager.LoadScene("The_End", LoadSceneMode.Single);
        }else
        {
            SoundManager.GetInstance().PlaySFX("Victory");
            SceneManager.LoadScene("Level_Select", LoadSceneMode.Single);
        }
    }

    //Event

    private void OpenAlbum()
    {
        SoundManager.GetInstance().PlaySFX("Album");
        hudManager.ChangeAlbumVisibility(true);
    }
    private void CloseAlbum()
    {
        SoundManager.GetInstance().PlaySFX("Album");
        hudManager.ChangeAlbumVisibility(false);
    }

    private void TimeTravel()
    {
        SoundManager.GetInstance().PlaySFX("Switch");
        m_TimeTravelManager.TimeTravel();
    }

    /**
     * This function take a photo if scene is in 'Young mode' and 
     * retrieves a photo by deleteing the corresponding spawned object if
     * the scene is in 'Old Mode'
     */
    private void UsePolaroid()
    {
        if (m_TimeTravelManager.IsFuture())
        {
            Photographable l_Photographable = m_PolaroidManager.RetrievePhoto();
            if (l_Photographable != null)
            {
                SoundManager.GetInstance().PlaySFX("Polaroid");
                Photographable l_RetrievedPhoto = m_PhotosManager.SetBusyPhotoToAvailable(l_Photographable);
                UpdatePhotoImage(l_RetrievedPhoto);
                m_PhotographablesManager.DeletePhotographable(l_Photographable);
            }else
            {
                SoundManager.GetInstance().PlaySFX("Error");
            }
        }
        else
        {
            Photographable l_Photographable = m_PolaroidManager.TakePhoto();
            if (l_Photographable != null)
            {
                SoundManager.GetInstance().PlaySFX("Polaroid");
                m_PhotosManager.TakePhoto(l_Photographable);
                UpdatePhotoImage(l_Photographable);
                UpdatePaper();
            }else
            {
                SoundManager.GetInstance().PlaySFX("Error");
            }
        }
    }

    private void PreviousPhoto()
    {
        Photographable l_Photographable = m_PhotosManager.SelectedPreviousPhoto();
        if(l_Photographable != null)
        {
            SoundManager.GetInstance().PlaySFX("Change_Photo");
            UpdatePhotoImage(l_Photographable);
        }
        else
        {
            SoundManager.GetInstance().PlaySFX("Error");
        }

    }

    private void NextPhoto()
    {
        Photographable l_Photographable = m_PhotosManager.SelectedNextPhoto();
        if (l_Photographable != null)
        {
            SoundManager.GetInstance().PlaySFX("Change_Photo");
            UpdatePhotoImage(l_Photographable);
        }
        else
        {
            SoundManager.GetInstance().PlaySFX("Error");
        }
    }

    /**
     * This function spawn an object related to the current selected photo if scene is in
     * 'Old mode' or delete the currente selected photo in the scene is in 'Young mode'
     */
    private void UsePhoto()
    {
        if (m_TimeTravelManager.IsFuture())
        {
            if (m_OldLevel != null && m_OldPlayer != null)
            {
                Photographable l_CurrentSelectedElement = m_PhotosManager.GetCurrentSelectedPhoto();
                if (m_PhotographablesManager.SpawnPhotographable(l_CurrentSelectedElement))
                {
                    SoundManager.GetInstance().PlaySFX("Click_Photo");
                    m_PhotosManager.SetAvailablePhotoToBusy(l_CurrentSelectedElement);
                    Photographable l_NewCurrentPhotographable = m_PhotosManager.GetCurrentSelectedPhoto();
                    UpdatePhotoImage(l_NewCurrentPhotographable);
                }
                else if(l_CurrentSelectedElement != null)
                {
                    SoundManager.GetInstance().PlaySFX("Error");
                }
            }
        }
        else
        {
            Photographable l_RemovePhoto = m_PhotosManager.RemovePhoto();
            if(l_RemovePhoto != null)
            {
                SoundManager.GetInstance().PlaySFX("Click_Photo");
                m_PolaroidManager.ReleasePhoto(l_RemovePhoto);

                UpdatePhotoImage(m_PhotosManager.GetCurrentSelectedPhoto());
                UpdatePaper();
            }
        }
    }

    private void UpdatePaper()
    {
        hudManager.ChangePaperLeft(m_Numbers[m_PolaroidManager.GetPaperNumber()]);
    }


    private void ShowPause()
    {
        Time.timeScale = 0.0f;
        SoundManager.GetInstance().PlaySFX("Button");
        hudManager.ChangePauseVisibility(true);
    }

    private void HidePause()
    {
        Time.timeScale = 1.0f;
        SoundManager.GetInstance().PlaySFX("Button");
        hudManager.ChangePauseVisibility(false);
    }

    private void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SoundManager.GetInstance().PlaySFX("Teleport");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void ExitLevel()
    {
        Time.timeScale = 1.0f;
        SoundManager.GetInstance().PlaySFX("Button");
        SceneManager.LoadScene("Level_Select", LoadSceneMode.Single);
    }

    private void RespawnPlayer()
    {
        SoundManager.GetInstance().PlaySFX("Teleport");
        if (m_TimeTravelManager.IsFuture())
        {
            m_CheckpointsManager.RespawnOld(m_OldPlayer);
        }
        else
        {
            m_CheckpointsManager.RespawnYoung(m_YoungPlayer);
        }
    }

    //HUD Utilities

    private void UpdatePhotoImage(Photographable i_Photographable)
    {
        Sprite l_Sprite = null;
        if (i_Photographable != null)
        {
            SpriteRenderer l_PhotographableSpriteRender = i_Photographable.gameObject.GetComponent<SpriteRenderer>();
            if (l_PhotographableSpriteRender != null)
            {
                l_Sprite = l_PhotographableSpriteRender.sprite;
            }
        }

        if(hudManager != null)
        {
            hudManager.ChangePhotoImage(l_Sprite);
        }
    }


    public void OnPlayerDeathHandler(PlayerPlatformerController player)
    {
        SoundManager.GetInstance().PlaySFX("Death");
        if (player == m_OldPlayer)
        {
            m_CheckpointsManager.RespawnOld(player);
            m_OldLevelBackground.ResetBackground();
        }
        else
        {
            m_CheckpointsManager.RespawnYoung(player);
            m_YoungLevelBackground.ResetBackground();
        }        
    }




    private PlayerPlatformerController GetCurrentActivetPlayer()
    {
        if(m_OldPlayer.gameObject.activeInHierarchy)
        {
            return m_OldPlayer;
        }
        else
        {
            return m_YoungPlayer;
        }
    }

    public void StartForcePlayerLeftMovement()
    {
        GetCurrentActivetPlayer().ForceLeftMovement = true;
    }

    public void StopForcePlayerLeftMovement()
    {
        GetCurrentActivetPlayer().ForceLeftMovement = false;
    }

    public void StartForcePlayerRightMovement()
    {
        GetCurrentActivetPlayer().ForceRightMovement = true;
    }

    public void StopForcePlayerRightMovement()
    {
        GetCurrentActivetPlayer().ForceRightMovement = false;
    }

    public void ForcePlayerJump()
    {
        GetCurrentActivetPlayer().ForceJump = true;
    }


}
