using UnityEditor;
using UnityEngine;

public abstract class QuizBaseState
{
    public abstract void EnterState(QuizStateManager quiz);

    public abstract void UpdateState(QuizStateManager quiz);

}
