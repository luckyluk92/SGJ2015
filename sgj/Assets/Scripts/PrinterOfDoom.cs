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

    public CircleCollider2D stressCollider;
    public GameObject paperPrefab;
    public GameObject paperPoint;
    public float probabilityOfPaperShooting = 0.05f;
    public float frequencyOfShooting = 0.1f;
    public float forceScalar = 1;

    private float _shootingTimer = 0;

    public bool IsShooting { get; private set; }

	// Use this for initialization
	void Start () 
	{
        _sprite = GetComponent<SpriteRenderer>();
        stressCollider.enabled = false;
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

        if (IsShooting) {
            stressCollider.enabled = true;
            if (_shootingTimer >= frequencyOfShooting) {
                ShootPaper();
                _shootingTimer = 0;
            } else {
                _shootingTimer += Time.deltaTime;
            }
        }
	}

	void RandomizeData()
	{
		_currentTime = 0;
		_timeOfSelfAnihillation = Random.Range(minTime, maxTime);
		_healthPoints = Random.Range(minHealth, maxHealth);
        if (Random.value <= probabilityOfPaperShooting) {
            IsShooting = true;
        }
	}


    IEnumerator HandleHit()
    {
        _sprite.color = new Color(1f, 0f, 0f);
        yield return new WaitForSeconds(0.02f);
        _sprite.color = Color.white;
    }
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.name == "Boss")
		{
			--_healthPoints;
            if (_healthPoints == 0) {
                IsShooting = false;
            }

            gameObject.SendMessage("DoShake");
            StartCoroutine(HandleHit());
		}
	}

    void ShootPaper() {
        var paper = Instantiate(paperPrefab);
        paper.transform.position = paperPoint.transform.position;
        paper.transform.Rotate(paperPoint.transform.rotation.eulerAngles + new Vector3(0,0, Random.Range(-30,30)));
        var direction = paper.transform.rotation * new Vector3(0, transform.position.y, 0);

        var sr = paper.GetComponent<SpriteRenderer>();
        var center = sr.bounds.center;
        paper.GetComponent<Rigidbody2D>().AddForceAtPosition(-(new Vector2(direction.x, direction.y)) * (forceScalar + Random.Range(-forceScalar, forceScalar)), paper.transform.position);
        
    }
}
