using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardClass
{
	private const int			rowSize = 3;
	private const int			colSize = 3;
	private const int			boardSize = rowSize * colSize;
	public PieceClass[]			piece;
	private Stack<MoveClass>	moveStack;
	public int					lowRank;
	public bool					bingoFlag;

	public			GameBoardClass()
	{
		piece = new PieceClass[boardSize];
		for (int i = 0; i < boardSize; ++i)
			piece[i] = new PieceClass(0, 0);
		moveStack = new Stack<MoveClass>();
		lowRank = 0;
		bingoFlag = false;
	}

	public bool		Move(int loc, int player, int rank)
	{
		if (piece[loc].rank >= rank)
			return false;
		moveStack.Push(
			new MoveClass(
				loc, 
				piece[loc].player, piece[loc].rank, 
				player, rank));
		piece[loc].Set(player, rank);
		BingoCheck(loc, player);
		UpdateRank();
		return true;
	}

	public void		Pass(int player)
	{
		moveStack.Push(
			new MoveClass(
				0, 
				0, 0, 
				player, 0));
	}

	public bool		MoveBack(ref MoveClass result)
	{
		if (moveStack.Count != 0)
		{
			bingoFlag = false;
			result = moveStack.Pop();
			if (result.postState.rank != 0)
			{
				piece[result.location].Set(result.preState.player, result.preState.rank);
				UpdateRank();
			}
			return true;
		}
		return false;
	}

	private void	BingoCheck(int loc, int player)
	{
		int		i;

		for (i = loc % rowSize; i < boardSize; i += rowSize)
			if (piece[i].player != player)
				break ;
		if (i >= boardSize)
		{
			bingoFlag = true;
			return ;
		}
		for (i = (loc / rowSize) * rowSize; i / rowSize == loc / rowSize; ++i)
			if (piece[i].player != player)
				break ;
		if (i / rowSize != loc / rowSize)
		{
			bingoFlag = true;
			return ;
		}
		if (loc % (rowSize + 1) == 0)
		{
			for (i = 0; i < boardSize; i += rowSize + 1)
				if (piece[i].player != player)
					break ;
			if (i >= boardSize)
			{
				bingoFlag = true;
				return ;
			}
		}
		if (loc != 0 && loc != boardSize - 1 && loc % (rowSize - 1) == 0)
		{
			for (i = rowSize - 1; i < boardSize - 1; i += rowSize - 1)
				if (piece[i].player != player)
					break ;
			if (i == boardSize - 1)
			{
				bingoFlag = true;
				return ;
			}
		}
	}

	private void	UpdateRank()
	{
		lowRank = piece[0].rank;
		for (int i = 1; i < boardSize; ++i)
			if (piece[i].rank < lowRank)
				lowRank = piece[i].rank;
	}
}