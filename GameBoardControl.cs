using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardControl : MonoBehaviour
{
	private int					gameMode;
	private GameBoardClass		gameState;
	public GameObject			displayObject;
	private SpriteRenderer[]	boardSpriteArray;
	private SpriteRenderer[]	boardAboveSpriteArray;
	private SpriteRenderer[]	displaySpriteArray;
	private Sprite[]			player1Sprite;
	private Sprite[]			player2Sprite;
	private Sprite[]			display1Sprite;
	private Sprite[]			display2Sprite;
	private int					hideLayer;
	private int					defaultLayer;

	void Awake()
	{
		gameState = new GameBoardClass();
		gameMode = PlayerPrefs.GetInt("Mode", 1);
		player1Sprite = Resources.LoadAll<Sprite>(
			PlayerPrefs.GetString("Sprite1", "white"));
		player2Sprite = Resources.LoadAll<Sprite>(
			PlayerPrefs.GetString("Sprite2", "black"));
		display1Sprite = Resources.LoadAll<Sprite>("num/red");
		display2Sprite = Resources.LoadAll<Sprite>("num/blue");
		hideLayer = SortingLayer.NameToID("Hide");
		defaultLayer = SortingLayer.NameToID("Default");

		boardSpriteArray = new SpriteRenderer[9];
		boardAboveSpriteArray = new SpriteRenderer[9];
		displaySpriteArray = new SpriteRenderer[9];
		for (int i = 0; i < 9; ++i)
		{
			boardSpriteArray[i]
				= transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>();
			boardAboveSpriteArray[i]
				= transform.GetChild(i).GetComponent<SpriteRenderer>();
			displaySpriteArray[i]
				= displayObject.transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>();
		}
		BoardUpdate();
	}

	public void				BoardUpdate()
	{
		for (int i = 0; i < 9; ++i)
		{
			if (gameState.piece[i].rank != 0)
			{
				if (gameState.piece[i].player == 1)
				{
					boardSpriteArray[i].sprite
						= player1Sprite[gameState.piece[i].rank - 1];
					displaySpriteArray[i].sprite
						= display1Sprite[gameState.piece[i].rank - 1];
				}
				else
				{
					boardSpriteArray[i].sprite
						= player2Sprite[gameState.piece[i].rank - 1];
					displaySpriteArray[i].sprite
						= display2Sprite[gameState.piece[i].rank - 1];
				}
			}
			else
			{
				boardSpriteArray[i].sprite = null;
				displaySpriteArray[i].sprite = null;
			}
			transform.GetChild(i).GetChild(0).tag = gameState.piece[i].rank + "";
			boardAboveSpriteArray[i].sortingLayerID = hideLayer;
		}
	}

	public bool				Move(int location, int player, int rank, ref int catchPiece)
	{
		if (gameState.piece[location].rank >= rank)
		{
			boardAboveSpriteArray[location].sortingLayerID = hideLayer;
			return false;
		}
		catchPiece = gameState.piece[location].rank;
		gameState.Move(location, player, rank);
		return true;
	}

	public void				Pass(int player)
	{
		gameState.Pass(player);
	}

	public void				MoveBack(ref MoveClass move)
	{
		gameState.MoveBack(ref move);
	}

	public int				LowRank()
	{
		return gameState.lowRank;
	}

	public bool				IsBingo()
	{
		return gameState.bingoFlag;
	}

	public PieceClass[]		Pieces()
	{
		return gameState.piece;
	}
}
