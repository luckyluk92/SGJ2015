using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;


public class ChooseRange : MonoBehaviour {
    public float coolTime;

    private Cooldown _timeCooler;
    private bool _wasDown;
    private SpriteRenderer _renderer;

    private Range _range;
    private Range _Range {
        get {
            if (_range == null) {
                _range = GetComponent<Range>();
            }
            return _range;
        }
    }

    void Start() {
        _timeCooler = new Cooldown();
        _renderer = GetComponent<SpriteRenderer>();
    }

	void Update () {

        if (!_timeCooler.IsCooling) {
            if (Input.GetKeyDown(KeyCode.Space) && !_wasDown) {
                _Range.Started = true;
                _wasDown = true;

            } else if (Input.GetKeyUp(KeyCode.Space) && _wasDown) {
                _Range.Started = false;
                _wasDown = false;
                _Range.CreateCollider();
                _timeCooler.StartCooling(coolTime);
                _renderer.color = Color.black;
            }
        } else {
            _timeCooler.Update(Time.deltaTime);
            _renderer.color = Color.Lerp(_renderer.color, Color.red, 2*Time.deltaTime);
        }
	}
}
