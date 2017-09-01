using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public delegate void HUDEvent();

public abstract class HUDManager : MonoBehaviour
{
    protected struct ListenedButton
    {
        public Button m_Button;
        public UnityAction m_Listener;

        public ListenedButton(Button i_Button, UnityAction i_Listener)
        {
            m_Button = i_Button;
            m_Listener = i_Listener;
        }

    }
    
    [SerializeField]
    private Canvas m_HUDPrefab;
    private GameObject m_HUD;

    private List<ListenedButton> m_ListenedButtons;

    private bool m_Initialized = false;
    private bool m_Listening = false;

    public void Awake()
    {
        Initialize(m_HUDPrefab);
    }

    public void OnEnable()
    {
        if (m_HUD != null)
        {
            AddListeners();
        }
    }

    public void OnDisable()
    {
        if (m_HUD != null)
        {
            RemoveListeners();
        }
    }

    public void OnDestroy()
    {
        GameObject.Destroy(m_HUD);
    }

    //Initialization

    public void Initialize(Canvas i_HUDPrefab)
    {
        if (m_Initialized)
        {
            return;
        }

        if (i_HUDPrefab != null)
        {
            m_HUD = GameObject.Instantiate(i_HUDPrefab.gameObject, transform);
            m_HUD.name = "HUD";

            if (m_HUD != null)
            {
                RetrieveHUDObjects();
                m_ListenedButtons = GetListenedButtons();
                AddListeners();

                m_Initialized = true;
            }
        }
    }

    //Abstract

    protected abstract void RetrieveHUDObjects();

    protected abstract List<ListenedButton> GetListenedButtons();

    //Protected Utilities

    protected void AddOnClickListener(string i_ButtonName, UnityAction i_Listener)
    {
        Button button = RetrieveHUDComponent<Button>(i_ButtonName);
        if (button != null)
        {
            button.onClick.AddListener(i_Listener);
        }
    }

    protected void RemoveOnClickListener(string i_ButtonName, UnityAction i_Listener)
    { 
        Button button = RetrieveHUDComponent<Button>(i_ButtonName);
        if (button != null)
        {
            button.onClick.RemoveListener(i_Listener);
        }
    }

    protected ComponentType RetrieveHUDComponent<ComponentType>(string i_ComponentName)
    {
        Transform componentTransform = m_HUD.transform.Find(i_ComponentName);
        if (componentTransform != null)
        {
            return componentTransform.gameObject.GetComponent<ComponentType>();
        }

        return default(ComponentType);
    }

    protected GameObject getHUD()
    {
        return m_HUD;
    }

    //Static Utilities

    static protected void ChangeObjectVisibility(GameObject i_ToChange, bool i_Visibility)
    {
        if (i_ToChange != null)
        {
            i_ToChange.SetActive(i_Visibility);
        }
    }

    static protected void BroadCastHUDEvent(HUDEvent i_HUDEvent)
    {
        if (i_HUDEvent != null)
        {
            i_HUDEvent();
        }
    }

    //Private Utilities

    private void AddListeners()
    {
        if (!m_Listening)
        {
            foreach (ListenedButton listenedButton in m_ListenedButtons)
            {
                if (listenedButton.m_Button != null)
                {
                    listenedButton.m_Button.onClick.AddListener(listenedButton.m_Listener);
                }
            }

            m_Listening = true;
        }
    }

    private void RemoveListeners()
    {
        if (m_Listening)
        {
            foreach (ListenedButton listenedButton in m_ListenedButtons)
            {
                if (listenedButton.m_Button != null)
                {
                    listenedButton.m_Button.onClick.RemoveListener(listenedButton.m_Listener);
                }
            }

            m_Listening = false;
        }
    }
}
