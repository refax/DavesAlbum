using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotosManager : MonoBehaviour
{
    private ArrayList m_PhotosList;
    private ArrayList m_PhotosBusy;
    private int m_SelectedPhotoIndex;

    private int m_PhotosIDCounter;

    private void Awake()
    {
        m_PhotosList = new ArrayList();
        m_PhotosBusy = new ArrayList();
        m_SelectedPhotoIndex = 0;
        m_PhotosIDCounter = 0;
    }

    private void UpdatePhotoIndex()
    {
        m_SelectedPhotoIndex = m_PhotosList.Count - 1;
    }

    /**
     * Return current added element if was added, null otherwise 
     */
    public Photographable TakePhoto(Photographable i_PhotographableObj)
    {
        if (i_PhotographableObj != null)
        {
            i_PhotographableObj.setID(m_PhotosIDCounter);
            m_PhotosList.Add(i_PhotographableObj);
            UpdatePhotoIndex();

            m_PhotosIDCounter++;

            return i_PhotographableObj;
        }

        return null;
    }

    public Photographable SetBusyPhotoToAvailable(Photographable i_SpawnedObj)
    {
        if (i_SpawnedObj != null)
        {
            /* Search the busy photo with the same ID of the spawned one */
            for (int i = 0; i < m_PhotosBusy.Count; i++)
            {
                Photographable l_PhotoInUse = (Photographable)m_PhotosBusy[i];

                if (l_PhotoInUse != null)
                {
                    if (i_SpawnedObj.getID() == l_PhotoInUse.getID())
                    {
                        m_PhotosBusy.Remove(l_PhotoInUse);
                        m_PhotosList.Add(l_PhotoInUse);

                        UpdatePhotoIndex();

                        return l_PhotoInUse;
                    }
                }
            }
        }

        return null;
    }

    public void SetAvailablePhotoToBusy(Photographable i_PhotographableObj )
    {
        if (i_PhotographableObj != null && m_PhotosList.Count != 0 )
        {
            m_PhotosList.Remove(i_PhotographableObj);
            m_PhotosBusy.Add(i_PhotographableObj);
            UpdatePhotoIndex();
        }
    }
    
    public Photographable RemovePhoto()
    {
        if(m_PhotosList.Count != 0)
        {
            Photographable l_CurrentPhoto = (Photographable)m_PhotosList[m_SelectedPhotoIndex];
            m_PhotosList.RemoveAt(m_SelectedPhotoIndex);
            UpdatePhotoIndex();
            return l_CurrentPhoto;
        }

        return null;
    }

    public Photographable SelectedPreviousPhoto()
    {
        if(m_PhotosList.Count > 1)
        {
            m_SelectedPhotoIndex =
                (m_SelectedPhotoIndex == 0) ?
                                                (m_PhotosList.Count - 1) :
                                                (m_SelectedPhotoIndex - 1);

            return (Photographable)m_PhotosList[m_SelectedPhotoIndex];
        }

        return null;
    }

    public Photographable SelectedNextPhoto()
    {
        if (m_PhotosList.Count > 1)
        {
            m_SelectedPhotoIndex = (m_SelectedPhotoIndex + 1) % m_PhotosList.Count;

            return (Photographable)m_PhotosList[m_SelectedPhotoIndex];
        }
        return null;
    }

    /**
     * Return current selected photo, null of list is empty
     */
    public Photographable GetCurrentSelectedPhoto()
    {
        if(m_PhotosList.Count != 0 && m_SelectedPhotoIndex < m_PhotosList.Count)
        {
            return (Photographable)m_PhotosList[m_SelectedPhotoIndex];
        }

        return null;
    }
}
