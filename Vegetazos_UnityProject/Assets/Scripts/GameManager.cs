using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScenes
{
    MAINMENU,
    CHARACTERSELECTOR,
    COMBATSCENE,
}
public enum SceneStates
{
    STARTING,
    PLAYING,
    PAUSED,
    ENDED
}
public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance = null;
    [Header("Scene References")]
    [Tooltip("La variable seteada es a donde el juego se ira al darle play.")]
    [SerializeField]private GameScenes currentScene = GameScenes.MAINMENU;
    public SceneStates sceneState = SceneStates.STARTING;


    //[Header("Other Managers")]
    private MenusBehaviour menusManager;
    private void Awake()
    {
        CheckForGameManagerInstance();
    }

    public void CallPause()
    {
        if(menusManager == null)
        {
            menusManager = GameObject.FindGameObjectWithTag("Manager/Menus").GetComponent<MenusBehaviour>();
        }
        switch (sceneState)
        {
            case SceneStates.PLAYING:
                Time.timeScale = 0.0f;
                sceneState = SceneStates.PAUSED;
                menusManager.OpenMenu(menusManager.pausePopUpMenu);
                break;
            case SceneStates.PAUSED:
                Time.timeScale = 1.0f;
                sceneState = SceneStates.PLAYING;
                menusManager.CloseMenu(menusManager.pausePopUpMenu);
                break;
        }
    }

    public GameScenes GetCurrentScene()
    {
        return currentScene;
    }


    #region PUBLIC CALLS
    public void CallMainMenuScene()
    {
        SetNextScene(GameScenes.MAINMENU);
    }
    public void CallCharacterSelectorScene()
    {
        SetNextScene(GameScenes.CHARACTERSELECTOR);
    }
    public void CallCombatScene()
    {
        SetNextScene(GameScenes.COMBATSCENE);
    }
    #endregion


    #region SCENE MANAGEMENT
    void SetNextScene(GameScenes sceneToSet)
    {
        EndCurrentScenePhase();
        currentScene = sceneToSet;
        StartCurrentScenePhase();
    }

    void StartCurrentScenePhase()
    {
        sceneState = SceneStates.STARTING;
        switch (currentScene)
        {
            case GameScenes.MAINMENU:
                LoadSceneByIndex(0);
                break;
            case GameScenes.CHARACTERSELECTOR:
                LoadSceneByIndex(1);
                break;
            case GameScenes.COMBATSCENE:
                LoadSceneByIndex(2);
                sceneState = SceneStates.PLAYING;
                break;
        }
    }

    void EndCurrentScenePhase()
    {
        sceneState = SceneStates.ENDED;
        switch (currentScene)
        {
            case GameScenes.MAINMENU:
                break;
            case GameScenes.CHARACTERSELECTOR:
                break;
            case GameScenes.COMBATSCENE:
                break;
        }
    }

    void LoadSceneByIndex(int sceneID)
    {
        if(SceneManager.GetActiveScene().buildIndex != sceneID)
        {
            SceneManager.LoadScene(sceneID);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        EndCurrentScenePhase();
        Debug.Log("Qutting app...");
        Application.Quit();
    }
    #endregion

    void CheckForGameManagerInstance()
    {
        if(gameManagerInstance == null)
        {
            gameManagerInstance = this;
            DontDestroyOnLoad(gameObject);

            StartCurrentScenePhase();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}