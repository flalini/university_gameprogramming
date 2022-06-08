using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualChange : MonoBehaviour
{
	[SerializeField]
	private GameObject		displayObject;
	[SerializeField]
	private GameObject		textObject;
	private Sprite[]		manualSprite;
	private int				manualIndex;
	private Image			displaySprite;
	private Text			numberingText;

	void Awake()
	{
		manualSprite = Resources.LoadAll<Sprite>("manual");
		displaySprite = displayObject.GetComponent<Image>();
		numberingText = textObject.GetComponent<Text>();
	}

	private void	OnEnable()
	{
		manualIndex = 0;
		ChangeManual();
	}

	public void		PositiveChange()
	{
		++manualIndex;
		ChangeManual();
	}

	public void		NegativeChange()
	{
		--manualIndex;
		ChangeManual();
	}

	private void	ChangeManual()
	{
		if (manualIndex == -1)
			manualIndex = manualSprite.Length - 1;
		else if (manualIndex == manualSprite.Length)
			manualIndex = 0;
		displaySprite.sprite = manualSprite[manualIndex];
		numberingText.text = "-" + (manualIndex + 1) + "-";
	}
}
