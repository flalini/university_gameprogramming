using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointListInit : MonoBehaviour
{
	private Sprite[]			player1Sprite;
	private Sprite[]			player2Sprite;

	void Awake()
	{
		player1Sprite = Resources.LoadAll<Sprite>(
			PlayerPrefs.GetString("Sprite1", "white"));
		player2Sprite = Resources.LoadAll<Sprite>(
			PlayerPrefs.GetString("Sprite2", "black"));
		for (int i = 0; i < 6; ++i)
		{
			transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite
				= player1Sprite[i];
			transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite
				= player2Sprite[i];
		}
	}
}
