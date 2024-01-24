using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    private Transform gameOverGUI;

    // Start is called before the first frame update
    void Start()
    {
        gameOverGUI = gameObject.transform;
        gameOverGUI.localPosition = new Vector2(0, Screen.height);
        gameOverGUI.LeanMoveLocalY(0, 1f).setEaseOutExpo().delay = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
