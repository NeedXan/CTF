
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class GameEventListener : UdonSharpBehaviour
{
    public static GameEventListener instance()
    {
        return GameObject.Find("GameEventListener").GetComponent<GameEventListener>();
    }

    #region Script References

    [Header("Script References")]
    [SerializeField] GameManager gameManager;
    [SerializeField] TeamTeleporter teamTeleporter;
    [SerializeField] GameTimer gameTimer;
    [SerializeField] ScoringDisplay scoreDisplay;

    #endregion Script References

    public void OnGamePrep()
    {
        gameManager.SendCustomNetworkEvent(NetworkEventTarget.All, "OnGamePrep");
        teamTeleporter.SendCustomNetworkEvent(NetworkEventTarget.All, "OnGamePrep");
    }

    public void OnGameStart()
    {
        gameManager.SendCustomNetworkEvent(NetworkEventTarget.All, "OnGameStart");
        gameTimer.SendCustomNetworkEvent(NetworkEventTarget.All, "OnGameStart");
    }

    public void OnGameEnd()
    {
        gameManager.SendCustomNetworkEvent(NetworkEventTarget.All, "OnGameEnd");
        teamTeleporter.SendCustomNetworkEvent(NetworkEventTarget.All, "OnGameEnd");
    }
}
