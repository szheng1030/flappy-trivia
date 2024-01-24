using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizStateManager : MonoBehaviour
{
    QuizBaseState currentState;
    public QuizNormalState normalState = new QuizNormalState();
    public QuizPromptState promptState = new QuizPromptState();
    public QuizAnswerState answerState = new QuizAnswerState();
    public QuizCooldownState cooldownState = new QuizCooldownState();

    public LogicScript logic;
    public BirdScript bird;

    public GameObject pipeSpawner;

    private int numBirds = 0;
    private int randomBird = 0;
    private GameObject currentBird;

    public int correctGap = 0;

    [SerializeField]
    private Transform birdPrompt;

    [SerializeField]
    private GameObject[] Answer;

    [SerializeField]
    private GameObject BirdOfDeath;

    private GameObject lastJudge;

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

    private int numBirdNames;

    // Start is called before the first frame update
    void Start() {

        numBirds = birdPrompt.childCount;

        for (int i = 0; i < numBirds; i++) {
            birdNames.Add(birdPrompt.GetChild(i).transform.name.ToUpper());
        }

        numBirdNames = birdNames.Count;

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        bird = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();

        currentState = normalState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
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
        correctGap = Random.Range(0, 2);
        randomBird = Random.Range(0, numBirds);

        currentBird = birdPrompt.GetChild(randomBird).gameObject;

        string correctAnswer = currentBird.name.ToUpper();
        string incorrectAnswer = "";
        do {
            incorrectAnswer = birdNames[Random.Range(0, numBirdNames)];
        } while (correctAnswer == incorrectAnswer);

        Answer[0].GetComponent<TextMeshPro>().text = correctGap == 0 ? correctAnswer : incorrectAnswer;
        Answer[1].GetComponent<TextMeshPro>().text = correctGap == 1 ? correctAnswer : incorrectAnswer;

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
        currentBird.LeanMoveX(-64, 2f).setEaseOutExpo();
        Answer[0].LeanMoveX(-48.2f, 2f).setEaseOutExpo();
        Answer[1].LeanMoveX(-48.2f, 2f).setEaseOutExpo();
        StartCoroutine(delayDeactivte(currentBird, 2.1f));
        StartCoroutine(delayDeactivte(Answer[0], 2.1f));
        StartCoroutine(delayDeactivte(Answer[1], 2.1f));
        StartCoroutine(delayDeactivte(lastJudge, 2.1f));
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
            lastJudge = Answer[gapIndex].transform.Find("Correct").gameObject;
            lastJudge.SetActive(true);
        } else {
            Debug.Log("Incorrect...");
            BirdOfDeath.SetActive(true);
            lastJudge = Answer[gapIndex].transform.Find("Incorrect").gameObject;
            lastJudge.SetActive(true);
            //bird.killBird();
        }
    }

    IEnumerator delayDeactivte(GameObject obj, float delay) {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
