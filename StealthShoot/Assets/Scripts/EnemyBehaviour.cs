using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    protected GameObject playerObj;
    public float chaseSpeed;
    public float damage;
    protected Rigidbody rb;
    protected Vector3 playerPos;
    protected Vector3 vectorToPlayer;
    protected NavMeshAgent enemyNavMeshAgent;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody>();
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        playerObj = References.thePlayer;
    }

    protected virtual void Update() {
        GetPlayerPos();
        ChasePlayer();
    }

    protected void GetPlayerPos() {
        if(playerObj != null) {
            playerPos = playerObj.transform.position;
            vectorToPlayer = playerObj.transform.position - transform.position;
        }
    }

    protected void ChasePlayer() {
        if (playerObj != null) {
            enemyNavMeshAgent.SetDestination(playerPos);
            
            //rb.velocity = vectorToPlayer.normalized * chaseSpeed;
            //transform.LookAt(playerPos);
        }
    }

    protected void OnCollisionEnter(Collision collision) {
        GameObject colObj = collision.gameObject;
        if(colObj.GetComponent<PlayerBehaviour>() != null) {
            HealthController colliderHealth = colObj.GetComponent<HealthController>();
            if(colliderHealth != null) {
                colliderHealth.TakeDmg(damage);
            }
        }
    }
}
