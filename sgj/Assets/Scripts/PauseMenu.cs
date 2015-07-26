using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public Transform resumeButtonPos, exitButtonPos; 
	public GameObject resumeButton, exitButton;

	private bool _didEnterPause, _isPaused;
	// Use this for initialization
	void Start () 
	{
		_didEnterPause = false;
		_isPaused = false;

		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Pause();
		SpawnButtons();
	
	}


	void Pause()
	{
		if(Time.timeScale == 1 && Input.GetKeyDown(KeyCode.Escape))
			Time.timeScale = 0;
	}

	void SpawnButtons()
	{
		if(Time.timeScale == 0f && !_isPaused)
			_didEnterPause = true;
		
		if(_didEnterPause == true)
		{
			_didEnterPause = false;
			_isPaused = true;
			
			Instantiate(resumeButton, resumeButtonPos.position, resumeButtonPos.rotation);
			Instantiate(exitButton, exitButtonPos.position, exitButtonPos.rotation);
		}
	}
}
