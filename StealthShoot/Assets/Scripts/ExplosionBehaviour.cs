using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public float maxLifespan;
    float currentLifespan;
    public float damage;
    public float explosionRadius;

    void Start() {
        currentLifespan = 0f;
    }

    void FixedUpdate() {
        currentLifespan += Time.deltaTime;
        Explode();        
    }

    void Explode() {
        float lifeFraction = currentLifespan / maxLifespan;
        Vector3 maxScale = Vector3.one * explosionRadius;

        transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, lifeFraction);      

        if(currentLifespan >= maxLifespan) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider) {
        HealthController healthSys = collider.gameObject.GetComponent<HealthController>();
        if(healthSys != null) {
            healthSys.TakeDmg(damage);
        }
    }
}
