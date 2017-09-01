using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public delegate void PlayerDeath(PlayerPlatformerController player);


public class PlayerPlatformerController : PhysicsObject
{
    /* Delegate */
    public event PlayerDeath OnPlayerDeath = null;
    [SerializeField]
    private float m_maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    Vector3 m_MyStartPosition;

    private bool m_EnemyHit = false;
    private bool m_Jump = false;

    private bool m_LeftMovementFromOutside = false;
    private bool m_RightMovementFromOutside = false;
    private bool m_JumpFromOutside = false;


    public bool ForceLeftMovement
    {
        set { m_LeftMovementFromOutside = value; }
    }

    public bool ForceRightMovement
    {
        set { m_RightMovementFromOutside = value; }
    }

    public bool ForceJump
    {
        set { m_JumpFromOutside = value; }
    }

    public bool EnemyHit
    {
        get
        {
            return m_EnemyHit;
        }

        set
        {
            m_EnemyHit = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        m_MyStartPosition = transform.position;
    }

    protected override void virtualUpdate()
    {

        ComputeVelocity();
        HandleAnimation();
    }


    private void ComputeVelocity()
    {

        Vector2 move = Vector2.zero;


        if(m_LeftMovementFromOutside)
        {
            move.x = -1.0f;
        }
        else if(m_RightMovementFromOutside)
        {
            move.x = 1.0f;
        }
        else
        {
            move.x = Input.GetAxis("Horizontal");
        }

        if (m_JumpFromOutside)
        {
            m_Jump = true;
            m_JumpFromOutside = false;
        }
        else
        {
            m_Jump = Input.GetButtonDown("Jump");
        }
       

        if (m_Jump && grounded)
        {
            SoundManager.GetInstance().PlaySFX("Jump");
            velocity.y = jumpTakeOffSpeed;
            m_Jump = false;
        }


        if ((move.x > 0 && spriteRenderer.flipX) || (move.x < 0 && !spriteRenderer.flipX))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        targetVelocity = move * m_maxSpeed;
    }

    public void stop()
    {
        targetVelocity = Vector2.zero;
        velocity = Vector2.zero;
        Input.ResetInputAxes();
    }

    public bool IsFacingRight()
    {
        return !spriteRenderer.flipX;
    }

    private void HandleAnimation()
    {

        if (!grounded)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }

        animator.SetFloat("Speed", Mathf.Abs(velocity.x) / m_maxSpeed);

        animator.SetFloat("VelY", velocity.y);

        animator.SetBool("IsOnAir", !grounded);

    }


    public bool CheckDistanceCollisionBetweenPlayerAndFrontObject(float MaxDistance)
    {
        return false;
    }

    public void Kill()
    {
        if(OnPlayerDeath!=null)
        {
            OnPlayerDeath( this );
        }
    }

    public void ResetPlayerPosition()
    {
        transform.position = m_MyStartPosition;
    }


    private void OnDisable()
    {
        m_RightMovementFromOutside = false;
        m_RightMovementFromOutside = false;
    }




}