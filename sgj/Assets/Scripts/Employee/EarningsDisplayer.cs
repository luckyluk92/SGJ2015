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
            mesh.text = string.Format("{0:C}", Convert.ToDecimal(gain));
        }
        else
        {
            mesh.color = Color.red;
            mesh.text = "$0.00";
        }
    }
}
