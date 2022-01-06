using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public Image fillBar;
    public Image background;

    public void ShowHealthFraction(float fraction) {

        fillBar.rectTransform.localScale = new Vector3(fraction, 1, 1);
        if (fraction < 1) {
            fillBar.enabled = true;
            background.enabled = true;
        } else {
            fillBar.enabled = false;
            background.enabled = false;
        }
    }
}
