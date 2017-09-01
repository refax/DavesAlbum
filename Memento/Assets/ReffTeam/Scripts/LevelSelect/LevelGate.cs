using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void OnGateTriggeredEvent(LevelGate i_Gate);

public class LevelGate : Gate
{

    public OnGateTriggeredEvent OnGateEnteredEvent;
    public OnGateTriggeredEvent OnGateExitedEvent;

    [SerializeField]
    private string m_LevelName;
    [SerializeField]
    private int m_LevelIndex;

    public void Enter()
    {
        if(m_LevelName != null && open)
        {
            SceneManager.LoadScene(m_LevelName, LoadSceneMode.Single);
        }
    }

    public int levelIndex
    {
        get
        {
            return m_LevelIndex;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
        if (player != null && open)
        {

            if (OnGateEnteredEvent != null)
            {
                OnGateEnteredEvent(this);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
        if (player != null && open)
        {
            if (OnGateExitedEvent != null)
            {
                OnGateExitedEvent(this);
            }
        }
    }

}
