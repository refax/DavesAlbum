using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public delegate void HoldingButtonEvent();

[RequireComponent(typeof(EventTrigger))]
public class HoldingButton : MonoBehaviour {

    public event HoldingButtonEvent OnButtonDown;
    public event HoldingButtonEvent OnButtonUp;


    public void OnPointerDown()
    {
        if(OnButtonDown != null)
        {
            OnButtonDown();
        }
    }
    public void OnPointerUp()
    {
        if(OnButtonUp != null)
        {
            OnButtonUp();
        }
    }
}
