using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

	private SpriteRenderer _spriteRenderer;
	
	public Sprite active,inactive;
	
	private bool _isHighlighted;
	
	// Use this for initialization
	void Start () 
	{
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		_isHighlighted = false;
		_spriteRenderer.sprite = inactive;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			_isHighlighted = true;
			_spriteRenderer.sprite = active;
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			_spriteRenderer.sprite = inactive;
			_isHighlighted = false;
		}
		
		if(_isHighlighted && Input.GetKeyDown(KeyCode.Return))
		{
				Application.LoadLevel("MainMenu");
		}

	}

}
