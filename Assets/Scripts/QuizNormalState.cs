using Unity.VisualScripting;
using UnityEngine;

public class QuizNormalState : QuizBaseState
{

    float baseChance = 0.3f;
    float currentChance = 0.3f;

    public override void EnterState(QuizStateManager quiz) {
        quiz.pipeSpawner.GetComponent<PipeSpawnScript>().isNormalSpawn = true;

        Debug.Log("ENTER: Normal State");
    }

    public override void UpdateState(QuizStateManager quiz) {
        if (quiz.isPipeFlagSet()) {
            if (Random.Range(0.0f, 1.0f) < currentChance) {
                currentChance = baseChance;
                quiz.SwitchState(quiz.promptState);
            } else {
                if (currentChance < 1.0f) {
                    currentChance += 0.1f;
                }
            }
        }
    }
}
