using UnityEngine;
using System.Collections;

public class employee : MonoBehaviour {

	public int productivityDecreaseTime = 1;
	public int productivityDecreateValue = 1;
	public int productivity = 10;
	public int unproductivityThreshold = 3;
	public int productiveGain = 2;
	public int unproductiveGain = 1;
	bool isProductive;
	float timeBetweenGain;

	static int stress;
	

	public Sprite normal, unproductive, doingNothing;
	// Use this for initialization
	void Start () 
	{
		timeBetweenGain = 0;
		stress = 0;
		gameObject.GetComponent<SpriteRenderer>().sprite = normal;
		isProductive = true;
	}
	
	// Update is called once per frame
	void Update () 
	{

		DecreaseProductivity();

		Debug.Log(timeBetweenGain + " " + productivity + " " ); 

		if(stress > 5)
		{
			//Employee will shit bricks here
		}
	}

	void DecreaseProductivity()
	{
		timeBetweenGain += Time.deltaTime;
		
		if(productivity > 0)
		{
			if(timeBetweenGain > productivityDecreaseTime)
			{
				timeBetweenGain -= productivityDecreaseTime;
				productivity -= productivityDecreateValue;
				
				GainMoney();
				
				if(productivity <= unproductivityThreshold)
					isProductive = false;
				else
					isProductive = true;
			}
		}
		else
			gameObject.GetComponent<SpriteRenderer>().sprite = doingNothing;
	}

	void GainMoney()
	{
		if(isProductive)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = normal;
			//gain money here
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = unproductive;
			//gain less money here
		}
			

	}


}

