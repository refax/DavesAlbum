using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    public static bool OpenedFromMainMenu = false;

    [SerializeField]
    private Button m_BackToMenu;
    [SerializeField]
    private Button m_Start;

    [SerializeField]
    private GameObject m_HowToPlay;
    [SerializeField]
    private GameObject m_Old;
    [SerializeField]
    private GameObject m_Young;

    [SerializeField]
    private Image m_HowToPlayImage;

    [SerializeField]
    private Sprite m_HowToPlayMobile;
    [SerializeField]
    private Sprite m_HowToPlayPC;

    private void Awake()
    {
        m_Start.gameObject.SetActive(!OpenedFromMainMenu);
        m_BackToMenu.gameObject.SetActive(OpenedFromMainMenu);

        #if UNITY_ANDROID || UNITY_IOS
            m_HowToPlayImage.sprite = m_HowToPlayMobile;
        #else
            m_HowToPlayImage.sprite = m_HowToPlayPC;
        #endif
    }

    public void NextToYoung()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        m_Old.SetActive(false);
        m_Young.SetActive(true);
    }
    public void NextToOld()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        m_HowToPlay.SetActive(false);
        m_Old.SetActive(true);
    }


    public void BackToOld()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        m_Old.SetActive(true);
        m_Young.SetActive(false);
    }

    public void BackToTutorial()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        m_HowToPlay.SetActive(true);
        m_Old.SetActive(false);
    }

    public void StartLevel()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void BackToMain()
    {
        SoundManager.GetInstance().PlaySFX("Button");
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }

}
