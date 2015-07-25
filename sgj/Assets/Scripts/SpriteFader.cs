using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SpriteFader : MonoBehaviour {

    public UnityEvent onDestroy;

    public void Kill() {
        Destroy(gameObject);
        onDestroy.Invoke();
    }
}
