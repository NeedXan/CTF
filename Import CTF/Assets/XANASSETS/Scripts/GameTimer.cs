
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;
using System;

[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class GameTimer : UdonSharpBehaviour
{
    [UdonSynced] bool _running = false;
    int maxTime = 300;
    [UdonSynced] float curTime;
    [UdonSynced] string display;

    [SerializeField] TextMeshProUGUI timerText;

    public void OnGameStart()
    {
        _running = true;
        curTime = maxTime;
    }

    private void Update()
    {
        if (_running)
        {
            curTime -= Time.deltaTime;

            if (curTime <= 0)
            {
                _running = false;
                curTime = maxTime;
                GameEventListener.instance().OnGameEnd();
            }
        }

        TimeSpan t = TimeSpan.FromSeconds(Mathf.CeilToInt(curTime));

        display = string.Format("{0:D2} : {1:D2}",
                t.Minutes,
                t.Seconds);

        timerText.text = display;
    }
}
