using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneController<T> : MonoBehaviour where T : HUDManager {

    [SerializeField]
    private Canvas m_HUDPrefab;
    private T m_HUDManager;

    public void Awake()
    {
        CreateHUD();
    }

    private void CreateHUD()
    {
        m_HUDManager = this.gameObject.AddComponent<T>();
        if (m_HUDManager != null)
        {
            m_HUDManager.Initialize(m_HUDPrefab);
            HUDInitialization();
            RegisterToHUDDelegate();
        }
    }

    protected abstract void HUDInitialization();

    protected abstract void RegisterToHUDDelegate();

    protected T hudManager
    {
        get
        { 
            return m_HUDManager;
        }
    }

}
