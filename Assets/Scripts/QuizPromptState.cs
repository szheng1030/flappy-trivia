using UnityEngine;

public class QuizPromptState : QuizBaseState
{
    // Quiz Prompt State
    // -> Display prompt, spawn 2 more pipes afterwards before requesting answer

    private int promptPipes = 2;
    private int currentPipes = 0;

    public override void EnterState(QuizStateManager quiz) {
        // Spawn a random bird prompt; keep normal pipes spawning
        quiz.pipeSpawner.GetComponent<PipeSpawnScript>().isNormalSpawn = true;
        quiz.spawnRandomBird();

        Debug.Log("ENTER: Prompt State");
    }

    public override void UpdateState(QuizStateManager quiz) {
        // Increment for each set of normal pipe spawn
        if (quiz.isPipeFlagSet()) {
            currentPipes++;
        }

        // If 2 normal pipes have spawned since prompt appearing -> next state
        if (currentPipes >= promptPipes) {
            currentPipes = 0;
            quiz.SwitchState(quiz.answerState);
        }
    }
}
