using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameBootStrap : MonoBehaviour , ITimerUser
{
    [SerializeField] private HomeTrigger homeTrigger;
    [SerializeField] private List<Transform> cameraBordersTransformList; // 0 - left , 1 - right , 2 - down
    [SerializeField] private CameraController gameCamera;
    [SerializeField] private Player mainPlayer;
    [SerializeField] private View gameView;

    [SerializeField] private int playerHealthStart;

    private TimerHandle _gameTimer;

    private void OnEnable()
    {

        //player subscribe
        mainPlayer.OnGetDamage += gameView.UpdateHealthCount;
        mainPlayer.OnPlayerDead += RegisterPlayerDead;
        mainPlayer.OnConsumeHealth += gameView.UpdateHealthCount;
        mainPlayer.InitCharacter(Mathf.Clamp(playerHealthStart, 1, 3));

        //view subscribe
        gameView.UpdateHealthCount(playerHealthStart);
        gameView.OnRestarnSceneClick += ReloadScene;
        gameView.OnNextSceneClick += LoadNextScene;

        //camera init
        gameCamera.InitCamera(mainPlayer.transform, cameraBordersTransformList[0], cameraBordersTransformList[1], cameraBordersTransformList[2]);

        //home subscribe
        homeTrigger.OnCrossFinish += RegisterPlayerWin;


    }
    private void OnDisable()
    {
        gameView.OnRestarnSceneClick -= ReloadScene;
        gameView.OnNextSceneClick -= LoadNextScene;

        mainPlayer.OnGetDamage -= gameView.UpdateHealthCount;
        mainPlayer.OnGetDamage -= gameView.UpdateHealthCount;
        mainPlayer.OnPlayerDead -= RegisterPlayerDead;

        _gameTimer.OnTimerEnd -= OnEndTimer;

        homeTrigger.OnCrossFinish -= RegisterPlayerWin;

    }

    private void Start()
    {
        SetupGameTimer();
    }

    private void Update()
    {
        UpdateGameTimer();
    }



    private void UpdateGameTimer()
    {
        gameView.UpdateTimer(_gameTimer);
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void LoadNextScene()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings ;
        int currentIDScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((currentIDScene + 1) > sceneCount - 1 ? 0 : ++currentIDScene);
    }

    private void RegisterPlayerDead()
    {
        TimerGlobalManager.StaticClass.StopTimer(_gameTimer, this);
        gameView.ShowEndPanel(EndPanelType.lose);
    }

    private void OnEndTimer()
    {
        mainPlayer.KillPlayer();
    }

    private void RegisterPlayerWin()
    {
        TimerGlobalManager.StaticClass.StopTimer(_gameTimer, this);
        mainPlayer.AcceptFinish();
        gameView.ShowEndPanel(EndPanelType.win);
    }

    private void SetupGameTimer()
    {
        _gameTimer = new TimerHandle();
        _gameTimer.maxTime = 50f;
        _gameTimer.OnTimerEnd += OnEndTimer;

        TimerGlobalManager.StaticClass.SetTimer(_gameTimer, this);
    }
}
