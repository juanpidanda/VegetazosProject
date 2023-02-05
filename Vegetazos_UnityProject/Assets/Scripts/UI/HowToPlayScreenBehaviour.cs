using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayScreenBehaviour : MonoBehaviour
{
    public GameObject keyBoardScreen, controlsScreen;

    private void OnEnable()
    {
        keyBoardScreen.SetActive(true);
        controlsScreen.SetActive(false);
    }
    public void ChangeControlsScreen()
    {
        if (keyBoardScreen.activeInHierarchy)
        {
            keyBoardScreen.SetActive(false);
            controlsScreen.SetActive(true);
        }
        else
        {
            keyBoardScreen.SetActive(true);
            controlsScreen.SetActive(false);
        }
    }
}
