
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class GameManager : UdonSharpBehaviour
{
    public static GameManager instance()
    {
        return GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    [UdonSynced] public bool inPregame = false;
    [UdonSynced] public bool gameRunning = false;
    [UdonSynced] public int alphaScore = 0;
    [UdonSynced] public int bravoScore = 0;

    public void OnGamePrep()
    {
        inPregame = true;
        GameEventListener.instance().OnGameStart();
    }

    public void OnGameStart()
    {
        inPregame = false;
        gameRunning = true;
        RequestSerialization();
    }

    public void OnGameEnd()
    {
        gameRunning = false;
        RequestSerialization();
    }
}
