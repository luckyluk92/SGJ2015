using UnityEngine;
using System.Collections;

public class MoneyFader : MonoBehaviour {

    private MeshRenderer mesh;
	// Update is called once per frame
	public void Kill () {
        Destroy(gameObject);
	}
}
