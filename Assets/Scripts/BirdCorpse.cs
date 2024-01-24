using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCorpse : MonoBehaviour
{

    private Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        float deathStrength = GetComponentInParent<BirdScript>().flapStrength * 1.5f;
        rigidBody2D.velocity = Vector2.up * deathStrength;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidBody2D.rotation += 1.0f;
    }
}
