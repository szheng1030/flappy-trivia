using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerMiddleScript : MonoBehaviour
{
    public QuizStateManager quiz;

    // Start is called before the first frame update
    void Start() {
        quiz = GameObject.FindGameObjectWithTag("Quiz").GetComponent<QuizStateManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            int gapIndex = transform.name == "UpperGap" ? 0 : 1;
            if (transform.name == "UpperGap") {
                Debug.Log("Upper, index " + gapIndex);
            } else {
                Debug.Log("Lower, index " + gapIndex);
            }
            quiz.judgeAnswer(gapIndex);
        }
    }
}
