using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFirstText : MonoBehaviour
{
	public Text				thisText;
	private string[]		outText;
	private const string	gameMode = "Game Mode : ";
	private const string	normalMode = "Normal Mode";
	private const string	infinityMode = "Infinity Mode";
	private const string	shogiMode = "Shogi Mode";

	void Start()
	{
		outText = new string[4]{gameMode, normalMode, infinityMode, shogiMode};
		thisText.text = outText[0] + outText[PlayerPrefs.GetInt("Mode", 1)];
	}
}
