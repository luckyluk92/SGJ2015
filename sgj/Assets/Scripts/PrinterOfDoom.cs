using UnityEngine;
using System.Collections;

public class PrinterOfDoom : MonoBehaviour {

	private float _timeOfSelfAnihillation;
	public float minTime = 10;
	public float maxTime = 30;
	private float _currentTime;

	private int _healthPoints;
    private SpriteRenderer _sprite;
	public int minHealth = 1;
	public int maxHealth = 5;
	

	// Use this for initialization
	void Start () 
	{
        _sprite = GetComponent<SpriteRenderer>();
		RandomizeData();
	}
	
	// Update is called once per frame
	void Update () 
	{
		_currentTime += Time.deltaTime;
        

		if(_currentTime >= _timeOfSelfAnihillation)
		{
			if(_healthPoints == 0) {
				RandomizeData();
            } else {

            }
		}
	}

	void RandomizeData()
	{
		_currentTime = 0;
		_timeOfSelfAnihillation = Random.Range(minTime, maxTime);
		_healthPoints = Random.Range(minHealth, maxHealth);
	}


    IEnumerator HandleHit()
    {
        _sprite.color = new Color(1f, 0f, 0f, 0.4f);
        yield return new WaitForSeconds(0.05f);
        _sprite.color = Color.white;
    }
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.name == "Boss")
		{
			--_healthPoints;
            gameObject.SendMessage("DoShake");
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            var forceVector = -rb.velocity.normalized * 20f;
            rb.AddForce(forceVector, ForceMode2D.Impulse);
            StartCoroutine(HandleHit());
		}
	}
}
