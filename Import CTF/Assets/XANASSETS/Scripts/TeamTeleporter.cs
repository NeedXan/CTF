
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TeamTeleporter : UdonSharpBehaviour
{
    [SerializeField] private Transform alphaTpPoints;
    [SerializeField] private Transform bravoTpPoints;

    [SerializeField] private Transform spawn;
    
    private TeamManager teamManager;

    private void Start()
    {
        teamManager = TeamManager.instance();
    }

    public void OnGamePrep()
    {
        for (int i = 0; i < teamManager.alphaTeam.Length; i++)
        {
            if (Utilities.IsValid(VRCPlayerApi.GetPlayerById(teamManager.alphaTeam[i])))
                VRCPlayerApi.GetPlayerById(teamManager.alphaTeam[i]).TeleportTo(alphaTpPoints.GetChild(i).transform.position, alphaTpPoints.GetChild(i).transform.rotation);
        }

        for (int i = 0; i < teamManager.bravoTeam.Length; i++)
        {
            if (Utilities.IsValid(VRCPlayerApi.GetPlayerById(teamManager.bravoTeam[i])))
                VRCPlayerApi.GetPlayerById(teamManager.bravoTeam[i]).TeleportTo(bravoTpPoints.GetChild(i).transform.position, bravoTpPoints.GetChild(i).transform.rotation);
        }
    }

    public void OnGameEnd()
    {
        for (int i = 0; i < teamManager.alphaTeam.Length; i++)
        {
            if (Utilities.IsValid(VRCPlayerApi.GetPlayerById(teamManager.alphaTeam[i])))
                VRCPlayerApi.GetPlayerById(teamManager.alphaTeam[i]).TeleportTo(spawn.position, spawn.rotation);
        }

        for (int i = 0; i < teamManager.bravoTeam.Length; i++)
        {
            if (Utilities.IsValid(VRCPlayerApi.GetPlayerById(teamManager.bravoTeam[i])))
                VRCPlayerApi.GetPlayerById(teamManager.bravoTeam[i]).TeleportTo(spawn.position, spawn.rotation);
        }
    }
}
