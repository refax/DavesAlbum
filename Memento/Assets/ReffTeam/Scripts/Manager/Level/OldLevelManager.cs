using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldLevelManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_StartingPoint;

    [SerializeField]
    private GameObject m_OldPlayer;

    private void Awake()
    {
        if (m_StartingPoint != null && m_OldPlayer != null)
        {
            m_OldPlayer.transform.position = m_StartingPoint.transform.position + new Vector3(0, 10, 0);
        }
        else
        {
            Debug.Log("Starting Point or Player not Defined in OldLevelManager");
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
