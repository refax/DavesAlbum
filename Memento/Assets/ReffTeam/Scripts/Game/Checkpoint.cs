using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnCheckpointEnterEvent(Checkpoint i_Checkpoint);

public class Checkpoint : MonoBehaviour {

    public OnCheckpointEnterEvent OnCheckpointEnterEvent;

    [SerializeField]
    private Sprite m_OpenSprite;

    [SerializeField]
    private Sprite m_CloseSprite;

    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(m_SpriteRenderer != null)
        {
            m_SpriteRenderer.sprite = m_CloseSprite;
        }
    }

    public void ChangeState(bool i_Open)
    {
        if(m_SpriteRenderer != null)
        {
            if (i_Open)
            {
                m_SpriteRenderer.sprite = m_OpenSprite;
            }
            else
            {
                m_SpriteRenderer.sprite = m_CloseSprite;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(OnCheckpointEnterEvent != null && collision.gameObject.GetComponent<PlayerPlatformerController>() != null)
        {
            OnCheckpointEnterEvent(this);
        }
    }

}
