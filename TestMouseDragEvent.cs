using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMouseDragEvent : MonoBehaviour
{
	private SpriteRenderer	rend;
	private Color			existingColor;
	public GameObject		other;
	private SpriteRenderer	otherRend;
	public GameObject		control;
	private GameControl		script;
	private int				hideLayer;
	private int				defaultLayer;

	void Start()
	{
		rend = GetComponent<SpriteRenderer>();
		existingColor = rend.color;
		otherRend = other.GetComponent<SpriteRenderer>();
		script = control.GetComponent<GameControl>();
		hideLayer = SortingLayer.NameToID("Hide");
		defaultLayer = SortingLayer.NameToID("Default");
	}

	void OnMouseDown()
	{
		rend.color -= Color.black * 0.5f;
		otherRend.sprite = rend.sprite;
		otherRend.color = rend.color;
		other.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		otherRend.sortingLayerID = defaultLayer;
		other.transform.localScale = transform.localScale;
		other.tag = gameObject.tag;
	}

	void OnMouseDrag()
	{
		other.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
	}

	void OnMouseUp()
	{
		rend.color = existingColor;
		otherRend.sortingLayerID = hideLayer;
		if (script.moveLocation != 0)
		{
			script.moveLocation--;
			script.Move(int.Parse(other.tag));
		}
	}
}
