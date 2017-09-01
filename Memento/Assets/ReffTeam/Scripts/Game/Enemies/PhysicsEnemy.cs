using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsEnemy : Enemy
{
    private const int MAX_FRAMES_WITHOUT_MOVING = 2;

    private int m_FrameWithoutMove = 0;

    [SerializeField]
    private float m_StartMovementDirection = -1;

    [SerializeField]
    private float m_MaxLeftDistance = 10000f;
    [SerializeField]
    private float m_MaxRightDistance = 10000f;

    private bool m_DoNotCheckDistanceMovementForAWhile = false;

    private float m_CurrentMovementDirection;

    private float m_InitialXPosition;

    private float m_CheckMovementEnablerTimer = 0.0f; 

    protected override void virtualAwake()
    {
        m_CurrentMovementDirection = m_StartMovementDirection;
        m_InitialXPosition = transform.position.x;
    }


    private void ChangeDirection()
    {
        m_CurrentMovementDirection *= -1;
        m_FrameWithoutMove = -1;
        m_DoNotCheckDistanceMovementForAWhile = true;
    }

    protected override void ComputeEnemyVelocity()
    {
        Vector2 move = Vector2.zero;

        /* Change Direction if Enemy can't move */
        if (velocity == Vector2.zero)
        {
            m_FrameWithoutMove++;

            if (m_FrameWithoutMove > MAX_FRAMES_WITHOUT_MOVING)
            {
                ChangeDirection();
            }
        }
        else
        {
            m_FrameWithoutMove = 0;

            if (!m_DoNotCheckDistanceMovementForAWhile)
            {
                if (transform.position.x > (m_InitialXPosition + m_MaxRightDistance) || transform.position.x < (m_InitialXPosition - m_MaxLeftDistance))
                {
                    ChangeDirection();
                }

            }
        }

        if(m_DoNotCheckDistanceMovementForAWhile)
        {
            m_CheckMovementEnablerTimer += Time.deltaTime;
            if(m_CheckMovementEnablerTimer > 0.1)
            {
                m_DoNotCheckDistanceMovementForAWhile = false;
            }
        }
        else
        {
            m_CheckMovementEnablerTimer = 0.0f;
        }
        


        if (!m_IsDead)
        {       
            move.x = m_CurrentMovementDirection;
            
        }
        else
        {
            move.y = -1;
        }

        targetVelocity = move * m_maxSpeed;

        HandleAnimation();
    }


}
