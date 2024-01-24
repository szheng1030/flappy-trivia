using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    [SerializeField] private LogicScript logic;
    private int birdLayer = 3;

    void Start() {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Increment score when detect collision with bird
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == birdLayer) {
            logic.addScore(1);
        }
    }
}
