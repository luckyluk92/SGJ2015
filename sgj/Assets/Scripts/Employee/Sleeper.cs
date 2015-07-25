using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class Sleeper : MonoBehaviour {

    public GameObject zzz; //ZZZzzZzzzZZzzzzZZzz!!!
    public UnityEvent OnAsleep;
    public UnityEvent OnAwakening;
    public float probability = 0;
    public float time = 2;

    public bool IsSleeping {
        get {
            return _isSleeping;
        }
        set {
            var prev = _isSleeping;
            _isSleeping = value;
            if (value && !prev) {
                _ParticleSystem.Play();
                OnAsleep.Invoke();
            } else if (!value && prev) {
                _ParticleSystem.Stop();
                _ParticleSystem.Clear();
                OnAwakening.Invoke();
            }
        }
    }

    private ParticleSystem _ParticleSystem {
        get {
            if (_particleSystem == null && _zzzInstance != null) {
                _particleSystem = _zzzInstance.GetComponent<ParticleSystem>();
            }
            return _particleSystem;
        }
    }

    private bool _isSleeping;
    private float _timer;
    private ParticleSystem _particleSystem;
    private GameObject _zzzInstance;

    void Start() {
        _zzzInstance = Instantiate(zzz);
        _zzzInstance.transform.parent = transform;
        _zzzInstance.transform.localPosition = Vector3.zero;
    }

	void Update () {
        if (_timer >= time && !IsSleeping) {
            _timer = 0;
            if (Random.value <= probability) {
                IsSleeping = true;
            }
        } else {
            _timer += Time.deltaTime;
        }
	}

    void OnCollisionEnter2D() {
        IsSleeping = false;
    }

}
