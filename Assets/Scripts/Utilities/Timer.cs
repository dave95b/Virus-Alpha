using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour, ISaveable {

    public bool isPaused = false;

    [SerializeField]
    private GameConfiguration gameConfiguration;

    [SerializeField]
    private int warningDuration;

    [SerializeField]
    private Color warningColor;


    [Space, SerializeField]
    private NativeEvent onTurnChange;

    [SerializeField]
    private NativeEvent onGameOver;

    [SerializeField]
    private UnityEvent OnCountdownFinished;


    private int remainingTime, nextTime;

    private Text text;
    private Color defaultTextColor;


    private void Start()
    {
        if (gameConfiguration.TurnTime == 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            return;
        }
        //remainingTime = gameConfiguration.TurnTime;

        text = GetComponent<Text>();
        defaultTextColor = text.color;
        FormatText(remainingTime);
    }

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }


    private void OnEnable()
    {
        onTurnChange.AddListener(TurnChanged);
        onGameOver.AddListener(GameOver);
    }

    private void OnDisable()
    {
        onTurnChange.RemoveListener(TurnChanged);
        onGameOver.RemoveListener(GameOver);
        StopAllCoroutines();
    }


    // Postanowiłem zmienić to na Coroutine, skoro interesuje nas interwał jednosekundowy ;)
    private IEnumerator Countdown()
    {
        WaitForSeconds secondDelay = new WaitForSeconds(1f);
        while (true)
        {
            while(isPaused)
                yield return null;

            yield return secondDelay;

            remainingTime--;
            if (remainingTime == warningDuration)
                text.color = warningColor;
            if (remainingTime == 0)
                OnCountdownFinished.Invoke();

            FormatText(remainingTime);
        }
    }

    private void FormatText(int seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        text.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }


    private void TurnChanged()
    {
        StopAllCoroutines();
        remainingTime = nextTime;
        nextTime = gameConfiguration.TurnTime;
        text.color = defaultTextColor;
        FormatText(remainingTime);
    }

    private void GameOver()
    {
        gameObject.SetActive(false);
    }


    public void SaveState(SavedState state)
    {
        state.RemainingTurnTime = remainingTime;
    }

    public void ApplyStateEarly(SavedState state)
    {
        if (state.IsLoaded)
            nextTime = state.RemainingTurnTime;
        else
            nextTime = gameConfiguration.TurnTime;
    }

    public void ApplyStateLate(SavedState state)
    {

    }
}
