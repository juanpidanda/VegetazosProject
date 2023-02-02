using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsBehaviours : MonoBehaviour
{


    #region CHANGE SCENE BUTTONS
    public void MainMenuButtonPressed()
    {
        if (GameManagerExists())
        {
            GameManager.gameManagerInstance.CallMainMenuScene();
        }
    }
    public void StartGameButtonPressed()
    {
        if (GameManagerExists())
        {
            GameManager.gameManagerInstance.CallCharacterSelectorScene();
        }
    }

    public void PlayGameButtonPressed()
    {
        if (GameManagerExists())
        {
            GameManager.gameManagerInstance.CallCombatScene();
        }
    }
    public void QuitGameButtonPressed()
    {
        if (GameManagerExists())
        {
            GameManager.gameManagerInstance.QuitGame();
        }
    }
    #endregion

    protected bool GameManagerExists()
    {
        if(GameManager.gameManagerInstance != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
