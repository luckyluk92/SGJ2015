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

	public float ringingChanceThreshold = .9f;
	
    public AudioClip hangupClip;
    public AudioClip pickupClip;
    public AudioClip callingClip;
    public AudioClip talkingClip;

    private AudioSource _source;
	private GameState _gameState;
    private float _timeToLoad = 0;
    private float _timeInterval = 0;
	// Use this for initialization
	void Start () {
		isBossSpeaking = false;
		_gameState = Camera.main.GetComponent<GameState>();
		ringingTime = 5;
		_currentTime = 0;

        _timeInterval = _gameState.dayTime/4f;
		progressBar.GetComponent<StressProgressBar>().isVisible = false;
        _source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        _timeToLoad += Time.deltaTime;
		if(_timeToLoad > _timeInterval && _gameState.currentTime != 0)
		{
            _timeToLoad = 0f;
			float diceThrow = Random.value;
			if(diceThrow > ringingChanceThreshold && !isBossSpeaking && !isRinging) {
				isRinging = true;
                _source.clip = callingClip;
                _source.loop = true;
                _source.Play();
                gameObject.SendMessage("DoShake");
                gameObject.GetComponent<Animator>().SetBool("Calling", true);
            }
		}

		if(isBossSpeaking)
		{
			_currentTime = Time.deltaTime;
			_totalTime += _currentTime;
            ContinueTalking();
		}
		else if(isRinging)
		{
			_currentTime += Time.deltaTime;

			if(_currentTime >= ringingTime)
			{
				isRinging = false;
                _source.Stop();
                _source.loop = false;
                _gameState.money -= -moneyPerSecond/10*ringingTime;
                gameObject.SendMessage("DisplayEarnings", -moneyPerSecond/10*ringingTime);
                gameObject.GetComponent<Animator>().SetBool("Calling", false);
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

    IEnumerator Blab() {
        yield return new WaitForSeconds(1f);
        if(isBossSpeaking) {
            _source.pitch = 1f;
            _source.clip = talkingClip;
            _source.Play();
        }
    }

    void StartTalking()
    {
        _source.Stop();
        _source.loop = false;
        _source.PlayOneShot(pickupClip);
        CalculateSpeakingTime();

        isRinging = false;
        isBossSpeaking = true;
        StartCoroutine(Blab());

        gameObject.GetComponent<Animator>().SetBool("Calling", false);

        progressBar.GetComponent<StressProgressBar>().isVisible = true;
    }

    void ContinueTalking()
    {
        if(Mathf.Abs(_totalTime - speakingTime) < 0.1f)
        {
            _source.Stop();
            _source.pitch = .8f;
            _source.loop = false;
            _source.PlayOneShot(hangupClip);
            _gameState.money += moneyPerSecond*speakingTime;
			gameObject.SendMessage("DisplayEarnings", moneyPerSecond*speakingTime);
            isBossSpeaking = false;
            _totalTime = 0;
            progressBar.GetComponent<StressProgressBar>().isVisible = false;
        }
    }

    void StopTalking()
    {
        if(isBossSpeaking) {
            _source.Stop();
            _source.loop = false;
            _source.PlayOneShot(hangupClip);
            isBossSpeaking = false;
            _totalTime = 0;
            progressBar.GetComponent<StressProgressBar>().isVisible = false;
        }
    }
}
