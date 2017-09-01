using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour {

    [SerializeField]
    private Transform m_CameraTransform;

    [SerializeField]
	private Transform[] m_BackgroundHorizontal;
    [SerializeField]
    private Transform[] m_BackgroundUp;
    [SerializeField]
    private Transform[] m_BackgroundDown;


    private Transform[][] m_BackgroundsV;

    private Vector3[,] m_BackgroundsVPositionBackup;



    private int m_LeftIndex;
	private int m_RightIndex;

    private int m_DownIndex;
    private int m_UpIndex;
	[SerializeField]
	private float m_BackgroundWSize = 20.34f;
    
    [SerializeField]
	private float m_ParalaxSpeed;
	private float m_LastCameraX;


    private Vector3 m_BackgroundDownPositionBackup;
    private Vector3 m_BackgroundCentralPositionBackup;
    private Vector3 m_BackgroundUpPositionBackup;

    // Use this for initialization

    private void Awake()
    {
        m_BackgroundsV = new Transform[3][];

        m_BackgroundsV[0] = m_BackgroundDown;
        m_BackgroundsV[1] = m_BackgroundHorizontal;
        m_BackgroundsV[2] = m_BackgroundUp;

        m_BackgroundsVPositionBackup = new Vector3[3,3];

        for(int i=0; i<m_BackgroundsV.Length; i++)
        {
            for(int j=0; j<m_BackgroundsV[i].Length; j++)
            {
                m_BackgroundsVPositionBackup[i, j] = new Vector3(m_BackgroundsV[i][j].position.x, m_BackgroundsV[i][j].position.y, m_BackgroundsV[i][j].position.z);
            }
        }


    }

    public void ResetBackground()
    {
        m_LastCameraX = m_CameraTransform.position.x;
        m_LeftIndex = 0;
        m_DownIndex = 0;
        m_RightIndex = m_BackgroundHorizontal.Length - 1;
        m_UpIndex = 2;

        for (int i = 0; i < m_BackgroundsV.Length; i++)
        {
            for (int j = 0; j < m_BackgroundsV[i].Length; j++)
            {
                m_BackgroundsV[i][j].position = new Vector3(m_BackgroundsVPositionBackup[i,j].x, m_BackgroundsVPositionBackup[i,j].y, m_BackgroundsVPositionBackup[i,j].z);
            }
        }
    }

    void Start ()
    {
        ResetBackground();
    }

	private void ScrollLeft()
	{
        // int lastRight = m_RightIndex;
        m_BackgroundUp[m_RightIndex].position = new Vector3((m_BackgroundUp[m_LeftIndex].position.x - m_BackgroundWSize), m_BackgroundUp[m_RightIndex].position.y, m_BackgroundUp[m_RightIndex].position.z);
        m_BackgroundHorizontal[m_RightIndex].position = new Vector3((m_BackgroundHorizontal[m_LeftIndex].position.x - m_BackgroundWSize), m_BackgroundHorizontal[m_RightIndex].position.y, m_BackgroundHorizontal[m_RightIndex].position.z);
        m_BackgroundDown[m_RightIndex].position = new Vector3((m_BackgroundDown[m_LeftIndex].position.x - m_BackgroundWSize), m_BackgroundDown[m_RightIndex].position.y, m_BackgroundDown[m_RightIndex].position.z);

        m_LeftIndex = m_RightIndex;
		m_RightIndex--;
		if (m_RightIndex < 0) m_RightIndex = m_BackgroundHorizontal.Length - 1;
	}

	private void ScrollRight()
	{
        //  int lastLeft = m_LeftIndex;

        m_BackgroundUp[m_LeftIndex].position = new Vector3((m_BackgroundUp[m_RightIndex].position.x + m_BackgroundWSize), m_BackgroundUp[m_LeftIndex].position.y, m_BackgroundUp[m_LeftIndex].position.z);
        m_BackgroundHorizontal[m_LeftIndex].position = new Vector3((m_BackgroundHorizontal[m_RightIndex].position.x + m_BackgroundWSize), m_BackgroundHorizontal[m_LeftIndex].position.y, m_BackgroundHorizontal[m_LeftIndex].position.z);
        m_BackgroundDown[m_LeftIndex].position = new Vector3((m_BackgroundDown[m_RightIndex].position.x + m_BackgroundWSize), m_BackgroundDown[m_LeftIndex].position.y, m_BackgroundDown[m_LeftIndex].position.z);

        m_RightIndex = m_LeftIndex;
		m_LeftIndex++;
		if (m_LeftIndex == m_BackgroundHorizontal.Length) m_LeftIndex = 0;
	}


    private void ScrollUp()
    {
        //  int lastLeft = m_LeftIndex;
        for (int i=0; i< m_BackgroundsV[m_DownIndex].Length; i++)
        {
            m_BackgroundsV[m_DownIndex][i].position = new Vector3(m_BackgroundsV[m_UpIndex][i].position.x, (m_BackgroundsV[m_UpIndex][i].position.y + 9), m_BackgroundsV[m_UpIndex][i].position.z);

        }
        m_UpIndex = m_DownIndex;
        m_DownIndex++;

        if (m_DownIndex == 3) m_DownIndex = 0;
    }

    private void ScrollDown()
    {   
        for (int i = 0; i < m_BackgroundsV[m_UpIndex].Length; i++)
        {
            m_BackgroundsV[m_UpIndex][i].position = new Vector3(m_BackgroundsV[m_DownIndex][i].position.x, (m_BackgroundsV[m_DownIndex][i].position.y - 9), m_BackgroundsV[m_DownIndex][i].position.z);
        }
        m_DownIndex = m_UpIndex;
        m_UpIndex--;
        if (m_UpIndex < 0) m_UpIndex = 2;
    }

    // Update is called once per frame
    void Update () {

		float deltaX = m_CameraTransform.position.x - m_LastCameraX;
		transform.position += Vector3.right * (deltaX * m_ParalaxSpeed);
		m_LastCameraX = m_CameraTransform.position.x;

		if (m_CameraTransform.position.x < (m_BackgroundHorizontal[m_LeftIndex]).transform.position.x + m_BackgroundWSize / 2)
			ScrollLeft();
		if (m_CameraTransform.position.x > (m_BackgroundHorizontal[m_RightIndex]).transform.position.x - m_BackgroundWSize / 2)
			ScrollRight();

        if (m_CameraTransform.position.y < (m_BackgroundsV[m_DownIndex][0]).transform.position.y + 9 / 2)
            ScrollDown();

        if (m_CameraTransform.position.y > (m_BackgroundsV[m_UpIndex][0]).transform.position.y - 9 / 2)
            ScrollUp();
    }
}
