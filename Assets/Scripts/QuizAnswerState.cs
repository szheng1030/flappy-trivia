using UnityEngine;

public class QuizAnswerState : QuizBaseState
{
    // Quiz Answer State
    // -> Spawn answer pipe

    public override void EnterState(QuizStateManager quiz) {
        quiz.pipeSpawner.GetComponent<PipeSpawnScript>().isNormalSpawn = false;

        Debug.Log("ENTER: Answer State");
    }

    public override void UpdateState(QuizStateManager quiz) {
        if (quiz.isPipeFlagSet()) {
            quiz.SwitchState(quiz.cooldownState);
        }
    }
}
