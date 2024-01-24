using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCorpse : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private int deathStrength = 35;
    private float deathSpin = 1;

    void Start()
    {
        // Send corpse flying up
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = Vector2.up * deathStrength;
    }

    void FixedUpdate()
    {
        // Give corpse some rotation
        rigidBody2D.rotation += deathSpin;
    }
}
