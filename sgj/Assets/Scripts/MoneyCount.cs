using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyCount : MonoBehaviour {

    GameState state;
    Text moneyText;

	// Use this for initialization
	void Start () {
        state = Camera.main.GetComponent<GameState>();
        moneyText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        moneyText.text = string.Format("Money: {0:C}", Convert.ToDecimal(state.money));
	}
}
