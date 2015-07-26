using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    private Canvas _canvas;
    public GameObject panel;

	void Start () 
	{
        _canvas = GetComponent<Canvas>();
        panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(Time.timeScale == 1f) {
                Pause();
            } else {
                Resume();
            }
        }
	}

    public void Pause()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);
    }
    
    public void Resume()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("Menu");
    }
}
