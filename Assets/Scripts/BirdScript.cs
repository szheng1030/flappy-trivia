using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{

    private Rigidbody2D myRigidBody;
    public float flapStrength;
    private LogicScript logic;
    public bool birdIsAlive = true;

    public Animator animator;

    private float flapTime = 0.03f;
    private float currentFlap = 0;

    [SerializeField]
    private GameObject corpse;

    [SerializeField]
    private PipeSpawnScript spawner;

    private AudioSource gameOverSFX;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PipeSpawnScript>();

        gameOverSFX = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidBody.velocity = Vector2.up * flapStrength;
            animator.SetBool("IsFlapping", true);
        }

        if (transform.position.y < -22 && birdIsAlive) {
            killBird();
        }

        if (animator.GetBool("IsFlapping")) {
            currentFlap += Time.deltaTime;
            if (currentFlap >= flapTime) {
                currentFlap = 0;
                animator.SetBool("IsFlapping", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        killBird();
    }

    public void killBird() {
        logic.gameOver();
        birdIsAlive = false;
        GetComponent<SpriteRenderer>().enabled = false;
        corpse.SetActive(true);
        gameOverSFX.Play();


        //Destroy(spawner);
    }


}
