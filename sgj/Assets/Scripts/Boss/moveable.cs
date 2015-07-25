using UnityEngine;
using System.Collections;

public class moveable : MonoBehaviour {

    public float speed = 0;
	//public float rotSpeed = 20;

    private Rigidbody2D _rigidBody;

    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.W)) {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, CalculateSpeed());
        } 

        if (Input.GetKey(KeyCode.S)) {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, -CalculateSpeed());
        }

        if (Input.GetKey(KeyCode.A)) {
            _rigidBody.velocity = new Vector2(-CalculateSpeed(), _rigidBody.velocity.y);
        }

        if (Input.GetKey(KeyCode.D)) {
            _rigidBody.velocity = new Vector2(CalculateSpeed(), _rigidBody.velocity.y);
        }
        
        
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
        } else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)){
            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        }

        //if (Input.GetKey(KeyCode.Q)) {
        //    transform.Rotate(new Vector3(0,0,1)*rotSpeed*Time.deltaTime,Space.Self);
        //}
        //if (Input.GetKey(KeyCode.E)) {
        //    transform.Rotate(new Vector3(0,0,-1)*rotSpeed*Time.deltaTime,Space.Self);
        //}
	}

    float CalculateSpeed() {
        if (Time.deltaTime > 0) {
            return speed / Time.fixedDeltaTime;
        } else {
            return 0;
        }
    }
}
