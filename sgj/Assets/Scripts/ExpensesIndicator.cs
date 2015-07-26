using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExpensesIndicator : MonoBehaviour {

    GameState state;
    Text expensesText;
    
	// Use this for initialization
	void Start () {
        state = Camera.main.GetComponent<GameState>();
        expensesText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        expensesText.text = string.Format("Expected Expenses: {0:C}", Convert.ToDecimal(state.Expenses));
	}
}
