using System;
using UnityEngine;
using System.Collections;

public class EarningsDisplayer : MonoBehaviour {

    public GameObject textPrefab;

    void DisplayEarnings(float gain)
    {
        var newPos = transform.position + new Vector3(0, 0.1f, 0);
        var obj = Instantiate(textPrefab, newPos, Quaternion.identity) as GameObject;
        obj.transform.parent = gameObject.transform;
        TextMesh mesh = obj.GetComponent<TextMesh>();
        if(gain > 0)
        {
            mesh.color = Color.green;
            if(gain < 50) {
                mesh.text = "$";
                mesh.characterSize = .3f;
            } else {
                mesh.text = "$$";
                mesh.characterSize = .45f;
            }
        }
        else if (gain == 0) {
            GameObject.Destroy(obj);
        }
        else
        {
            mesh.color = Color.red;
            if(gain > -50) {
                mesh.text = "$";
                mesh.characterSize = .3f;
            } else {
                mesh.text = "$$";
                mesh.characterSize = .45f;
            }
        }
    }
}
