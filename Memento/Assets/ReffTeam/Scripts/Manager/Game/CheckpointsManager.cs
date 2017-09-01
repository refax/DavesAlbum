using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsManager : MonoBehaviour {

    private Checkpoint[] m_CheckpointsOld;
    private Checkpoint[] m_CheckpointsYoung;

    private Checkpoint m_OldCheckpointOpen;
    private Checkpoint m_YoungCheckpointOpen;

    public void Initialize(Checkpoint[] i_CheckpointsOld, Checkpoint[] i_CheckpointsYoung,
                            Checkpoint i_OldCheckpointOpen, Checkpoint i_YoungCheckpointOpen)
    {
        m_CheckpointsOld = i_CheckpointsOld;
        m_CheckpointsYoung = i_CheckpointsYoung;
        m_OldCheckpointOpen = i_OldCheckpointOpen;
        m_YoungCheckpointOpen = i_YoungCheckpointOpen;

        foreach (Checkpoint checkpoint in m_CheckpointsOld)
        {
            checkpoint.ChangeState(false);
            checkpoint.OnCheckpointEnterEvent += OldCheckpointEntered;
        }
        m_OldCheckpointOpen.ChangeState(true);

        foreach (Checkpoint checkpoint in m_CheckpointsYoung)
        {
            checkpoint.ChangeState(false);
            checkpoint.OnCheckpointEnterEvent += YoungCheckpointEntered;
        }
        m_YoungCheckpointOpen.ChangeState(true);

    }

    public void RespawnOld(PlayerPlatformerController i_OldPlayer)
    {
        i_OldPlayer.transform.parent = m_OldCheckpointOpen.transform.parent;
        i_OldPlayer.stop();
        i_OldPlayer.transform.position = m_OldCheckpointOpen.transform.position + new Vector3(0, -0.275f, 0);
    }

    public void RespawnYoung(PlayerPlatformerController i_YoungPlayer)
    {
        i_YoungPlayer.transform.parent = m_YoungCheckpointOpen.transform.parent;
        i_YoungPlayer.stop();
        i_YoungPlayer.transform.position = m_YoungCheckpointOpen.transform.position + new Vector3(0,-0.275f, 0);

    }

    public void OldCheckpointEntered(Checkpoint i_Old)
    {
        if (m_OldCheckpointOpen != i_Old)
        {
            SoundManager.GetInstance().PlaySFX("Teleport");
            m_OldCheckpointOpen = i_Old;
            foreach (Checkpoint checkpoint in m_CheckpointsOld)
            {
                checkpoint.ChangeState(false);
            }
            m_OldCheckpointOpen.ChangeState(true);
        }
    }

    public void YoungCheckpointEntered(Checkpoint i_Young)
    {
        if (m_YoungCheckpointOpen != i_Young)
        {
            SoundManager.GetInstance().PlaySFX("Teleport");
            m_YoungCheckpointOpen = i_Young;
            foreach (Checkpoint checkpoint in m_CheckpointsYoung)
            {
                checkpoint.ChangeState(false);
            }
            m_YoungCheckpointOpen.ChangeState(true);
        }
    }
}
