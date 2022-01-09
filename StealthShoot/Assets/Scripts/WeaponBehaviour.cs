using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{    
    public float fireRate;
    float secondsSinceLastShot;
    public GameObject bulletObj;
    public float accuracy;
    public int numsOfProjectiles;

    void Start() {
        secondsSinceLastShot = fireRate;
    }

    void Update() {        
        secondsSinceLastShot += Time.deltaTime;
    }

    public void FireBullet(Vector3 cursorPos) {        
        if(secondsSinceLastShot >= fireRate){
            //Trigger alarm when a bullet is shot
            //References.enemySpawner.isActivated = true;
            References.alarmMode.TriggerAlarm();

            for(int i = 0; i < numsOfProjectiles; i++) {
                GameObject newBullet = Instantiate(bulletObj, transform.position + transform.forward, transform.rotation);
                float inaccuracy = Vector3.Distance(transform.position, cursorPos) / accuracy;
                Vector3 innacuratePos = cursorPos;
                innacuratePos.x += Random.Range(-inaccuracy, inaccuracy);
                innacuratePos.z += Random.Range(-inaccuracy, inaccuracy);
                newBullet.transform.LookAt(innacuratePos);
                    
            }

            secondsSinceLastShot = 0f;
        }
    }
}
