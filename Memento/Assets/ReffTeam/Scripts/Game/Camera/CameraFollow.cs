using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	private GameObject m_LeftWall;
	[SerializeField]
	private GameObject m_RightWall;
	[SerializeField]
	private float m_UpEdge;
	[SerializeField]
	private float m_DownEdge;

	[SerializeField]
	private GameObject m_Player;


	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float l_CameraWith = Camera.main.orthographicSize * Camera.main.aspect;
			transform.position = new Vector3(
				Mathf.Clamp(m_Player.transform.position.x, m_LeftWall.transform.position.x + l_CameraWith, m_RightWall.transform.position.x - l_CameraWith),
				Mathf.Clamp(m_Player.transform.position.y, m_DownEdge, m_UpEdge),
				transform.position.z
				);
	}
}
