using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayButtons : ButtonsBehaviours
{
    
    public void PauseButtonPressed()
    {
        if (GameManagerExists())
        {
            GameManager.gameManagerInstance.CallPause();
        }
    }
    public void RestartButtonPressed()
    {
        if (GameManagerExists())
        {
            GameManager.gameManagerInstance.ReloadScene();
        }
    }
}
