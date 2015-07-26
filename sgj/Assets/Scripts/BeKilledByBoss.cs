using UnityEngine;
using System.Collections;

public class BeKilledByBoss : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.name == "Boss") {
            Destroy(gameObject);
        }
    }
}
