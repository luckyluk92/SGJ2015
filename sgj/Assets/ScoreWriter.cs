using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameData;

public class ScoreWriter : MonoBehaviour {

    private int score;
	// Use this for initialization
	void Start () {
        score = ScoreKeeper.Instance.Days;
        gameObject.GetComponent<Text>().text = "GAME OVER\nYou survived " + score.ToString() + " days";
	}
}
