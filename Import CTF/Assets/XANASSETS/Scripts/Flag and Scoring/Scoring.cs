
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class Scoring : UdonSharpBehaviour
{
    public Flag flag;
    public ScoringDisplay scoreDisplay;
    [UdonSynced] public int alphaScore;
    [UdonSynced] public int bravoScore;
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player.GetPlayerTag("HasFlag").Equals("true"))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            switch (player.GetPlayerTag("CurTeam"))
            {
                case "Alpha":
                    alphaScore++;
                    break;
                case "Bravo":
                    bravoScore++;
                    break;
            }
            RequestSerialization();
            OnDeserialization();
        }
    }
    public void OnDeserialization()
    {
        scoreDisplay.UpdateDisplay(alphaScore, bravoScore);
        flag.Score();
    }
}
