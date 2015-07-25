using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;


public class ChooseRange : MonoBehaviour {
    public float coolTime;

    private Cooldown _timeCooler;

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
    }

	void Update () {

        if (!_timeCooler.IsCooling) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _Range.Started = true;

            } else if (Input.GetKeyUp(KeyCode.Space)) {
                _Range.Started = false;
                _Range.CreateCollider();
                Debug.Log("Chosen radius: " + _Range.ActualRadius);
                _timeCooler.StartCooling(coolTime);
            }
        } else {
            _timeCooler.Update(Time.deltaTime);
        }
	}
}
