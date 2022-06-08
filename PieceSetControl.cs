using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSetControl : MonoBehaviour
{
	public int					player;
	private PieceSetClass		pieceSet;
	private Sprite[]			pieceSprite;
	private SpriteRenderer[]	pieceSpriteArray;
	private Collider2D[]		pieceCollider;

	void Awake()
	{
		pieceSet = new PieceSetClass(player);
		pieceSprite = Resources.LoadAll<Sprite>(
			PlayerPrefs.GetString("Sprite" + player, "white"));
		pieceSpriteArray = new SpriteRenderer[6];
		pieceCollider = new Collider2D[6];
		for (int i = 0; i < 6; ++i)
		{
			pieceSpriteArray[i]
				= transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>();
			pieceCollider[i]
				= transform.GetChild(i).GetChild(0).GetComponent<Collider2D>();
		}
		PieceSetUpdate();
	}

	public void		PieceSetUpdate()
	{
		for (int i = 0; i < 6; ++i)
		{
			if (pieceSet.piece[i].rank != 0)
				pieceSpriteArray[i].sprite
					= pieceSprite[pieceSet.piece[i].rank - 1];
			else
				pieceSpriteArray[i].sprite = null;
			transform.GetChild(i).GetChild(0).tag = pieceSet.piece[i].rank + "";
		}
	}

	public void		ColliderUpdate(bool flag)
	{
		for (int i = 0; i < 6; ++i)
		{
			if (pieceSet.piece[i].rank != 0)
				pieceCollider[i].enabled = flag;
			else
				pieceCollider[i].enabled = false;
		}
	}

	public void		SetCover(bool flag)
	{
		transform.GetChild(6).gameObject.SetActive(flag);
	}

	public void		RemovePiece(int rank)
	{
		pieceSet.RemovePiece(rank);
	}

	public void		AddPiece(int rank)
	{
		pieceSet.AddPiece(rank);
	}

	public int		HighRank()
	{
		return pieceSet.highRank;
	}
}
