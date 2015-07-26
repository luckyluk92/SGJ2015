using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    public void ToTheGame() {
        Application.LoadLevel("Menu");
    }

    public void Sound() {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
