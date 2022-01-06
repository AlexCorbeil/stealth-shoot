using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthController : MonoBehaviour
{
    [FormerlySerializedAs("hp")]
    public float maxHp;
    float currentHealth;
    public GameObject healthbarPrefab;
    public GameObject deathEffectPrefab;
    HealthBarBehaviour myHealthBar;
    Camera mainCam;

    void Start() {
        GameObject healthBarObj = Instantiate(healthbarPrefab, References.canvas.transform);
        myHealthBar = healthBarObj.GetComponent<HealthBarBehaviour>();
        currentHealth = maxHp;
        mainCam = Camera.main;
        UpdateHealth();
    }

    void Update() {
        FollowChar();
    }

    void FollowChar() {
        myHealthBar.transform.position = mainCam.WorldToScreenPoint(transform.position) + Vector3.up * 30;
    }

    void UpdateHealth() {
        if(myHealthBar != null) {
            myHealthBar.ShowHealthFraction(currentHealth / maxHp);
        }
    }

    public void TakeDmg(float dmgAmount) {
        if(currentHealth >= 0) {
            currentHealth -= dmgAmount;
            UpdateHealth();

            if(currentHealth <= 0) {
                if(deathEffectPrefab != null){
                    Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy() {
        if(myHealthBar != null) {
            Destroy(myHealthBar.gameObject);
        }
    }
}
