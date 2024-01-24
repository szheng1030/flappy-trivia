using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BirdofDeathScript : MonoBehaviour {
    [SerializeField]
    private Transform victim;
    private float speed = 100.0f;
    private Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Start() {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, victim.position, step);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        float deathStrength = 40;
        rigidBody2D.velocity = new Vector2(15, deathStrength);
        StartCoroutine(alsoDie());
        
    }

    IEnumerator alsoDie() {
        
        float timePassed = 0;
        while (timePassed < 3) {
            rigidBody2D.rotation -= 1.0f;
            timePassed += Time.deltaTime;
            yield return null;
        }
    }
}
