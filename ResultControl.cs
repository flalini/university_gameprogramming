using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultControl : MonoBehaviour
{
	private GameObject			playInterface;
	private GameObject			resultInterface;
	private GameObject			tttUI;
	private GameObject			pointUI;
	private Text[]				player1Text;
	private Text[]				player2Text;
	private Text				winnerText;

	void Awake()
	{
		playInterface = transform.GetChild(0).gameObject;
		resultInterface = transform.GetChild(1).gameObject;
		tttUI = resultInterface.transform.GetChild(1).gameObject;
		pointUI = resultInterface.transform.GetChild(2).gameObject;
		winnerText = resultInterface.transform.GetChild(3).GetComponent<Text>();
		player1Text = new Text[8];
		player2Text = new Text[8];
		for (int i = 0; i < 8; ++i)
		{
			player1Text[i] = pointUI.transform.GetChild(1).GetChild(i).GetComponent<Text>();
			player2Text[i] = pointUI.transform.GetChild(2).GetChild(i).GetComponent<Text>();
		}
	}

	public void		MakeResult(int result, PieceClass[] pieces)
	{
		playInterface.SetActive(false);
		resultInterface.SetActive(true);
		if (result != 0)
		{
			tttUI.SetActive(true);
			pointUI.SetActive(false);
			if (result == 1)
				winnerText.text = "백";
			else
				winnerText.text = "흑";
			winnerText.text += " 승리";
		}
		else
		{
			tttUI.SetActive(false);
			pointUI.SetActive(true);
			Scoring(pieces);
		}
	}

	private void	Scoring(PieceClass[] pieces)
	{
		float[,]	playerPoint = new float[2, 8];
		float		komi;
		int			i;


		komi = PlayerPrefs.GetFloat("Komi", 0.5f);
		if (komi > 0)
			playerPoint[1, 0] = komi;
		else
			playerPoint[0, 0] = -komi;
		for (i = 0; i < 9; ++i)
			if (pieces[i].rank != 0)
				playerPoint[pieces[i].player - 1, pieces[i].rank] += pieces[i].rank;
		for (i = 0; i < 7; ++i)
		{
			playerPoint[0, 7] += playerPoint[0, i];
			if (playerPoint[0, i] != 0f)
				player1Text[i].text = playerPoint[0, i] + "";
			else
				player1Text[i].text = "";
			playerPoint[1, 7] += playerPoint[1, i];
			if (playerPoint[1, i] != 0f)
				player2Text[i].text = playerPoint[1, i] + "";
			else
				player2Text[i].text = "";
		}
		player1Text[7].text = playerPoint[0, 7] + "";
		player2Text[7].text = playerPoint[1, 7] + "";
		if (playerPoint[0, 7] > playerPoint[1, 7])
			winnerText.text = "백";
		else
			winnerText.text = "흑";
		winnerText.text += " 승리";
	}
}
