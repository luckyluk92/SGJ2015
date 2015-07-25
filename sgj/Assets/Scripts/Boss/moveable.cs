using UnityEngine;
using System.Collections;

public class moveable : MonoBehaviour {

    public float speed = 0;
	public float rotSpeed = 20;

	void Update () {
        if (Input.GetKey(KeyCode.W)) {
            transform.position += new Vector3(0, CalculateSpeed(), 0);
        }

        if (Input.GetKey(KeyCode.S)) {
            //transform.position += new Vector3(0, -CalculateSpeed(), 0);
			transform.Translate(new Vector3(0, -CalculateSpeed(), 0));
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.position += new Vector3(-CalculateSpeed(), 0, 0);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.position += new Vector3(CalculateSpeed(), 0, 0);
        }
		if (Input.GetKey(KeyCode.Q)) {
			transform.Rotate(new Vector3(0,0,1)*rotSpeed*Time.deltaTime,Space.Self);
		}
		if (Input.GetKey(KeyCode.E)) {
			transform.Rotate(new Vector3(0,0,-1)*rotSpeed*Time.deltaTime,Space.Self);
		}
	}

    float CalculateSpeed() {
        if (Time.deltaTime > 0) {
            return speed / Time.deltaTime;
        } else {
            return 0;
        }
    }
}
