using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleScreenScript : MonoBehaviour
{
    private float tweenDuration = 1f;
    private float tweenDelay = 0.1f;


    public void hideTitleScreen() {
        gameObject.transform.LeanMoveLocalY(-Screen.height, tweenDuration).setEaseInOutBack().delay = tweenDelay;
    }

}
