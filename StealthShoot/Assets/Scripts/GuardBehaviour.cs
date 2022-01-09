using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuardBehaviour : EnemyBehaviour {
    public float regularSpeed;
    public float visionRange;
    public float visionConeAngle;
    public float detectionDuration;
    public float rotationSpeed;
    public bool alerted;
    public Light visionLight;
    public GameObject alarmPanel;
    float detectionTimer = 0;
    float initRotSpeed;

    protected override void Start() {
        base.Start();
        alerted = false;
        visionLight.color = Color.white;
        initRotSpeed = rotationSpeed;
    }

    protected override void Update() {
        GetPlayerPos();
        PatrolMovement();
    }

    void PatrolMovement() {
        if (alerted) {
            if (!References.alarmMode.isAlarmTriggered) {
                FindNearestAlarmPanel();
            } else {
                ChasePlayer();
            }
        }
        else {
            SearchPlayer();
        }
    }

    void SearchPlayer() {

        
        Vector3 lateralOffset = transform.right * Time.deltaTime * rotationSpeed;
        transform.LookAt(transform.position + transform.forward + lateralOffset);
        //rb.velocity = transform.forward * speed;

        if (PlayerInSight()) {
            rotationSpeed = 0f;
            transform.LookAt(playerPos);
            DOTween.Kill(visionLight);

            if (detectionTimer <= 0) {
                visionLight.DOColor(Color.red, detectionDuration);
            }
            else {
                visionLight.DOColor(Color.red, detectionDuration - detectionTimer);
            }

            detectionTimer += Time.deltaTime;

            if (detectionTimer >= detectionDuration) {
                //References.enemySpawner.isActivated = true;
                alerted = true;
                //References.alarmMode.TriggerAlarm();
            }
        }
        else {
            rotationSpeed = initRotSpeed;
            if (!alerted) {
                if (detectionTimer <= 0) {
                    detectionTimer = 0;
                }
                else {
                    DOTween.Kill(visionLight);
                    visionLight.DOColor(Color.white, detectionTimer);
                    detectionTimer -= Time.deltaTime;
                }
            }
        }
    }

    void FindNearestAlarmPanel() {
        enemyNavMeshAgent.SetDestination(alarmPanel.transform.position);
    }

    bool PlayerInSight() {
        bool playerInSight = Vector3.Distance(transform.position, playerPos) <= visionRange 
            && Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle
            && !Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer);

        return playerInSight;
    }
}
