using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferencesManager {

	private const string FIRST_PLAY = "First_Play";
	private const string MUSIC_ENABLED = "Music_Enabled";
	private const string MAX_LEVEL_COMPLETED = "Max_Level";

    public static bool IsMusicEnabled()
	{
		return PlayerPrefs.GetInt(MUSIC_ENABLED, 1) == 1;
	}

	public static void EnableMusic()
	{
		PlayerPrefs.SetInt(MUSIC_ENABLED, 1);
	}

	public static void DisableMusic()
	{
		PlayerPrefs.SetInt(MUSIC_ENABLED, 0);
	}

	public static int GetMaxLevelCompleted()
	{
		return PlayerPrefs.GetInt(MAX_LEVEL_COMPLETED, GlobalDataManager.NO_LEVEL_PLAYED);
	}

	public static void ResetData()
	{
		PlayerPrefs.SetInt(MAX_LEVEL_COMPLETED, GlobalDataManager.NO_LEVEL_PLAYED);
		PlayerPrefs.SetInt(FIRST_PLAY, 0);
	}

	public static void LevelCompleted(int i_LevelDone)
	{
		if (GetMaxLevelCompleted() < i_LevelDone)
		{
			PlayerPrefs.SetInt(MAX_LEVEL_COMPLETED, i_LevelDone);
		}
	}

	public static bool IsFirstPlay()
	{
		return PlayerPrefs.GetInt(FIRST_PLAY, 0) == 0;
	}

	public static void FirstTimePlayed()
	{
		PlayerPrefs.SetInt(FIRST_PLAY, 1);
	}

}
