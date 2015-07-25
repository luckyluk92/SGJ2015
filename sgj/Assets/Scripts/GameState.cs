using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public float dayTime = 60f;
	public float currentTime;
	public int currentDay;

	public float wage = 100;
    public float mortgage = 300;

	//[HideInInspector]
	public float numberOfEmployees;

	public float money;

    public float DayProgress
    {
        get
        {
            return currentTime/dayTime;
        }
    }

    public float Expenses
    {
        get
        {
            return numberOfEmployees * wage + mortgage;
        }
    }
	
	// Use this for initialization
	void Start () 
	{
		currentTime = 0;
		currentDay = 1;
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
		if( currentTime < dayTime)
		{
			currentTime += Time.deltaTime;
		}
		else
		{
			currentTime = 0;
			money -= (numberOfEmployees * wage + mortgage);

			isThisTheEnd();

			++currentDay; 
			//Load next Day
		}
	
	}

	void isThisTheEnd()
	{
		if(money <= 0)
			Debug.Log("Game Over"); //TODO
	}

    void EmployeeDied()
    {
        numberOfEmployees--;
    }
}
