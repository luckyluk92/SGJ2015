using UnityEngine;
using System.Collections;

public class Retry : MonoBehaviour {

	private SpriteRenderer _spriteRenderer;

	public Sprite active,inactive;

	private bool _isHighlighted;
	public bool isPaused = false;
	public string objectName = "Quit(Clone)";
	// Use this for initialization
	void Start () 
	{
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		_isHighlighted = true;
		_spriteRenderer.sprite = active;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			_isHighlighted = true;
			_spriteRenderer.sprite = active;
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			_spriteRenderer.sprite = inactive;
			_isHighlighted = false;
		}

		if(_isHighlighted && Input.GetKeyDown(KeyCode.Return))
		{
			if(isPaused)
			{
				Time.timeScale = 1f;
				Destroy(gameObject);
				Destroy(GameObject.Find(objectName));
			}
			else
				Application.LoadLevel("Main");
		}
	}

}
