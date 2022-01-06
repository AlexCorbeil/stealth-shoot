using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunProjectile : BulletBehaviour
{
    public LineRenderer railBeam;
    public float beamThickness;
    private RaycastHit[] hitInfoList;


    private void Start() {

        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, References.maxDistanceInLevel, References.wallsLayer);
        float distanceToWall = hitInfo.distance;

        hitInfoList = Physics.SphereCastAll(transform.position, beamThickness, transform.forward, distanceToWall, References.enemyLayer);

        foreach (RaycastHit enemyHitInfo in hitInfoList) {
            HealthController hitHealthController = enemyHitInfo.collider.GetComponent<HealthController>();
            if(hitHealthController != null) {
                hitHealthController.TakeDmg(damage);
            }            
        }

  
        railBeam.SetPosition(0, transform.position);
        railBeam.SetPosition(1, hitInfo.point);
    }
}
