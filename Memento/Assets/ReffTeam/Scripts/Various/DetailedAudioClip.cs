using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedAudioClip {

    private AudioClip m_AudioClip;

    private float m_Volume;

    public DetailedAudioClip(AudioClip i_AudioClip, float i_Volume)
    {
        m_AudioClip = i_AudioClip;
        m_Volume = i_Volume;
    }

    public AudioClip audioClip
    {
        get
        {
            return m_AudioClip;
        }
    }

    public float volume
    {
        get
        {
            return m_Volume;
        }
    }

}
