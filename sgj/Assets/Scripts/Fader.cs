using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

    private MeshRenderer mesh;
	// Update is called once per frame
	public void Kill () {
        Destroy(gameObject);
	}
}
