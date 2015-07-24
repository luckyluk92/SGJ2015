using UnityEngine;
using System.Collections;

using Assets.Scripts.Boss;

public class Range : MonoBehaviour {
    public float minRange = 0;
    public float maxRange = 0;
    public float time = 1;

    public bool Started { get; set; }

    public float ActualRadius {
        get {
            return _radius;
        }
        private set {
            _radius = Mathf.Clamp(value, minRange, maxRange);
        }
    }
    private float _radius = 0;
    private float _timer = 0;

    private RangeMoveType _MoveType { get; set; }

    public void CreateCollider() {
        RemoveCollider();
        var collider = gameObject.AddComponent<CircleCollider2D>();
        collider.radius = ActualRadius;
    }

    public void RemoveCollider() {
        Destroy(GetComponent<CircleCollider2D>());
    }
	
	void Update () {
        if (Started) {
            if (_MoveType == RangeMoveType.Increase) {
                _timer += Time.deltaTime;
            } else {
                _timer -= Time.deltaTime;
            }

            ActualRadius = RangeChangeFunction();
            UpdateMoveType();

        } else {
            _MoveType = RangeMoveType.Increase;
            ActualRadius = minRange;
            _timer = 0;
        }
	}

    void UpdateMoveType() {
        if (_MoveType == RangeMoveType.Increase && Mathf.Abs(ActualRadius - maxRange) < 0.001) {
            _MoveType = RangeMoveType.Decrease;
        } else if (_MoveType == RangeMoveType.Decrease && Mathf.Abs(ActualRadius - minRange) < 0.001) {
            _MoveType = RangeMoveType.Increase;
        }
    }

    float RangeChangeFunction() {
        if (time > 0) {
            return maxRange * (Mathf.Cos((_timer / time) * Mathf.PI * 2) + 1) / 2.0f;
        } else {
            return 0;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, ActualRadius * transform.localScale.x);
    }
}
