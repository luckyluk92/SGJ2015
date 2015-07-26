using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public IEnumerator QuitGameCoroutine() {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    public IEnumerator StartGameCoroutine() {
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("Main");
    }

    public void QuitGame() {
        StartCoroutine(QuitGameCoroutine());
    }

    public void StartGame() {
        StartCoroutine(StartGameCoroutine());
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey("escape")){
            QuitGame();
        }
	}
}
