using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class LogicScript : MonoBehaviour
{
    [HideInInspector] public bool gameStarted { get; private set; } = false;
    private int playerScore, bestScore;
    private AudioSource scoreSFX;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI scoreText, bestScoreText;

    [SerializeField] private BirdScript bird;
    [SerializeField] private Animator circleWipe;
    [SerializeField] private PipeSpawnScript spawner;
    [SerializeField] private QuizStateManager quizManager;
    [SerializeField] private TitleScreenScript titleScreen;


    void Awake() {
        if (!PlayerPrefs.HasKey("best")) {
            PlayerPrefs.SetInt("best", 0);
        }
        bestScore = PlayerPrefs.GetInt("best");
        bestScoreText.text = bestScore.ToString();

        scoreSFX = gameObject.GetComponent<AudioSource>();
    }

    public void addScore(int scoreToAdd) {
        // Update score if conditions met
        if (bird.birdIsAlive && gameStarted) {
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
        // 1. Hide Title Screen
        // 2. Enable spawners
        // 3. Set flag
        titleScreen.hideTitleScreen();
        spawner.enabled = true;
        quizManager.enabled = true;
        gameStarted = !gameStarted;
    }

    public void restartGame() {
        StartCoroutine(startCircleWipe());
    }

    // Display game over screen, update playerprefs
    public void gameOver() {
        gameOverScreen.SetActive(true);
        PlayerPrefs.SetInt("best", bestScore);
    }

    // Wait for circle wipe for finish before reloading scene
    IEnumerator startCircleWipe() {
        circleWipe.SetTrigger("RetryGame");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
