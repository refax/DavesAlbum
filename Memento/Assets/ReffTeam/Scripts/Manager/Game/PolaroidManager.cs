using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnPaperIncreasedEvent();

public class PolaroidManager : MonoBehaviour
{
    public const int MAX_PAPER = 9;

    public OnPaperIncreasedEvent OnPaperIncreasedEvent;

    private int m_PaperNumber;

    private Paper[] m_Papers;

    private GameObject m_OldPlayer;
    private GameObject m_YoungPlayer;

    public void Initialize(GameObject i_OldPlayer, GameObject i_YoungPlayer, int i_PaperNumber)
    {
        m_OldPlayer = i_OldPlayer;
        m_YoungPlayer = i_YoungPlayer;
        m_PaperNumber = i_PaperNumber;

        if (m_PaperNumber > MAX_PAPER)
        {
            m_PaperNumber = MAX_PAPER;
        }

        m_Papers = FindObjectsOfType<Paper>();
        if(m_Papers != null)
        {
            foreach (Paper paper in m_Papers)
            {
                paper.OnPaperGrabEvent += GrabPaper;
            }
        }

    }

    public Photographable TakePhoto()
    {
        if(m_PaperNumber > 0)
        {
            Photographable l_Photographable = FindFrontPhotographable(m_YoungPlayer);
            if (l_Photographable != null && !l_Photographable.isPhotographed())
            {
                --m_PaperNumber;
                l_Photographable.setPhotographed(true);
                return l_Photographable;
            }
        }
        return null;
    }

    public Photographable RetrievePhoto()
    {
        return FindFrontPhotographable(m_OldPlayer);
    }

    public void ReleasePhoto( Photographable i_Photo )
    {
        if(i_Photo != null)
        {
            i_Photo.setPhotographed(false);
            ++m_PaperNumber;
        }
    }

    public int GetPaperNumber()
    {
        return m_PaperNumber;
    }

    private Photographable FindFrontPhotographable(GameObject i_Player)
    {
        if (i_Player != null)
        {
            Photographable photographable = FindFrontPhotographableOffset(i_Player, new Vector3(0, 0, 0));

            if(photographable == null)
            {
                photographable = FindFrontPhotographableOffset(i_Player, new Vector3(0, -0.4f, 0));

                if (photographable == null)
                {
                    photographable = FindFrontPhotographableOffset(i_Player, new Vector3(0, +0.4f, 0));
                }
            }

            return photographable;
        }

        return null;
    }

    private Photographable FindFrontPhotographableOffset(GameObject i_Player, Vector3 i_VerticalOffset)
    {
        Transform l_PlayerTransform = i_Player.transform;
        BoxCollider2D l_PlayerCollider = i_Player.GetComponent<BoxCollider2D>();
        PlayerPlatformerController l_playerController = i_Player.GetComponent<PlayerPlatformerController>();

        Vector2 l_RayCastDirection = Vector2.right;
        Vector3 l_RayCastOffset = new Vector3(-0.2f, 0, 0);

        if (l_playerController != null && !l_playerController.IsFacingRight())
        {
            l_RayCastDirection = Vector2.left;
            l_RayCastOffset = new Vector3(0.2f, 0, 0);
        }

        if (l_PlayerCollider != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(l_PlayerTransform.position + l_RayCastOffset + i_VerticalOffset,
                                                    l_RayCastDirection, 1.0f);
            if (hit.collider != null)
            {
                Photographable l_Photographable = hit.collider.gameObject.GetComponent<Photographable>();
                return l_Photographable;
            }
        }

        return null;
    }
    
    private void GrabPaper(Paper i_ToGrab)
    {
        SoundManager.GetInstance().PlaySFX("Paper");
        i_ToGrab.gameObject.SetActive(false);
        i_ToGrab.OnPaperGrabEvent -= GrabPaper;
        m_PaperNumber++;
        if(m_PaperNumber > MAX_PAPER)
        {
            m_PaperNumber = MAX_PAPER;
        }
        if (OnPaperIncreasedEvent != null)
        {
            OnPaperIncreasedEvent();
        }
    }
}
