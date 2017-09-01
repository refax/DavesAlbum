using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSetup : MonoBehaviour {
    
    [System.Serializable]
    public class StringToAudioClip
    {
        public string m_AudioClipName;
        public AudioClip m_AudioClip;

        [Range(0.0f, 1.0f)]
        public float m_Volume = 1.0f;
    }

    [SerializeField]
    private int m_MaxLevel;

    [SerializeField]
    private string m_FirstSceneName;

    [SerializeField]
    private StringToAudioClip[] m_Musics;

    [SerializeField]
    private StringToAudioClip[] m_SFXs;

    private void Start()
    {
        Setup();
        SceneManager.LoadScene(m_FirstSceneName, LoadSceneMode.Single);
    }

    private void Setup()
    {

        #if UNITY_ANDROID || UNITY_IOS
                Application.targetFrameRate = 30;
        #endif

        GlobalDataManager.MaxLevelIndex = m_MaxLevel;

        if (PreferencesManager.IsMusicEnabled())
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }

        InitializeSoundManager();
    }

    private void InitializeSoundManager()
    {
        Dictionary<string, DetailedAudioClip> musics = new Dictionary<string, DetailedAudioClip>();
        Dictionary<string, DetailedAudioClip> sfxs = new Dictionary<string, DetailedAudioClip>();

        foreach (StringToAudioClip toAdd in m_Musics)
        {
            musics.Add(toAdd.m_AudioClipName, new DetailedAudioClip(toAdd.m_AudioClip, toAdd.m_Volume));
        }
        foreach (StringToAudioClip toAdd in m_SFXs)
        {
            sfxs.Add(toAdd.m_AudioClipName, new DetailedAudioClip(toAdd.m_AudioClip, toAdd.m_Volume));
        }

        SoundManager soundManager = SoundManager.GetInstance();
        soundManager.Initialize(musics, sfxs);
    }
}
