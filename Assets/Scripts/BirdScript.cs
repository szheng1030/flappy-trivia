using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86.Avx;

public class BirdScript : MonoBehaviour {
    // Members
    [HideInInspector] public bool birdIsAlive { get; private set; } = true;
    private int flapStrength = 23;
    private int lowerKillZone = -22;
    private int birdGravity = 5;


    // Self components
    private Rigidbody2D rigidBody;
    private PlayerInput playerInput;
    private Animator animator;
    private AudioSource gameOverSFX;
    
    // Other components
    [SerializeField] private LogicScript logic;
    [SerializeField] private GameObject corpse;

    private void Awake() {
        // Self assigns
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = gameObject.GetComponent<Animator>();
        gameOverSFX = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable() {
        playerInput.actions["Jump"].performed += Jump;
    }

    private void OnDisable() {
        playerInput.actions["Jump"].performed -= Jump;
    }

    private void Jump(InputAction.CallbackContext context) {
        Debug.Log(logic.gameStarted);
        if (!logic.gameStarted) {
            // If game hasn't started, start game
            // Enable gravity on bird and disable looping animation
            animator.SetTrigger("startBird");
            rigidBody.gravityScale = birdGravity;
            logic.startGame();
        } else {
            if (birdIsAlive) {
                rigidBody.velocity = Vector2.up * flapStrength;
                animator.SetTrigger("birdFlap");
            } else {
                logic.restartGame();
            }
        }
    }

    void Update() {
        // Kill bird if fall below screen
        if (transform.position.y < lowerKillZone && birdIsAlive) {
            killBird();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Excluding skybox, all collisions should kill player
        if (collision.gameObject.tag != "Skybox" && birdIsAlive) {
            killBird();
        }
    }

    public void killBird() {
        // Game over handling
        birdIsAlive = false;
        logic.gameOver();
        gameOverSFX.Play();
        
        // Spawn corpse gameobject; hid original player sprite
        GetComponent<SpriteRenderer>().enabled = false;
        corpse.SetActive(true);
    }


}
