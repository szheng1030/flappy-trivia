using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BirdofDeathScript : MonoBehaviour {

    [SerializeField]private Transform victim;
    private Rigidbody2D rigidBody2D;

    private Vector2 deathStrength = new Vector2(15, 40);
    private float launchSpeed = 100.0f;
    private float spinStrength = 1.0f;
    private int spinSeconds = 3;

    void Start() {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
        float step = launchSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, victim.position, step);
    }

    // Send flying when collide with player bird
    private void OnCollisionEnter2D(Collision2D collision) {
        rigidBody2D.velocity = deathStrength;
        StartCoroutine(spinWhileDying());
    }

    // Add a spin to dying animation
    IEnumerator spinWhileDying() {
        float timePassed = 0;
        while (timePassed < spinSeconds) {
            rigidBody2D.rotation -= spinStrength;
            timePassed += Time.deltaTime;
            yield return null;
        }
    }
}
