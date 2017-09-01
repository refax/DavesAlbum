using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    [SerializeField]
    private Sprite m_GateTopOpen;
    [SerializeField]
    private Sprite m_GateMiddleOpen;
    [SerializeField]
    private Sprite m_GateTopClose;
    [SerializeField]
    private Sprite m_GateMiddleClose;

    [SerializeField]
    private bool m_Open = false;

    private SpriteRenderer m_GateTop;
    private SpriteRenderer m_GateMiddle;

    private void Awake()
    {
        Transform topTransform = this.transform.Find("Gate_Top");
        if (topTransform != null)
        {
            m_GateTop = topTransform.gameObject.GetComponent<SpriteRenderer>();
        }

        Transform middleTransform = this.transform.Find("Gate_Middle");
        if (middleTransform != null)
        {
            m_GateMiddle = middleTransform.gameObject.GetComponent<SpriteRenderer>();
        }

        SetGateState(m_Open);

    }

    public void SetGateState(bool i_Open)
    {
        m_Open = i_Open;

        if (m_Open)
        {
            m_GateTop.sprite = m_GateTopOpen;
            m_GateMiddle.sprite = m_GateMiddleOpen;
        }
        else
        {
            m_GateTop.sprite = m_GateTopClose;
            m_GateMiddle.sprite = m_GateMiddleClose;
        }
    }

    public bool open
    {
        get
        {
            return m_Open;
        }
    }

}
