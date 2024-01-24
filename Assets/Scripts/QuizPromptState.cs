using UnityEngine;

public class QuizPromptState : QuizBaseState
{

    public int promptPipes = 2;
    private int currentPipes = 0;

    public override void EnterState(QuizStateManager quiz) {
        quiz.pipeSpawner.GetComponent<PipeSpawnScript>().isNormalSpawn = true;
        quiz.spawnRandomBird();

        Debug.Log("ENTER: Prompt State");
    }

    public override void UpdateState(QuizStateManager quiz) {
        if (quiz.isPipeFlagSet()) {
            currentPipes++;
        }

        if (currentPipes >= promptPipes) {
            currentPipes = 0;
            quiz.SwitchState(quiz.answerState);
        }
    }
}
