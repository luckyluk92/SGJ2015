using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {

    public float speed = 0;

    private Rigidbody2D _rigidBody;

    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
	void Update () {
        if (Input.GetKey(KeyCode.W)) {
            _rigidBody.velocity = Vector2.up * CalculateSpeed();
        } 

        if (Input.GetKey(KeyCode.S)) {
            _rigidBody.velocity = Vector2.down * CalculateSpeed();
        }

        if (Input.GetKey(KeyCode.A)) {
            _rigidBody.velocity = Vector2.left * CalculateSpeed();
        }

        if (Input.GetKey(KeyCode.D)) {
            _rigidBody.velocity = Vector2.right * CalculateSpeed();
        }
        
        
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
        } else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)){
            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
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
