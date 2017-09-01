using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoungLevelManager : MonoBehaviour {
    [SerializeField]
    private GameObject m_StartingPoint;

    [SerializeField]
    private GameObject m_YoungPlayer;


    private void Awake()
    {
        if(m_StartingPoint != null && m_YoungPlayer != null)
        {
            m_YoungPlayer.transform.position = m_StartingPoint.transform.position + new Vector3(0,50,0);
        }
        else
        {
            Debug.Log("Starting Point or Player not Defined in YoungLevelManager");
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
