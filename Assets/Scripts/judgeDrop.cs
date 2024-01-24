using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judgeDrop : MonoBehaviour
{

    private SpriteRenderer sprite;

    [SerializeField]
    private GameObject smoke;

    // Start is called before the first frame update
    void OnEnable()
    {
        // set initial parameters
        transform.localScale = new Vector3(80, 80, 1);
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = new Color (1f, 1f, 1f, 0f);
        dropIt();
    }

    void dropIt() {
        transform.LeanScaleX(35, 0.3f).setEaseOutExpo();
        transform.LeanScaleY(35, 0.3f).setEaseOutExpo();
        gameObject.LeanColor(new Color(1f, 1f, 1f, 1f), 0.3f).setEaseOutExpo();
        smoke.GetComponent<Animator>().SetTrigger("startSmoke");
        //StartCoroutine(delaySetActive(smoke, 0.1f, true));
        //StartCoroutine(delaySetActive(smoke, 1f, false));
    }

    IEnumerator delaySetActive (GameObject obj, float delay, bool active) {
        yield return new WaitForSeconds(delay);
        obj.SetActive(active);
    }
}
