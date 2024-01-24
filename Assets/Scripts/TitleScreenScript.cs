using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenScript : MonoBehaviour
{
    private float tweenDuration = 1f;
    private float tweenDelay = 0.1f;

    // Move title screen components down after pressing start
    public void hideTitleScreen() {
        gameObject.transform.LeanMoveLocalY(-Screen.height, tweenDuration).setEaseInOutBack().delay = tweenDelay;
    } 

}
