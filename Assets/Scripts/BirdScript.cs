using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    // Members
    [HideInInspector] public bool birdIsAlive { get; private set; } = true;
    private int flapStrength = 23;
    private int lowerKillZone = -22;

    // Self components
    private Rigidbody2D myRigidBody;
    private Animator animator;
    private AudioSource gameOverSFX;

    // Other components
    [SerializeField] private LogicScript logic;
    [SerializeField] private GameObject corpse;

    void Start()
    {
        // Self component assignments
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        gameOverSFX = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        // Space to jump
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive) {
            myRigidBody.velocity = Vector2.up * flapStrength;
            animator.SetTrigger("birdFlap");
        }

        // Kill bird if fall below screen
        if (transform.position.y < lowerKillZone && birdIsAlive) {
            killBird();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
