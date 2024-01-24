using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] topPipe;
    [SerializeField] private GameObject[] bottomPipe;
    [SerializeField] private GameObject AnswerPipe;

    private float spawnRate = 2.5f;
    private float answerRate = 2.5f;
    private float timer = 0;
    private int spawnFlag = 0;
    private float heightOffset = 7;

    public bool isNormalSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNormalSpawn) {
            if (timer < spawnRate) {
                timer += Time.deltaTime;
            } else {
                spawnPipe();
                timer = 0;
                spawnFlag = 1;
            }
        } else {
            if (timer < answerRate) {
                timer += Time.deltaTime;
            } else {
                spawnAnswerPipe();
                timer = 0;
                spawnFlag = 1;
            }
        }
    }

    // spawns a random top pipe and bottom pipe with a random offset
    public void spawnPipe() {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        float randomPoint = Random.Range(lowestPoint, highestPoint);

        Instantiate(topPipe[Random.Range(0, topPipe.Length)], new Vector3(transform.position.x, randomPoint, -0.3f), transform.rotation);
        Instantiate(bottomPipe[Random.Range(0, bottomPipe.Length)], new Vector3(transform.position.x, randomPoint, -0.3f), transform.rotation);
    }

    // spawns the answer pipe
    public void spawnAnswerPipe() {
        Instantiate(AnswerPipe, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }

    // public member to get if a pipe has spawned
    public int getSpawnFlag() {
        return spawnFlag;
    }

    // public member to unset the pipe-has-spawned flag
    public void unsetSpawnFlag() {
        spawnFlag = 0;
    }
}
