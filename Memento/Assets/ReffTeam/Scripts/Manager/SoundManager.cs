using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager {

    private const int MAX_SFX_AUDIO_SOURCES = 10;

    private static SoundManager m_Instance = null;

    private GameObject m_ParentObject;

    private AudioSource m_MusicAudioSource;
    private AudioSource[] m_SFXAudioSources;

    private Dictionary<string, DetailedAudioClip> m_Musics;
    private Dictionary<string, DetailedAudioClip> m_SFXs;

    private SoundManager()
    {
        m_ParentObject = new GameObject("SoundManager");
        Object.DontDestroyOnLoad(m_ParentObject);

        m_MusicAudioSource = m_ParentObject.AddComponent<AudioSource>();
        m_MusicAudioSource.loop = true;

        m_SFXAudioSources = new AudioSource[MAX_SFX_AUDIO_SOURCES];
        for (int i=0; i< MAX_SFX_AUDIO_SOURCES; ++i)
        {
            m_SFXAudioSources[i] = m_ParentObject.AddComponent<AudioSource>();
            m_SFXAudioSources[i].loop = false;
        }

        m_Musics = new Dictionary<string, DetailedAudioClip>();
        m_SFXs = new Dictionary<string, DetailedAudioClip>();
    }

    public void Initialize(Dictionary<string, DetailedAudioClip> i_Musics, Dictionary<string, DetailedAudioClip> i_SFXs)
    {
        m_Musics = i_Musics;
        m_SFXs = i_SFXs;
    }

    public void PlayMusic(string i_MusicName, bool i_Restart)
    {
        DetailedAudioClip music = null;
        if(m_Musics.TryGetValue(i_MusicName, out music))
        {
            if(i_Restart || (m_MusicAudioSource.clip != music.audioClip || m_MusicAudioSource.volume != music.volume))
            {
                m_MusicAudioSource.Stop();
                m_MusicAudioSource.clip = music.audioClip;
                m_MusicAudioSource.volume = music.volume;
                m_MusicAudioSource.Play();
            }
        }
    }

    public void StopMusic()
    {
        m_MusicAudioSource.Stop();
    }

    public void PlaySFX(string i_SFXName)
    {
        DetailedAudioClip sfx = null;
        if (m_SFXs.TryGetValue(i_SFXName, out sfx))
        {
            AudioSource sfxAudioSource = GetAvailableSfxAudioSource();
            sfxAudioSource.Stop();
            sfxAudioSource.clip = sfx.audioClip;
            sfxAudioSource.volume = sfx.volume;
            sfxAudioSource.Play();
        }

    }

    public void StopAllSFXs()
    {
        foreach(AudioSource audioSource in m_SFXAudioSources)
        {
            audioSource.Stop();
        }
    }

    private AudioSource GetAvailableSfxAudioSource()
    {
        AudioSource available = null;

        foreach (AudioSource audioSource in m_SFXAudioSources)
        {
            if (!audioSource.isPlaying)
            {
                available = audioSource;
                break;
            }
        }

        if(available == null)
        {
            available = m_SFXAudioSources[Random.Range(0, MAX_SFX_AUDIO_SOURCES)];
        }

        return available;
    }

    public static SoundManager GetInstance()
    {
        if(m_Instance == null)
        {
            m_Instance = new SoundManager();
        }

        return m_Instance;
    }

}
