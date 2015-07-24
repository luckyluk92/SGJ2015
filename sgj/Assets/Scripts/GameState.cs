using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public float dayTime = 60f;
	private float _currentTime;

	public float wage = 100;

	//[HideInInspector]
	public float numberOfEmployees;

	public float money;
	
	// Use this for initialization
	void Start () 
	{
		_currentTime = 0;
		money = 0;
		numberOfEmployees = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		TimeFlow();
		//Debug.Log(currentTime + " " + money);
	}

	void TimeFlow()
	{
		if( _currentTime < dayTime)
		{
			_currentTime += Time.deltaTime;
		}
		else
		{
			_currentTime = 0;
			money -= numberOfEmployees * wage;
			isThisTheEnd();
			//Load next Day
		}
	
	}

	void isThisTheEnd()
	{
		if(money <= 0)
			Debug.Log("Game Over"); //TODO
	}
}
