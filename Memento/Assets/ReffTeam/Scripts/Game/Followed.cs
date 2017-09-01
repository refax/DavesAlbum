using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followed : MonoBehaviour {

    [SerializeField]
    private string m_FollowedName;

    public string followedName
    {
        get
        {
            return m_FollowedName;
        }
    }

}
