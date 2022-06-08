using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClass
{
	public	int location;
	public	PieceClass preState;
	public	PieceClass postState;

	public	MoveClass(int loca, PieceClass pre, PieceClass post)
	{
		location = loca;
		preState = pre;
		postState = post;
	}

	public	MoveClass(int loca, int prePlayer, int preRank, int postPlayer, int postRank)
	{
		location = loca;
		preState = new PieceClass(prePlayer, preRank);
		postState = new PieceClass(postPlayer, postRank);
	}
}
