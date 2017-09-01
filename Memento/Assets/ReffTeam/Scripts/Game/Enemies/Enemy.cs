using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void EnemyDeath();

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : PhysicsObject
{
    private Animator m_Animator;
    [SerializeField]
    protected float m_maxSpeed = 1;
    private Collider2D m_Collider;

    public EnemyDeath OnEnemyDeath = null;

    protected bool m_IsDead = false;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider2D>();
        virtualAwake();
    }

    protected void HandleAnimation()
    {
        if (m_IsDead)
        {
            m_Animator.SetTrigger("Die");
        }
        m_Animator.SetFloat("Speed", Mathf.Abs(velocity.x) / m_maxSpeed);
    }




    public void Die()
    {
        m_IsDead = true;
        m_Collider.enabled = false;
        m_GravityModifier = 1.0f; /* On death force gravity for death animation */



        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerPlatformerController l_Player = collision.gameObject.GetComponent<PlayerPlatformerController>();
            if(l_Player != null)
                l_Player.Kill();
            /*
            if (l_Player != null)
            {
                Vector3 l_HitDirection = transform.position - collision.transform.position;
                Vector3 l_Formward = transform.right;

                float dotProduct = Vector3.Dot(l_HitDirection.normalized, l_Formward.normalized);

                dotProduct = Mathf.Abs(dotProduct);


                if (dotProduct < 0.5)
                {
                    l_Player.EnemyHit = true;
                    Die();
                }
                else
                {
                   
                }
            }
            else
            {
                Debug.Log("WARNING: Missing PlayerPlatformerController Script on player!!");
            }*/

        }
    }

    protected override void virtualUpdate()
    {
        ComputeEnemyVelocity();
        HandleAnimation();
    }

    protected virtual void ComputeEnemyVelocity()
    {

    }

    protected virtual void virtualAwake()
    {

    }
}
