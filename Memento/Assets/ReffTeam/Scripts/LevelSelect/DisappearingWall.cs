using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingWall : MonoBehaviour {
    
    [SerializeField]
    private int m_LevelIndex;

    [SerializeField]
    private bool m_DisappearIfGreater = false;

    public bool ShouldDisapper(int i_MaxLevelCompleted)
    {
        if (m_DisappearIfGreater && m_LevelIndex > i_MaxLevelCompleted
            || !m_DisappearIfGreater && m_LevelIndex <= i_MaxLevelCompleted)
        {
            return true;
        }

        return false;
    }

}