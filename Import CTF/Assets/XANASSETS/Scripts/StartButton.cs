
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class StartButton : UdonSharpBehaviour
{
    [SerializeField] GameObject startingDisplay;
    [SerializeField] TextMeshProUGUI startingTimer;
    [UdonSynced] int maxTime = 3;
    [UdonSynced] float timer = 3;

    [UdonSynced] bool timerOn = false;

    public void StartGame()
    {
        timer = maxTime;
        timerOn = true;
        startingDisplay.SetActive(true);

        RequestSerialization();
    }

    private void Update()
    {
        if (timerOn)
        {
            timer -= Time.deltaTime;
        }

        startingTimer.text = Mathf.CeilToInt(timer).ToString("00");

        if (timer <= 0 && timerOn)
        {
            timerOn = false;

            GameEventListener.instance().OnGamePrep();
        }

        RequestSerialization();
    }

    public override void OnDeserialization()
    {
        startingDisplay.SetActive(timerOn);
    }
}
