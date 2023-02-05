using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimersBehaviour : MonoBehaviour
{
    [Header("Timers Text")]
    public TextMeshProUGUI startTimerText;
    public TextMeshProUGUI gameTimerText;
    [Header("Start Timer Settings")]
    public int timeToStart;
    [SerializeField] private float timeLeftToStart;
    [Header("Game Timer Settings")]
    [Tooltip("Setear en segundos")]
    public int maxPlayingTime;
    [SerializeField] private float timeLeftToEndGame;
    private float minutes, seconds;

    private void Awake()
    {
        SetStartTimer();
        SetGameTimer();
    }

    void SetStartTimer()
    {
        startTimerText.gameObject.transform.parent.gameObject.SetActive(true);
        startTimerText.text = "";
        startTimerText.text = timeToStart.ToString();
        timeLeftToStart = timeToStart;

    }

    void SetGameTimer()
    {
        gameTimerText.text = "";
        int minutesToSetTimer = maxPlayingTime/60;
        int secondsToSetTimer = maxPlayingTime -minutesToSetTimer *60;
        gameTimerText.text = string.Format("{0:0}:{1:00}", minutesToSetTimer, secondsToSetTimer);
        timeLeftToEndGame = maxPlayingTime;
    }
    private void Update()
    {
        if(GameManager.gameManagerInstance != null)
        {
            switch (GameManager.gameManagerInstance.GetCurrentSceneState())
            {
                case SceneStates.STARTING:
                    CountDownToStart();
                    if(timeLeftToStart <= 0)
                    {
                        startTimerText.gameObject.transform.parent.gameObject.SetActive(false);
                        GameManager.gameManagerInstance.SetSceneState(SceneStates.PLAYING);
                    }
                    break;
                case SceneStates.PLAYING:
                    GameCountDown();
                    if(timeLeftToEndGame <= 0)
                    {
                        startTimerText.text = "0:00";
                        GameManager.gameManagerInstance.SetSceneState(SceneStates.ENDED);
                    }
                    break;
                case SceneStates.PAUSED:
                    break;
                case SceneStates.ENDED:
                    break;
            }
        }
    }
    public void CountDownToStart()
    {
        timeLeftToStart -= Time.deltaTime;
        startTimerText.text = ((int)timeLeftToStart).ToString();
    }
    public void GameCountDown()
    {
        timeLeftToEndGame -= Time.deltaTime;
        minutes = (int)(timeLeftToEndGame / 60.0f);
        seconds = (int)(timeLeftToEndGame - minutes * 60.0f);
        gameTimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

    }
}
