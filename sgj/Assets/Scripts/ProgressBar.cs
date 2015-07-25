using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public Texture progressBarEmpty, progressBarFull;
    public float progress;
    public Vector2 size;
    public Vector2 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () { 
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.y = Screen.height - (pos.y + offset.y);
        pos.x = pos.x + offset.x;
        GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), progressBarEmpty);
        GUI.BeginGroup(new Rect (pos.x, pos.y, size.x * Mathf.Clamp01(progress), size.y));
        GUI.DrawTexture(new Rect(0, 0, size.x, size.y), progressBarFull);
        GUI.EndGroup();
    }

    void ProgressUpdated(float val)
    {
        progress = val;
    }
}
