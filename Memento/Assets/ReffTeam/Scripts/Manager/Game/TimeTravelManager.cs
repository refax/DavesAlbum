using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelManager : MonoBehaviour
{
	private GameObject m_OldLevel;
	private GameObject m_YoungLevel;

	public void Initialize(GameObject i_OldLevel, GameObject i_YoungLevel)
	{
		m_OldLevel = i_OldLevel;
		m_YoungLevel = i_YoungLevel;
	}

	public void TimeTravel()
	{

		if(m_OldLevel != null && m_YoungLevel != null)
		{
			if (IsFuture())
			{
				TravelBackward();
			}
			else
			{
				TravelForward();
			}
		}

	}

	public bool IsFuture()
	{
		return m_OldLevel.activeSelf;
	}

	private void TravelForward()
	{
		m_OldLevel.SetActive(true);
		m_YoungLevel.SetActive(false);
	}

	private void TravelBackward()
	{
		m_OldLevel.SetActive(false);
		m_YoungLevel.SetActive(true);
	}
}
