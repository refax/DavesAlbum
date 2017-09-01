using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : PhysicsObject
{

    [SerializeField]
    private string m_ToFollow;

    [SerializeField]
    private float m_MinDistance;
    [SerializeField]
    private float m_MaxDistance;
    [SerializeField]
    private float m_MaxSpeed;

    protected override void virtualUpdate()
    {
        Vector2 move = Vector2.zero;
        move.x = ShouldMove();
        targetVelocity = move * m_MaxSpeed;
    }

    private int ShouldMove()
    {
        Transform transform = gameObject.transform;

        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(m_MinDistance, 0, 0), Vector2.right, m_MaxDistance);

        if (hitRight.collider != null)
        {
            Followed followed = hitRight.collider.gameObject.GetComponent<Followed>();
            if (followed != null && followed.followedName == m_ToFollow)
            {
                return 1;
            }
        }

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + new Vector3(-m_MinDistance, 0, 0), Vector2.left, m_MaxDistance);

        if (hitLeft.collider != null)
        {
            Followed followed = hitLeft.collider.gameObject.GetComponent<Followed>();
            if (followed != null && followed.followedName == m_ToFollow)
            {
                return -1;
            }
        }

        return 0;
    }

}
