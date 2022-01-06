using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public float killTimer;
    public float damage;
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void Update() {        
        Shrink();
        if(killTimer < 0) {
            DestroyBullet();
        }
    }

    void Shrink() {
        killTimer -= Time.deltaTime;
        if (killTimer < 1) {
            transform.localScale *= killTimer;
        }
    }

    void DestroyBullet() {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        GameObject colObj = collision.gameObject;
        if(colObj.GetComponent<EnemyBehaviour>() != null) {
            HealthController colliderHealth = colObj.GetComponent<HealthController>();
            if(colliderHealth != null) {
                colliderHealth.TakeDmg(damage);
            }

            DestroyBullet();
        }
    }
}
