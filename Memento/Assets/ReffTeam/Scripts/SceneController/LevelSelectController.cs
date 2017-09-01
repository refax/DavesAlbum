using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : SceneController<LevelSelectHUDManager>
{
    [SerializeField]
    private GameObject m_OldPlayer;
    
    private LevelGate m_LastGateTriggered;

    private void Start()
    {
        int maxLevelCompleted = PreferencesManager.GetMaxLevelCompleted();

        LevelGate[] gates = FindObjectsOfType<LevelGate>();

        if (gates != null)
        {
            OpenGate(gates, maxLevelCompleted);
            PlacePlayer(gates, maxLevelCompleted);
        }

        HideWalls(maxLevelCompleted);

    }

    private void OpenGate(LevelGate[] i_Gates, int i_MaxLevelCompleted)
    {

        if (i_MaxLevelCompleted == GlobalDataManager.NO_LEVEL_PLAYED)
        {
            i_MaxLevelCompleted = 0;
        }

        foreach (LevelGate gate in i_Gates)
        {
            if (gate.levelIndex <= i_MaxLevelCompleted + 1)
            {
                gate.SetGateState(true);
                gate.OnGateEnteredEvent = GateEntered;
                gate.OnGateExitedEvent = GateExited;
            }
            else
            {
                gate.SetGateState(false);
            }
        }
    }

    private void PlacePlayer(LevelGate[] i_Gates, int i_MaxLevelCompleted)
    {
        int levelToPosition = i_MaxLevelCompleted;

        if (GlobalDataManager.LastLevelPlayed != GlobalDataManager.NO_LEVEL_PLAYED)
        {
            levelToPosition = GlobalDataManager.LastLevelPlayed;
        }

        foreach (LevelGate gate in i_Gates)
        {
            if (gate.levelIndex == levelToPosition)
            {
                m_OldPlayer.gameObject.transform.position = new Vector3(gate.gameObject.transform.position.x, gate.gameObject.transform.position.y + 0.17f, 0);
                break;
            }
        }
    }

    private void HideWalls(int i_MaxLevelCompleted)
    {
        DisappearingWall[] walls = FindObjectsOfType<DisappearingWall>();
        if (walls != null)
        {
            foreach (DisappearingWall wall in walls)
            {
                if (wall.ShouldDisapper(i_MaxLevelCompleted))
                {
                    wall.gameObject.SetActive(false);
                }
                else
                {
                    wall.gameObject.SetActive(true);
                }
            }
        }
    }

    // Abstract Implementation

    protected override void HUDInitialization()
    {
        hudManager.ChangeEnterVisibility(false);
    }

    protected override void RegisterToHUDDelegate()
    {
        hudManager.OnEnterEvent += EnterLevel;
        hudManager.OnBackEvent += BackToMain;

        hudManager.OnStartLeftMovementEvent += StartForcePlayerLeftMovement;
        hudManager.OnStopLeftMovementEvent += StopForcePlayerLeftMovement;
        hudManager.OnStartRightMovementEvent += StartForcePlayerRightMovement;
        hudManager.OnStopRightMovementEvent += StopForcePlayerRightMovement;
        hudManager.OnJumpEvent += ForcePlayerJump;
    }

    //Event

    private void GateEntered(LevelGate i_Gate)
    {
        hudManager.ChangeEnterVisibility(true);
        m_LastGateTriggered = i_Gate;

    }

    private void GateExited(LevelGate i_Gate)
    {
        hudManager.ChangeEnterVisibility(false);
        m_LastGateTriggered = null;

    }

    private void EnterLevel()
    {
        SoundManager.GetInstance().PlaySFX("Teleport");
        if (m_LastGateTriggered != null)
        {
            m_LastGateTriggered.Enter();
        }
    }

    private void BackToMain()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }

    private PlayerPlatformerController GetCurrentActivetPlayer()
    {
        return m_OldPlayer.GetComponent<PlayerPlatformerController>();
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
