using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayIndicator : MonoBehaviour {

    GameState state;
    Text dayText;
    
	// Use this for initialization
	void Start () {
        state = Camera.main.GetComponent<GameState>();	
        dayText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	  dayText.text = "DAY " + state.currentDay.ToString();
	}
}
