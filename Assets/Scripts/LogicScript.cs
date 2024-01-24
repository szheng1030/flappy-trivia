using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class LogicScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private TextMeshProUGUI scoreText, bestScoreText;
    private int playerScore, bestScore;

    [SerializeField]
    private BirdScript bird;

    [SerializeField]
    private Rigidbody2D birdRB;

    [SerializeField]
    private Animator birdAnimator;

    [SerializeField]
    private Animator circleWipe;

    [SerializeField]
    private PipeSpawnScript spawner;

    [SerializeField]
    private QuizStateManager quizManager;

    private AudioSource scoreSFX;
    // private AudioSource gameOverSFX;

    private void Start() {
        if (!PlayerPrefs.HasKey("best")) {
            PlayerPrefs.SetInt("best", 0);
        }
        bestScore = PlayerPrefs.GetInt("best");
        bestScoreText.text = bestScore.ToString();

        scoreSFX = transform.GetChild(0).gameObject.GetComponent<AudioSource>();

        // gameOverSFX = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
    }

    [ContextMenu("Increment Score")]
    public void addScore(int scoreToAdd)
    {
        if (bird.birdIsAlive) {
            scoreSFX.Play();
            playerScore += scoreToAdd;
            scoreText.text = playerScore.ToString();

            // Update best score
            if (bestScore < playerScore) {
                bestScore = playerScore;
                bestScoreText.text = bestScore.ToString();
            }
        }
    }

    public void startGame() {
        spawner.enabled = true;
        quizManager.enabled = true;
        birdAnimator.SetTrigger("startBird");
        bird.enabled = true;
        birdRB.gravityScale = 5;
    }

    public void restartGame()
    {
        StartCoroutine(startCircleWipe());
    }

    public void gameOver()
    {
        //gameOverSFX.Play();
        gameOverScreen.SetActive(true);
        PlayerPrefs.SetInt("best", bestScore);
    }

    IEnumerator startCircleWipe() {
        circleWipe.SetTrigger("RetryGame");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
