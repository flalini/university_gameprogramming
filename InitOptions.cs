using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InitOptions : MonoBehaviour
{
	void Start()
	{
		PlayerPrefs.SetInt("Mode", 0);
		PlayerPrefs.SetFloat("Komi", 0.5f);
		PlayerPrefs.SetString("Sprite1", "white");
		PlayerPrefs.SetString("Sprite2", "black");
	}
}
