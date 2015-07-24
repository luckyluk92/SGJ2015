using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public int dayTime = 60;
	float currentTime;

	public float money;
	
	// Use this for initialization
	void Start () 
	{
		currentTime = 0;
		money = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		TimeFlow();

		//Debug.Log(currentTime + " " + money);
	}

	void TimeFlow()
	{
		if( currentTime < dayTime)
		{
			currentTime += Time.deltaTime;
		}
		else
		{
			currentTime = 0;
			//Load next Day
		}
	}	
}
