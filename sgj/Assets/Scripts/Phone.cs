using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

	public bool isRinging = false;
	public bool isBossSpeaking;

	public GameObject progressBar;

	public int minSpeakingTime = 2;
	public int maxSpeakingTime = 7;
	private int speakingTime;

	public float moneyPerSecond = 50;

	private float _currentTime;
	private float _totalTime;
	public float ringingTime;

	public float ringingChanceThreshold = 8.9f;
	

	private GameState _gameState;
	// Use this for initialization
	void Start () {
		isBossSpeaking = false;
		_gameState = Camera.main.GetComponent<GameState>();
		ringingTime = 5;
		_currentTime = 0;

		progressBar.GetComponent<StressProgressBar>().isVisible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((int)_gameState.currentTime % (int)(_gameState.dayTime/4) == 0)
		{
			float diceThrow = Random.Range(0,10);
			if(diceThrow > ringingChanceThreshold && !isBossSpeaking)
				isRinging = true;
		}


		if(isBossSpeaking)
		{
			_currentTime = Time.deltaTime;
			_totalTime += _currentTime;
		}
		else if(isRinging)
		{
			_currentTime += Time.deltaTime;

			if(_currentTime >= ringingTime)
			{
				isRinging = false;
				_currentTime = 0;
			}
		}



		progressBar.SendMessage("ProgressUpdated", _totalTime/speakingTime);
		_totalTime = Mathf.Clamp(_totalTime, 0, speakingTime);
	}

	void CalculateSpeakingTime()
	{
		speakingTime = Random.Range(minSpeakingTime, maxSpeakingTime);
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if(Input.GetKeyDown(KeyCode.LeftShift) && collider.name == "Boss" && isRinging)
		{
			CalculateSpeakingTime();

			isRinging = false;
			isBossSpeaking = true;

			progressBar.GetComponent<StressProgressBar>().isVisible = true;
		}
		else if(Input.GetKeyUp(KeyCode.LeftShift) && collider.name == "Boss")
		{
			if(_totalTime == speakingTime)
			{
				_gameState.money += moneyPerSecond*speakingTime;
				Debug.Log(moneyPerSecond* speakingTime);
			}
			isBossSpeaking = false;
			_totalTime = 0;
			progressBar.GetComponent<StressProgressBar>().isVisible = false;
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.name == "Boss")
		{
			isBossSpeaking = false;
			_totalTime = 0;
			progressBar.GetComponent<StressProgressBar>().isVisible = false;
		}
	}

}
