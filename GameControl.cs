using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
	private int					gameMode;

	public GameObject			player1SetObject;
	private PieceSetControl		player1Script;

	public GameObject			player2SetObject;
	private PieceSetControl		player2Script;

	public GameObject			gameBoardObject;
	private GameBoardControl	gameBoardScript;

	public GameObject			moveObject;

	private GameObject			movePlayerSetObject;
	private PieceSetControl		movePlayerScript;

	public GameObject			interfaceObject;
	private ResultControl		interfaceScript;

	private int				turn;
	private int[]			passTime;
	private int				backtobackPass;
	public int				moveLocation;


	void Awake()
	{
		gameMode = PlayerPrefs.GetInt("Mode");
		player1Script = player1SetObject.GetComponent<PieceSetControl>();
		player2Script = player2SetObject.GetComponent<PieceSetControl>();
		gameBoardScript = gameBoardObject.GetComponent<GameBoardControl>();
		interfaceScript = interfaceObject.GetComponent<ResultControl>();
		turn = 0;
		passTime = new int[2]{0, 0};
		movePlayerScript = player2Script;
	}

	void Start()
	{
		ChangeMovePlayer();
		PlayerColliderSwitch();
		GameSpriteUpdate();
		player2Script.PieceSetUpdate();
	}

	private void	ChangeMovePlayer()
	{
		movePlayerScript.SetCover(true);
		if (turn % 2 == 0)
		{
			movePlayerScript = player1Script;
			movePlayerSetObject = player1SetObject;
		}
		else
		{
			movePlayerScript = player2Script;
			movePlayerSetObject = player2SetObject;
		}
		movePlayerScript.SetCover(false);
	}

	private void	PlayerColliderSwitch()
	{
		bool	flag = (turn % 2 == 0);

		player1Script.ColliderUpdate(flag);
		player2Script.ColliderUpdate(!flag);
	}

	private void	GameSpriteUpdate()
	{
		movePlayerScript.PieceSetUpdate();
		gameBoardScript.BoardUpdate();
	}

	public void		Move(int rank)
	{
		int		tempRank = 0;

		if (!gameBoardScript.Move(moveLocation, turn % 2 + 1, rank, ref tempRank))
			return;
		backtobackPass = 0;
		movePlayerScript.RemovePiece(rank);
		if (gameMode == 2 && turn / 2 - passTime[turn % 2] >= 5)
			movePlayerScript.AddPiece(1);
		else if (gameMode == 3 && tempRank != 0)
			movePlayerScript.AddPiece(tempRank);
		GameSpriteUpdate();
		++turn;
		ChangeMovePlayer();
		if (gameBoardScript.IsBingo())
		{
			MakeResult((turn - 1) % 2 + 1);
			return ;
		}
		PlayerColliderSwitch();
		if (!CheckMoveable())
			Pass();
	}

	public void		Pass()
	{
		++backtobackPass;
		++passTime[turn % 2];
		gameBoardScript.Pass(turn % 2 + 1);
		++turn;
		ChangeMovePlayer();
		if (backtobackPass > 1)
		{
			MakeResult(0);
			return ;
		}
		PlayerColliderSwitch();
		if (!CheckMoveable())
			Pass();
	}

	public void		MoveBack()
	{
		MoveClass	move = null;

		Debug.Log(turn);
		if (turn == 0)
			return ;
		--turn;
		ChangeMovePlayer();
		gameBoardScript.MoveBack(ref move);
		Debug.Log(move.postState.rank);
		if (move.postState.rank != 0)
		{
			if (gameMode == 2 && turn / 2 - passTime[turn % 2] >= 5)
				movePlayerScript.RemovePiece(1);
			else if (gameMode == 3 && move.preState.rank != 0)
				movePlayerScript.RemovePiece(move.preState.rank);
			movePlayerScript.AddPiece(move.postState.rank);
			GameSpriteUpdate();
		}
		else
		{
			--passTime[turn % 2];
			--backtobackPass;
		}
		PlayerColliderSwitch();
		Debug.Log(CheckMoveable());
		if (!CheckMoveable())
			MoveBack();
	}

	private bool	CheckMoveable()
	{
		if (movePlayerScript.HighRank() > gameBoardScript.LowRank())
			return true;
		return false;
	}

	public void		MakeResult(int result)
	{
		player1Script.ColliderUpdate(false);
		player2Script.ColliderUpdate(false);
		player1Script.SetCover(true);
		player2Script.SetCover(true);
		interfaceScript.MakeResult(result, gameBoardScript.Pieces());
	}
}
