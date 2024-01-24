using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenScript : MonoBehaviour
{

    private Transform titleScreen;

    public void hideTitleScreen() {
        titleScreen = gameObject.transform;
        titleScreen.LeanMoveLocalY(-Screen.height, 1f).setEaseInOutBack().delay = 0.1f;
    } 

}
