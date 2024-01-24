using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judgeDrop : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private GameObject smoke;

    void OnEnable() {
        // set initial parameters
        transform.localScale = new Vector3(80, 80, 1);
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = new Color (1f, 1f, 1f, 0f);

        // Slide in anim + smoke anim
        transform.LeanScaleX(35, 0.3f).setEaseOutExpo();
        transform.LeanScaleY(35, 0.3f).setEaseOutExpo();
        gameObject.LeanColor(new Color(1f, 1f, 1f, 1f), 0.3f).setEaseOutExpo();
        smoke.GetComponent<Animator>().SetTrigger("startSmoke");
    }
}
