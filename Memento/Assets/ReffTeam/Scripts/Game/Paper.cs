using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PaperGrabEvent(Paper i_Grabbed);

public class Paper : MonoBehaviour {

    public PaperGrabEvent OnPaperGrabEvent;

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        PlayerPlatformerController player = i_Other.gameObject.GetComponent<PlayerPlatformerController>();
        if (player != null)
        {
            if(OnPaperGrabEvent != null)
            {
                OnPaperGrabEvent(this);
            }

        }
    }

}
