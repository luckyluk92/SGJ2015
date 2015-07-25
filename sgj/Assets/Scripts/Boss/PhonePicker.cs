using UnityEngine;
using System.Collections;

public class PhonePicker : MonoBehaviour {

    void OnTriggerStay2D(Collider2D collider) {
        if(collider.name == "Phone" && collider.GetComponent<Phone>().isRinging)
        {
            collider.SendMessage("StartTalking");
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider.name == "Phone")
        {
            collider.SendMessage("StopTalking");
        }
    }
}
