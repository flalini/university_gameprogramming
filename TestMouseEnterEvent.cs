using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMouseEnterEvent : MonoBehaviour
{
	private SpriteRenderer	rend;
	public GameObject		other;
	private	SpriteRenderer	otherRend;
	public GameObject		control;
	private GameControl		script;
	public int				location;

	void Start()
	{
		rend = GetComponent<SpriteRenderer>();
		otherRend = other.GetComponent<SpriteRenderer>();
		script = control.GetComponent<GameControl>();
	}

	void OnMouseEnter()
	{
		otherRend.color -= Color.black * 0.5f;
		rend.sprite = otherRend.sprite;
		rend.sortingLayerID = otherRend.sortingLayerID;
		script.moveLocation = location + 1;
		if (int.Parse(transform.GetChild(0).gameObject.tag) < int.Parse(other.tag))
			rend.color = Color.green;
		else
			rend.color = Color.red;
		rend.color -= Color.black * 0.5f;
	}

	void OnMouseExit()
	{
		script.moveLocation = 0;
		otherRend.color += Color.black * 0.5f;
		rend.color = Color.clear;
		rend.sortingLayerID = otherRend.sortingLayerID;
	}
}
