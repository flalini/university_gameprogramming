using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetModeSprite : MonoBehaviour
{
	private Sprite[]		outSprite;
	[SerializeField]
	private Image			thisImage;
	[SerializeField]
	private Sprite			normalMode;
	[SerializeField]
	private Sprite			infinityMode;
	[SerializeField]
	private Sprite			shogiMode;

	void Start()
	{
		outSprite = new Sprite[3]{normalMode, infinityMode, shogiMode};
		thisImage.sprite = outSprite[PlayerPrefs.GetInt("Mode", 1) - 1];
	}
}