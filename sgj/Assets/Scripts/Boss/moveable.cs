using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {

    public float speed = 0;
	//public float rotSpeed = 20;

    private Rigidbody2D _rigidBody;
    private Vector2 _direction;

    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.W)) {
            _direction = Vector2.up;
        }

        if (Input.GetKey(KeyCode.S)) {
            _direction = Vector2.down;
        }

        if (Input.GetKey(KeyCode.A)) {
            _direction = new Vector2(-1, _direction.y);
        }

        if (Input.GetKey(KeyCode.D)) {
            _direction = new Vector2(1, _direction.y);
        }


        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) {
            _direction = new Vector2(_direction.x, 0);
        }
        
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            _direction = new Vector2(0, _direction.y);
        }
    }

	void FixedUpdate () {
        _rigidBody.velocity = _direction * CalculateSpeed();
	}

    float CalculateSpeed() {
        if (Time.deltaTime > 0) {
            return speed / Time.fixedDeltaTime;
        } else {
            return 0;
        }
    }
}
