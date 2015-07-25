using UnityEngine;
using System.Collections;

public class PrinterOfDoom : MonoBehaviour {

	private float _timeOfSelfAnihilation;
	public float minTime = 10;
	public float maxTime = 30;
	private float _currentTime;

	private int _healthPoints;
	public int minHealth = 1;
	public int maxHealth = 5;
	

	// Use this for initialization
	void Start () 
	{
		RandomizeData();
	}
	
	// Update is called once per frame
	void Update () 
	{
		_currentTime += Time.deltaTime;

		if(_currentTime >= _timeOfSelfAnihilation)
		{
			if(_healthPoints == 0)
				RandomizeData();
			else
				Die();
		}
	}

	void RandomizeData()
	{
		_currentTime = 0;
		_timeOfSelfAnihilation = Random.Range(minTime, maxTime);
		_healthPoints = (int)Random.Range(minHealth, maxHealth);
	}

	void Die()
	{
		_spawnTime += Time.deltaTime;
		if(_spawnTime > shootingIntervals)
		{
			//Some effect
			_spawnTime -= shootingIntervals;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.name == "Boss")
		{
			--_healthPoints;
		}
	}
}
