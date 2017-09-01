using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhotographablesManager : MonoBehaviour
{
    private float m_SpaceLenghtRequiredToSpawn = 1.0f;

    private PlayerPlatformerController m_OldPlayer;
    private Transform m_OldLevel;


    public void Initialize(GameObject i_OldPlayer, GameObject i_OldLevel )
    {
        if (i_OldPlayer != null)
        {
            m_OldPlayer = i_OldPlayer.GetComponent<PlayerPlatformerController>();
        }

        if (i_OldLevel != null)
        {
            m_OldLevel = i_OldLevel.transform;
        }
    }

    public bool SpawnPhotographable(Photographable i_PhotographableObj)
    {
        if (m_OldPlayer != null && i_PhotographableObj != null)
        {
            if (!m_OldPlayer.CheckDistanceCollisionBetweenPlayerAndFrontObject(m_SpaceLenghtRequiredToSpawn))
            {
                float l_PlayerDirection = m_OldPlayer.IsFacingRight() ? 1 : -1;
                Vector3 l_OffsetPosition = new Vector3(0.7f, l_PlayerDirection * 0.25f, 0) * l_PlayerDirection;
                GameObject obj = Instantiate(
                    i_PhotographableObj.gameObject,
                    m_OldPlayer.transform.position + l_OffsetPosition, 
                    Quaternion.identity,
                    m_OldLevel);

                obj.GetComponent<Photographable>().setID(i_PhotographableObj.getID());
                obj.GetComponent<SpriteRenderer>().color = Color.white;

                if (obj != null)
                    return true;

            }
        }

        return false;
    }

    public void DeletePhotographable(Photographable i_PhotographableObj)
    {
        if (i_PhotographableObj != null)
        {       
            Destroy(i_PhotographableObj.gameObject);
        }
    }
}
