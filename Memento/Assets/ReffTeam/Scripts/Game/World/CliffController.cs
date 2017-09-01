using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffController : MonoBehaviour {

    [SerializeField]
    private BoxCollider2D m_PlatformCollider;
    [SerializeField]
    private BoxCollider2D m_PlatformTrigger;

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(m_PlatformCollider, m_PlatformTrigger, true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Physics2D.IgnoreCollision(m_PlatformCollider, other, true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Physics2D.IgnoreCollision(m_PlatformCollider, other, false);
    }
}
