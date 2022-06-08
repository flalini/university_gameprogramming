using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSetClass
{
	public const int		setSize = 6;
	public PieceClass[]		piece;
	private int				player;
	public int				highRank;

	public			PieceSetClass(int playerValue)
	{
		player = playerValue;
		piece = new PieceClass[setSize];
		for (int i = 0; i < setSize; ++i)
			piece[i] = new PieceClass(player, i + 1);
		highRank = setSize;
	}

	public void		RemovePiece(int rank)
	{
		if (rank > 0)
			for (int i = 0; i < setSize; ++i)
				if (piece[i].rank == rank)
				{
					for (; i > 0; --i)
						piece[i].Set(piece[i - 1].rank);
					piece[0].Set(0);
					UpdateRank();
					return ;
				}
	}

	public void		AddPiece(int rank)
	{
		int		i = 0;

		if (piece[0].rank != 0 || rank <= 0)
			return ;
		while (++i < setSize)
			if (piece[i].rank >= rank)
				break ;
		--i;
		for (int j = 0; j < i; ++j)
			piece[j].Set(piece[j + 1].rank);
		piece[i].Set(rank);
		UpdateRank();
	}

	private void	UpdateRank()
	{
		highRank = piece[setSize - 1].rank;
	}
}