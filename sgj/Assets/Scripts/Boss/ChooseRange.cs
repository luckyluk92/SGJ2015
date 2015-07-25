using UnityEngine;
using System.Collections;

public class ChooseRange : MonoBehaviour {

    private Range _range;
    private Range _Range {
        get {
            if (_range == null) {
                _range = GetComponent<Range>();
            }
            return _range;
        }
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _Range.Started = true;
            
        } else if (Input.GetKeyUp(KeyCode.Space)){
            _Range.Started = false;
            _Range.CreateCollider();
            Debug.Log("Chosen radius: " + _Range.ActualRadius);
        }
	}
}
