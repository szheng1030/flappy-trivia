using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    private RawImage img;
    private float x = 0.07f;

    void Start() {
        img = gameObject.GetComponent<RawImage>();
    }

    // Scrolling background
    void Update() {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x, 0) * Time.deltaTime, img.uvRect.size);
    }
}
