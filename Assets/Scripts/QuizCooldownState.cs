using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCooldownState : QuizBaseState
{
    // Quiz Cooldown State
    // -> Guarantee to spawn at least 3 normal pipes after answer

    private int cooldownLength = 3;
    private int currentCooldown = 0;

    public override void EnterState(QuizStateManager quiz) {
        quiz.pipeSpawner.GetComponent<PipeSpawnScript>().isNormalSpawn = true;
        currentCooldown = 0;

        Debug.Log("ENTER: Cooldown State");
    }

    public override void UpdateState(QuizStateManager quiz) {
        if (quiz.isPipeFlagSet()) {
            currentCooldown++;
        }

        // Add delay to the prompt disappearing post-answer
        if (currentCooldown >= cooldownLength) {
            quiz.despawnExistingBird();
            quiz.SwitchState(quiz.normalState);
        }
    }
}
