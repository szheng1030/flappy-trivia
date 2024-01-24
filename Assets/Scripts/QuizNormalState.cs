using Unity.VisualScripting;
using UnityEngine;

public class QuizNormalState : QuizBaseState
{
    // Quiz Normal State
    // -> Default pipe spawning state, chance to prompt

    private float baseChance = 0.3f;
    private float currentChance = 0.3f;
    private float incrChance = 0.1f;

    public override void EnterState(QuizStateManager quiz) {
        quiz.pipeSpawner.GetComponent<PipeSpawnScript>().isNormalSpawn = true;

        Debug.Log("ENTER: Normal State");
    }

    public override void UpdateState(QuizStateManager quiz) {
        // For every spawned set or normal pipes
        // Increase chance to spawn a prompt by 10%
        if (quiz.isPipeFlagSet()) {
            if (Random.Range(0.0f, 1.0f) < currentChance) {
                currentChance = baseChance;
                quiz.SwitchState(quiz.promptState);
            } else {
                if (currentChance < 1.0f) {
                    currentChance += incrChance;
                }
            }
        }
    }
}
