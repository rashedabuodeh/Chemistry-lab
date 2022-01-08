using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _startBt;
    [SerializeField] private Button _timerBt;
    [SerializeField] private Button _restartBt;

    [SerializeField] private GameObject _screenCanvas;
    [SerializeField] private GameObject _tableCanvas;
    [SerializeField] private Text timeText;


    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    public Action timerFinishedEvent;

    public float timer = 10;
    public float multiplier = 10;
    private float timeRemaining = 0;
    private bool timerIsRunning = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        _startBt.onClick.AddListener(StartExperiment);
        _timerBt.onClick.AddListener(StartTimer);
        _restartBt.onClick.AddListener(Restart);
    }
    private void OnDestroy()
    {
        _startBt.onClick.RemoveAllListeners();
        _timerBt.onClick.RemoveAllListeners();
        _restartBt.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        timerIsRunning = false;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime*multiplier;
                DisplayTime(timeRemaining);
            }
            else
            {
                timerFinishedEvent?.Invoke();
                timeRemaining = 0;
                timerIsRunning = false;
                timeText.text = string.Empty;
            }
        }
    }

    private void StartExperiment()
    {
        _startBt.gameObject.SetActive(false);
        _screenCanvas.SetActive(true);
        _tableCanvas.SetActive(true);
        _timerBt.gameObject.SetActive(true);
    }

    private void StartTimer()
    {
        timeRemaining = timer;
        timerIsRunning = true;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
