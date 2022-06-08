using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlBackup : MonoBehaviour
{
	private int				gameMode;

	private	PieceSetClass	player1;
	public GameObject		player1SetObject;

	private PieceSetClass	player2;
	public GameObject		player2SetObject;

	private GameBoardClass	gameState;
	public GameObject		gameBoardObject;
	public GameObject		displayObject;

	public GameObject		moveObject;

	private PieceSetClass	movePlayer;
	private GameObject		movePlayerSetObject;

	private Sprite[]		player1Sprite;
	private Sprite[]		player2Sprite;
	private Sprite[]		display1Sprite;
	private Sprite[]		display2Sprite;
	private Sprite[]		movePlayerSprite;

	private int				turn;
	private int[]			passTime;
	private int				backtobackPass;
	public int				moveLocation;


	void Start()
	{
		gameMode = PlayerPrefs.GetInt("Mode");
		player1 = new PieceSetClass(1);
		player2 = new PieceSetClass(2);
		gameState = new GameBoardClass();
		turn = 0;
		passTime = new int[2]{0, 0};
		player1Sprite = Resources.LoadAll<Sprite>("white");
		player2Sprite = Resources.LoadAll<Sprite>("black");
		display1Sprite = Resources.LoadAll<Sprite>("num/red");
		display2Sprite = Resources.LoadAll<Sprite>("num/blue");
		movePlayerSetObject = player2SetObject;
		ChangeMovePlayer();
		PlayerColliderSwitch();
		GameSpriteUpdate();
		Player2SpriteUpdate();
	}

	private void	ChangeMovePlayer()
	{
		movePlayerSetObject.transform.GetChild(7).gameObject.SetActive(true);
		if (turn % 2 == 0)
		{
			movePlayer = player1;
			movePlayerSetObject = player1SetObject;
			movePlayerSprite = player1Sprite;
		}
		else
		{
			movePlayer = player2;
			movePlayerSetObject = player2SetObject;
			movePlayerSprite = player2Sprite;
		}
		movePlayerSetObject.transform.GetChild(7).gameObject.SetActive(false);
	}

	private void	PlayerColliderSwitch()
	{
		bool	flag = (turn % 2 == 0);
		int		i;

		for (i = 0; i < 6; ++i)
		{
			if (player1.piece[i].rank != 0)
				player1SetObject.transform.GetChild(i).GetChild(0).GetComponent<Collider2D>().enabled = flag;
			else
				player1SetObject.transform.GetChild(i).GetChild(0).GetComponent<Collider2D>().enabled = false;
			if (player2.piece[i].rank != 0)
				player2SetObject.transform.GetChild(i).GetChild(0).GetComponent<Collider2D>().enabled = !flag;
			else
				player2SetObject.transform.GetChild(i).GetChild(0).GetComponent<Collider2D>().enabled = false;
		}
	}

	private void	GameSpriteUpdate()
	{
		int		i;

		for (i = 0; i < 6; ++i)
		{
			if (movePlayer.piece[i].rank != 0)
				movePlayerSetObject.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite
					= movePlayerSprite[movePlayer.piece[i].rank - 1];
			else
				movePlayerSetObject.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite
					= null;
			movePlayerSetObject.transform.GetChild(i).GetChild(0).tag = movePlayer.piece[i].rank + "";
		}
		//gameBoardScript.BoardUpdate();
		for (i = 0; i < 9; ++i)
		{
			if (gameState.piece[i].rank != 0)
			{
				if (gameState.piece[i].player == 1)
				{
					gameBoardObject.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite
						= player1Sprite[gameState.piece[i].rank - 1];
					displayObject.transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>().sprite
						= display1Sprite[gameState.piece[i].rank - 1];
				}
				else
				{
					gameBoardObject.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite
						= player2Sprite[gameState.piece[i].rank - 1];
					displayObject.transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>().sprite
						= display2Sprite[gameState.piece[i].rank - 1];
				}
			}
			else
			{
				gameBoardObject.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite
					= null;
				displayObject.transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>().sprite
					= null;
			}
			gameBoardObject.transform.GetChild(i).GetChild(0).tag = gameState.piece[i].rank + "";
			gameBoardObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 0;
		}
	}

	private void	Player2SpriteUpdate()
	{
		int		i;

		for (i = 0; i < 6; ++i)
		{
			if (player2.piece[i].rank != 0)
			{
				player2SetObject.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite
					= player2Sprite[player2.piece[i].rank - 1];
			}
			else
				player2SetObject.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite
					= null;
			player2SetObject.transform.GetChild(i).GetChild(0).tag = player2.piece[i].rank + "";
		}
	}

	public void		Move(int rank)
	{
		int		tempRank = 0;

		//if (!BoardScript.Move())
		//	return;
		if (gameState.piece[moveLocation].rank >= rank)
		{
			gameBoardObject.transform.GetChild(moveLocation).GetComponent<SpriteRenderer>().sortingOrder = 0;
			return ;
		}
		backtobackPass = 0;
		if (gameMode == 3)
			tempRank = gameState.piece[moveLocation].rank;
		gameState.Move(moveLocation, turn % 2 + 1, rank);
		movePlayer.RemovePiece(rank);
		if (gameMode == 2 && turn / 2 - passTime[turn % 2] > 5)
			movePlayer.AddPiece(1);
		else if (gameMode == 3 && tempRank != 0)
			movePlayer.AddPiece(tempRank);
		GameSpriteUpdate();
		++turn;
		ChangeMovePlayer();
		if (gameState.bingoFlag)
		{
			//MakeResult(turn % 2 + 1);
		}
		PlayerColliderSwitch();
		if (!CheckMoveable())
			Pass();
	}

	public void		Pass()
	{
		++backtobackPass;
		++passTime[turn % 2];
		gameState.Move(0, turn % 2 + 1, 0);
		++turn;
		ChangeMovePlayer();
		if (backtobackPass > 1)
		{
			//MakeResult(draw_var);
		}
		PlayerColliderSwitch();
		if (!CheckMoveable())
			Pass();
	}

	public void		MoveBack()
	{
		MoveClass	move = null;

		if (turn == 0)
			return ;
		--turn;
		ChangeMovePlayer();
		gameState.MoveBack(ref move);
		if (move.postState.rank != 0)
		{
			if (gameMode == 2 && turn / 2 - passTime[turn % 2] > 5)
				movePlayer.RemovePiece(1);
			else if (gameMode == 3 && move.preState.rank != 0)
				movePlayer.RemovePiece(move.preState.rank);
			movePlayer.AddPiece(move.postState.rank);
			GameSpriteUpdate();
		}
		else
		{
			--passTime[turn % 2];
			--backtobackPass;
		}
		PlayerColliderSwitch();
		if (!CheckMoveable())
			MoveBack();
	}

	private bool	CheckMoveable()
	{
		//if (movePlayerScript.HighRank() > gameStateScript.LowRank())
		if (movePlayer.highRank > gameState.lowRank)
			return true;
		return false;
	}

	public void		MakeResult(int result)
	{
		if (result == 0)
		{
			//포인트 계산
		}
		else
		{
			//player result win
		}
	}
}
