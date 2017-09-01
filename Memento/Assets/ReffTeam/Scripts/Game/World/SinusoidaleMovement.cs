using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidaleMovement : MonoBehaviour
{

    [SerializeField]
    private float m_MaxVerticalDistanceFromCenter;
    [SerializeField]
    private float m_MaxHorizontalDistanceFromCenter;

    [SerializeField]
    private float m_VerticalSpeed;
    [SerializeField]
    private float m_HorizontalSpeed;

    [SerializeField]
    private bool m_VerticalMovement;

    [SerializeField]
    private bool m_HorizontalMovment;

    [SerializeField]
    private bool m_ObliqualMovement;


   // [SerializeField]
   // private Collider2D m_BoxCollider;

   // [SerializeField]
   // private Collider2D m_BoxColliderAsTrigger;


    private float m_RunningTime;

    private void Awake()
    {
        m_RunningTime = 0;
        /*
        if (m_BoxCollider != null && m_BoxColliderAsTrigger != null)
        {
            Physics2D.IgnoreCollision(m_BoxColliderAsTrigger, m_BoxCollider);
        }
        else
        {
            Debug.LogError("You must set a BoxCollider and a BoxColliderAsTrigger in SinusoidaleMovement script!");
        }
        */
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float l_DeltaTime = Time.deltaTime;

        Vector3 l_NewPosition = transform.position;
        float l_DeltaHeight = (Mathf.Sin( (m_RunningTime + l_DeltaTime) * m_VerticalSpeed) - Mathf.Sin(m_RunningTime* m_VerticalSpeed));


        float l_DeltaWidth = (Mathf.Sin((m_RunningTime + l_DeltaTime) * m_HorizontalSpeed) - Mathf.Sin(m_RunningTime* m_HorizontalSpeed));
        float l_DeltaWidthObliqual = (Mathf.Sin((m_RunningTime + l_DeltaTime) * m_HorizontalSpeed) - Mathf.Sin(m_RunningTime * m_HorizontalSpeed));

        if (m_VerticalMovement || m_ObliqualMovement)
        {
            l_NewPosition.y += l_DeltaHeight * m_MaxVerticalDistanceFromCenter;
        }

        if(m_ObliqualMovement)
        {
            l_NewPosition.x += l_DeltaWidthObliqual * m_MaxHorizontalDistanceFromCenter;
        }
        if(m_HorizontalMovment)
        {
            l_NewPosition.x += l_DeltaWidth * m_MaxHorizontalDistanceFromCenter;
        }

        transform.position = l_NewPosition;

        m_RunningTime += l_DeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if (gameObject.activeInHierarchy)
        {
            i_Other.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D i_Other)
    {
        if (gameObject.activeInHierarchy)
        {
            i_Other.transform.parent = transform.parent;
        }
    }

}
