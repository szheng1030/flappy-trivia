using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizStateManager : MonoBehaviour
{
    // State Machine
    QuizBaseState currentState;
    public QuizNormalState normalState = new QuizNormalState();
    public QuizPromptState promptState = new QuizPromptState();
    public QuizAnswerState answerState = new QuizAnswerState();
    public QuizCooldownState cooldownState = new QuizCooldownState();

    [SerializeField] private LogicScript logic;
    [SerializeField] private BirdScript bird;
    public PipeSpawnScript pipeSpawner;

    private int numBirds = 0;
    private GameObject currentBird;
    private int correctGap = 0;
    private int numAnswers = 2;
    private GameObject lastJudgeObject;
    private int numBirdNames;

    [SerializeField] private Transform birdPrompt;
    [SerializeField] private GameObject[] Answer;
    [SerializeField] private GameObject BirdOfDeath;

    private List<string> birdNames = new List<string>() {
        "HOATZIN",
        "MAGNIFICENT FRIGATEBIRD",
        "VULTURINE GUINEAFOWL",
        "TAWNY FROGMOUTH",
        "GREAT POTOO",
        "JACANAS",
        "KING OF SAXONY BIRD OF PARADISE",
        "SPECTACLED EIDER",
        "WESTERN PAROTIA",
        "GREAT BUSTARD",
        "AMAZONIAN ROYAL FLYCATCHER",
        "KAKAPO",
        "NORTHERN ROYAL ALBATROSS",
        "AARAKOCRA",
        "RHINOCEROS HORNBILL"
    };

    void Start() {

        // Add actual birds to list of bird names
        numBirds = birdPrompt.childCount;
        for (int i = 0; i < numBirds; i++) {
            birdNames.Add(birdPrompt.GetChild(i).transform.name.ToUpper());
        }
        numBirdNames = birdNames.Count;

        // State Initialization
        currentState = normalState;
        currentState.EnterState(this);
    }

    void Update()
    {
        if (bird.birdIsAlive) {
            currentState.UpdateState(this);
        }
    }

    public void SwitchState(QuizBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void spawnRandomBird() {
        // Generate a random Bird and random correct gap (top/bottom)
        correctGap = Random.Range(0, numAnswers);
        currentBird = birdPrompt.GetChild(Random.Range(0, numBirds)).gameObject;

        // Set the answer texts
        string correctAnswer = currentBird.name.ToUpper();
        string incorrectAnswer = "";
        do {
            incorrectAnswer = birdNames[Random.Range(0, numBirdNames)];
        } while (correctAnswer == incorrectAnswer);
        Answer[0].GetComponent<TextMeshPro>().text = correctGap == 0 ? correctAnswer : incorrectAnswer;
        Answer[1].GetComponent<TextMeshPro>().text = correctGap == 1 ? correctAnswer : incorrectAnswer;

        // Generate the prompt and answer texts
        currentBird.SetActive(true);
        currentBird.transform.position = new Vector3(64, 0, 1);
        currentBird.LeanMoveX(0, 2f).setEaseOutExpo();

        Answer[0].SetActive(true);
        Answer[0].transform.position = new Vector3(79.8f, 3, 0);
        Answer[0].LeanMoveX(15.8f, 2f).setEaseOutExpo();

        Answer[1].SetActive(true);
        Answer[1].transform.position = new Vector3(79.8f, -7, 0);
        Answer[1].LeanMoveX(15.8f, 2f).setEaseOutExpo();

        Debug.Log("Correct Answer = " + correctGap);
    }

    public void despawnExistingBird() {
        // Animation to hide prompt
        currentBird.LeanMoveX(-64, 2f).setEaseOutExpo();
        Answer[0].LeanMoveX(-48.2f, 2f).setEaseOutExpo();
        Answer[1].LeanMoveX(-48.2f, 2f).setEaseOutExpo();
        
        // Delay deactivation until after animation completes
        StartCoroutine(delayDeactivte(currentBird, 2.1f));
        StartCoroutine(delayDeactivte(Answer[0], 2.1f));
        StartCoroutine(delayDeactivte(Answer[1], 2.1f));
        StartCoroutine(delayDeactivte(lastJudgeObject, 2.1f));
    }

    public bool isPipeFlagSet() {
        if (pipeSpawner.GetComponent<PipeSpawnScript>().getSpawnFlag() == 1) {
            pipeSpawner.GetComponent<PipeSpawnScript>().unsetSpawnFlag();
            return true;
        } else {
            return false;
        }
    }

    public void judgeAnswer(int gapIndex) {
        if (gapIndex == correctGap) {
            logic.addScore(5);
            Debug.Log("Correct!");
            lastJudgeObject = Answer[gapIndex].transform.Find("Correct").gameObject;
            lastJudgeObject.SetActive(true);
        } else {
            Debug.Log("Incorrect...");
            BirdOfDeath.SetActive(true);
            lastJudgeObject = Answer[gapIndex].transform.Find("Incorrect").gameObject;
            lastJudgeObject.SetActive(true);
        }
    }

    IEnumerator delayDeactivte(GameObject obj, float delay) {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
