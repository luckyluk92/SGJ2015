using UnityEngine;
using System.Collections;
using ProgressBar;

public class DayCompletionProgress : MonoBehaviour {

    GameState state;
    ProgressBarBehaviour bar;

	// Use this for initialization
	void Start () {
        state = Camera.main.GetComponent<GameState>();
        bar = gameObject.GetComponent<ProgressBarBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        bar.Value = state.DayProgress * 100;
	}
}
