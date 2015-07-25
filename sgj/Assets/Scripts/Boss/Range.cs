using System.Collections.Generic;
using UnityEngine;
using System.Collections;

using Assets.Scripts.Boss;

public class Range : MonoBehaviour {
    public GameObject circle;
    public float minRange = 0;
    public float maxRange = 1;
    public float time = 1;

    public bool Started {
        get {
            return _started;
        }
        set {
            if (value && !_started) {
                ShowCircle();
            } else if (!value && _started && _circleInstance != null) {
                _circleInstance.GetComponent<Animator>().SetTrigger("Hide");
            }
            _started = value;
        }
    }

    public List<AudioClip> clips;
    AudioSource audioSource;

    public float ActualRadius {
        get {
            return _radius;
        }
        private set {
            _radius = Mathf.Clamp(value, minRange, maxRange);
        }
    }

    private SpriteRenderer _Renderer {
        get {
            if (_spriteRenderer == null && _circleInstance != null) {
                _spriteRenderer = _circleInstance.GetComponent<SpriteRenderer>();
                _spritesSize = _spriteRenderer.bounds.size;
            }
            return _spriteRenderer;
        }
    }

    private SpriteRenderer _spriteRenderer;
    private GameObject _circleInstance;
    private Vector3 _spritesSize;
    private float _radius = 0;
    private float _timer = 0;
    private bool _started;

    private RangeMoveType _MoveType { get; set; }

    public void CreateCollider() {
        var collider = _circleInstance.AddComponent<CircleCollider2D>();
        collider.radius = ActualRadius;
        collider.isTrigger = true;
        int index = Random.Range(0, clips.Count - 1);
        audioSource.clip = clips[index];
        audioSource.Play();
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }
	
	void Update () {
        if (Started) {
            _circleInstance.transform.parent = null;
            _circleInstance.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            if (_MoveType == RangeMoveType.Increase) {
                _timer += Time.deltaTime;
            } else {
                _timer -= Time.deltaTime;
            }

            ActualRadius = RangeChangeFunction();
            UpdateMoveType();

            Debug.Log(_Renderer);
            Debug.Log(_spritesSize);
            if(_spriteRenderer != null)
                _spriteRenderer.transform.localScale = new Vector3(2*ActualRadius / _spritesSize.x, 2*ActualRadius / _spritesSize.y, 1);
        } else {
            _MoveType = RangeMoveType.Increase;
            ActualRadius = minRange;
            _timer = 0;
        }
	}

    void UpdateMoveType() {
        if (_MoveType == RangeMoveType.Increase && Mathf.Abs(ActualRadius - maxRange) < 0.001) {
            _MoveType = RangeMoveType.Decrease;
        } else if (_MoveType == RangeMoveType.Decrease && Mathf.Abs(ActualRadius - minRange) < 0.001) {
            _MoveType = RangeMoveType.Increase;
        }
    }

    float RangeChangeFunction() {
        if (time > 0) {
            return maxRange * Mathf.Abs((Mathf.Sin((_timer / time) * Mathf.PI * 2)));
        } else {
            return 0;
        }
    }

    void ShowCircle() {
        _circleInstance = Instantiate(circle);
        _circleInstance.transform.parent = transform;
        _circleInstance.transform.localPosition = Vector3.forward;
        _circleInstance.GetComponent<SpriteFader>().onDestroy.AddListener(HideCircle);
    }

    void HideCircle() {
        _spriteRenderer = null;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, ActualRadius * transform.localScale.x);
    }
}
