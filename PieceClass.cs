using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceClass
{
	public int		player;
	public int		rank;

	public			PieceClass(int playerValue, int rankValue)
	{
		player = playerValue;
		rank = rankValue;
	}

	public void		Set(int playerValue, int rankValue)
	{
		player = playerValue;
		rank = rankValue;
	}

	public void		Set(int rankValue)
	{
		rank = rankValue;
	}
}
