using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {
    [SerializeField]
    private string m_ExitDoorTag;

    [SerializeField]
    private GameObject m_DoorClosed;

    [SerializeField]
    private GameObject m_DoorOpened;

    private bool m_PortIsOpened;

    private void Awake()
    {
        m_PortIsOpened = false;

        if (m_DoorClosed == null)
        {
            Debug.Log("You must set DoorClosed (with SpriteRender) GameObject in ExitDoor");
        }
  

        if(m_DoorOpened == null)
        {
            Debug.Log("You must set DoorOpened (with SpriteRender) GameObject in ExitDoor");
        }
  

        m_DoorOpened.SetActive(m_PortIsOpened);
        m_DoorClosed.SetActive(!m_PortIsOpened);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_DoorOpened.SetActive(m_PortIsOpened);
        m_DoorClosed.SetActive(!m_PortIsOpened);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(m_ExitDoorTag))
        {
            m_PortIsOpened = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals(m_ExitDoorTag))
        {
            m_PortIsOpened = false;
        }
    }
}
