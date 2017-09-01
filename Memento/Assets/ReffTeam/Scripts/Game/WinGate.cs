using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void WinEvent();

public class WinGate : Gate
{

    public WinEvent OnWinEvent;

    private void OnTriggerEnter2D(Collider2D i_Collision)
    {
        if (!open)
        {
            GameObject key = i_Collision.gameObject;

            if (key.GetComponent<Key>() != null)
            {
                key.SetActive(false);
                SetGateState(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D i_Collision)
    {
        if (open)
        {
            if (i_Collision.gameObject.GetComponent<PlayerPlatformerController>() != null)
            {
                if (OnWinEvent != null)
                {
                    OnWinEvent();
                }
            }
        }
    }

}