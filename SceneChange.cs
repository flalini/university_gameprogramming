using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public void		SceneChangeCall(int mode)
	{
		PlayerPrefs.SetInt("Mode", mode);
		if (mode == 0)
			SceneManager.LoadScene("TitleScene");
		else
			SceneManager.LoadScene("GameScene");
	}

	public void		SceneReload()
	{
		SceneManager.LoadScene("GameScene");
	}
}
