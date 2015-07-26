using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class employee : MonoBehaviour {
	
	public float productivityDecreaseTime = 1f;
	private float productivityLoss;
	public float minProductivityLoss = 0.01f;
	public float maxProductivityLoss = 0.1f;
    
	
	public float productivityGain = 0.3f;
    public float productivityGainByCollision = 0.1f;
	public float stressLoss = 0.05f;
	public float stressGain = 0.3f;
    public float stressGainByCollision = 0.5f;
	
	public float flatMoneyGain = 20f;
	
	private float _timeBetweenGain;
	
	public float initialProductivity = 1f;
	private float _productivity;
	
	public float initialStress = 0f;
	private float _stress;
	
	public Color productiveColor = new Color(0f, 1f, 0f);
	public GameObject progressBar;

    public List<AudioClip> soundsOfGettingHit;
    private AudioSource _audioSource;
	
	private SpriteRenderer _spriteRenderer;
	private GameState _gameState;
	
	private bool _isBeingCreated;
    private Sleeper _sleeper;

    private ParticleSystem _particleSystem;

    private Sleeper _Sleeper {
        get {
            if (_sleeper == null) {
                _sleeper = GetComponent<Sleeper>();
            }
            return _sleeper;
        }
    }
	
	public bool IsProductive
	{
		get
		{
			return _productivity > 0;
		}
	}
	
	public void ScreamedAt() {
		_productivity = Mathf.Clamp(_productivity + productivityGain, 0, initialProductivity);
		_stress = Mathf.Clamp(_stress+stressGain, 0, 1f);
		progressBar.SendMessage("ProgressUpdated", _stress);
        gameObject.SendMessage("DoShake");
	}
	
	// Use this for initialization
	void Start () 
	{
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _gameState = Camera.main.GetComponent<GameState>();
        _spriteRenderer.color = productiveColor;
        _productivity = initialProductivity;
        _stress = initialStress;
		_isBeingCreated = true;
		CalculateProductivity();
        _Sleeper.OnAwakening.AddListener(OnAwakening);
        _audioSource = GetComponent<AudioSource>();
        _particleSystem = GetComponent<ParticleSystem>();
	}
	
    IEnumerator Dieing() {
        if(!_particleSystem.isPlaying)
            _particleSystem.Play();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        _gameState.SendMessage("EmployeeDied");
    }

	// Update is called once per frame
	void Update () 
	{
		if(_isBeingCreated) //It's crap, but it works.
		{
			_isBeingCreated = false;
			++_gameState.numberOfEmployees;
		}
		GainMoney();
		DecreaseProductivity();
		
		if(_stress == 1f)
		{
           StartCoroutine(Dieing());
		}
		else
		{
			_stress = Mathf.Clamp(_stress - stressLoss*Time.deltaTime, 0, 1f);
			progressBar.SendMessage("ProgressUpdated", _stress);
            if (_Sleeper.IsSleeping) {
                _stress = Mathf.Clamp((_stress - stressLoss * Time.deltaTime), 0, 1f);
                DecreaseProductivity();
            }
		}
	}

	void CalculateProductivity()
	{
		productivityLoss = Random.Range(minProductivityLoss, maxProductivityLoss);
	}

	void DecreaseProductivity()
	{
        var tColor = productiveColor * _productivity;
        tColor.a = 1f;
        var newColor = Color.Lerp(_spriteRenderer.color, tColor, 1 - _productivity/initialProductivity);

        _spriteRenderer.color = newColor;
		
		if(_productivity > 0)
		{
            _productivity -= productivityLoss*Time.deltaTime;
		}
	}
	
	void GainMoney()
	{
		_timeBetweenGain += Time.deltaTime;
		
		if(_timeBetweenGain > productivityDecreaseTime)
		{
			_timeBetweenGain = 0;
			
			var gain = flatMoneyGain * _productivity/initialProductivity;
			_gameState.money += gain;
			gameObject.SendMessage("DisplayEarnings", gain);
		}
	}
	
	void OnTriggerEnter2D() {
        ScreamedAt();

        if (!_Sleeper.IsSleeping) {
            CalculateProductivity();
        } else {
            productivityLoss = 0; 
        }
	}

    void OnAwakening() {
        _stress = Mathf.Clamp(_stress + 0.1f * _stress, 0, 1f);
    }

    IEnumerator Bleed() {
        if(!_particleSystem.isPlaying) {
            _particleSystem.Play();
        }
        yield return new WaitForSeconds(0.4f);
        _particleSystem.Stop();
        _particleSystem.time = 0f;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if(coll.gameObject.name == "Boss") {
            _audioSource.clip = soundsOfGettingHit[Random.Range(0, soundsOfGettingHit.Count -1)];
            _audioSource.Play();
            if(!_Sleeper.IsSleeping) {
                _productivity = Mathf.Clamp01(_productivity + productivityGainByCollision);
                DecreaseProductivity();
                _stress = Mathf.Clamp01(_stress + stressGainByCollision);
                progressBar.SendMessage("ProgressUpdated", _stress);
                if(_stress < 1f) 
                    StartCoroutine(Bleed());
            }
        }
    }
}
