using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AlarmMode : MonoBehaviour {
    public Light directionalLight;
    public Color calmColor;
    public Color alarmColor;
    public float calmIntensity;
    public float alarmIntensity;
    public bool isAlarmTriggered = false;

    private void Awake() {
        References.alarmMode = this;
    }

    private void Start() {
        directionalLight = GetComponent<Light>();
        directionalLight.color = calmColor;
        directionalLight.intensity = calmIntensity;
    }

    //private void Update() {
    //    TriggerAlarm();
    //}

    public void TriggerAlarm() {
        if (References.enemySpawner.isActivated) {
            if (!isAlarmTriggered) {
                DOTween.Kill(directionalLight);
                directionalLight.color = alarmColor;
                directionalLight.DOIntensity(alarmIntensity, 1f).SetLoops(-1, LoopType.Yoyo);
                isAlarmTriggered = true;
            }
        }
        else {
            DOTween.Kill(directionalLight);
            directionalLight.color = calmColor;
            directionalLight.intensity = calmIntensity;
            isAlarmTriggered = false;
        }
    }

}
