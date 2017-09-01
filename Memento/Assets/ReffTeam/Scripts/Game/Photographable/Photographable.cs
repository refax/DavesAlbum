using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Photographable : MonoBehaviour
{
    private int m_id = 0;
    private bool m_photographed = false;

    [SerializeField]
    private Color m_ColorOnPhotographed = Color.white;

    private SpriteRenderer m_SpriteRender;

    private void Awake()
    {
        m_SpriteRender = GetComponent<SpriteRenderer>();
    }

    public bool isPhotographed() {
        return m_photographed;
    }
    public bool equals(Photographable other) {
        return m_id == other.m_id;
    }

    public void setPhotographed(bool i_photographed) {
        m_photographed = i_photographed;

        if(i_photographed)
        {
            m_SpriteRender.color = m_ColorOnPhotographed;
        }
        else
        {
            m_SpriteRender.color = Color.white;
        }
    }

    public void setID(int i_id) {
        m_id = i_id;
    }

    public int getID()
    {
        return m_id;
    }
}
