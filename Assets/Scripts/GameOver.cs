using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Transform gameOverGUI;

    // When enabled, do slide in animation
    void Start() {
        gameOverGUI = gameObject.transform;
        gameOverGUI.localPosition = new Vector2(0, Screen.height);
        gameOverGUI.LeanMoveLocalY(0, 1f).setEaseOutExpo().delay = 0.1f;
    }
}
