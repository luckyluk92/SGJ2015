using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class Sleeper : MonoBehaviour {

    public GameObject zzz;
    public int numOfletters;
    public UnityEvent OnAsleep;
    public UnityEvent OnAwakening;
    public float probability = 0;
    public float time = 2;

    private List<GameObject> _lettersList;

    public bool IsSleeping {
        get {
            return _isSleeping;
        }
        set {
            var prev = _isSleeping;
            _isSleeping = value;
            if (value && !_isSleeping) {
                OnAsleep.Invoke();
            } else if (!value && _isSleeping) {
                OnAwakening.Invoke();
            }
            _Animator.SetBool("Sleep", value);
        }
    }

    private Animator _Animator {
        get {
            if (_animator == null) {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }

    private bool _isSleeping;
    private Animator _animator;
    private float _timer;

    void Start() {
        _lettersList = new List<GameObject>();
        for (int i = 0; i < numOfletters; i++) {
            _lettersList.Add(Instantiate(zzz));
            _lettersList.Last().transform.parent = transform;
        }
    }

	void Update () {
        if (_timer >= time && !IsSleeping) {
            _timer = 0;
            if (Random.value <= probability) {
                IsSleeping = true;
                _lettersList = new List<GameObject>();
                for (int i = 0; i < numOfletters; i++) {
                    _lettersList.Add(Instantiate(zzz));
                    _lettersList.Last().transform.parent = transform;
                }
            }
        } else {
            _timer += Time.deltaTime;
            _lettersList.Clear();
        }
	}

    void OnCollisionEnter2D() {
        IsSleeping = false;
    }

}
