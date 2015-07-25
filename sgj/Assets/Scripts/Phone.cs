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

    private AudioSource _source;
	private GameState _gameState;
	// Use this for initialization
	void Start () {
		isBossSpeaking = false;
		_gameState = Camera.main.GetComponent<GameState>();
		ringingTime = 5;
		_currentTime = 0;

		progressBar.GetComponent<StressProgressBar>().isVisible = false;
        _source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((int)(_gameState.DayProgress*100) % 25 == 0 && _gameState.currentTime != 0)
		{
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

    void StartTalking()
    {
        _source.Stop();
        _source.loop = false;
        _source.PlayOneShot(pickupClip);
        CalculateSpeakingTime();

        isRinging = false;
        isBossSpeaking = true;
        gameObject.GetComponent<Animator>().SetBool("Calling", false);

        progressBar.GetComponent<StressProgressBar>().isVisible = true;
    }

    void ContinueTalking()
    {
        if(Mathf.Abs(_totalTime - speakingTime) < 0.1f)
        {
            _source.Stop();
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
