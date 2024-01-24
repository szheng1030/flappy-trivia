using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCooldownState : QuizBaseState
{

    int cooldownLength = 3;
    int currentCooldown = 0;

    public override void EnterState(QuizStateManager quiz) {
        quiz.pipeSpawner.GetComponent<PipeSpawnScript>().isNormalSpawn = true;
        currentCooldown = 0;

        Debug.Log("ENTER: Cooldown State");
    }

    public override void UpdateState(QuizStateManager quiz) {
        if (quiz.isPipeFlagSet()) {
            currentCooldown++;
        }

        if (currentCooldown >= cooldownLength) {
            quiz.despawnExistingBird();
            quiz.SwitchState(quiz.normalState);
        }
    }
}
