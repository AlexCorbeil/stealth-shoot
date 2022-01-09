using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmPanel : MonoBehaviour
{
    [SerializeField]
    float interactionTimer;
    bool isPlayerInsideCollider = false;

    IEnumerator TriggerAlarm() {

        yield return new WaitForSeconds(interactionTimer);
        print("Alarm Triggered by a guard!");

        References.alarmMode.TriggerAlarm();
    }

    private void Update() {
        if (Input.GetButtonDown("Interact")) {
            if(isPlayerInsideCollider && References.alarmMode.isAlarmTriggered) {
                References.alarmMode.DeactivateAlarm();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        GameObject colObj = other.gameObject;
        
        if(colObj.tag == "Player") {
            print("Player is at the alarm panel");
            isPlayerInsideCollider = true;
        }

        if(colObj.tag == "Enemy") {
            StartCoroutine(TriggerAlarm());
        }
    }
}
