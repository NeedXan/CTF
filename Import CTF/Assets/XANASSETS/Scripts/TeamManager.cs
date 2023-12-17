
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class TeamManager : UdonSharpBehaviour
{
    public static TeamManager instance()
    {
        return GameObject.Find("TeamManager").GetComponent<TeamManager>();
    }



    public byte[] alphaTeam = new byte[6];
    public byte[] bravoTeam = new byte[6];

    [UdonSynced] byte alpha1, alpha2, alpha3, alpha4, alpha5, alpha6;
    [UdonSynced] byte bravo1, bravo2, bravo3, bravo4, bravo5, bravo6;

    void SyncArrays()
    {
        alphaTeam = new byte[6] { alpha1, alpha2, alpha3, alpha4, alpha5, alpha6 };
        bravoTeam = new byte[6] { bravo1, bravo2, bravo3, bravo4, bravo5, bravo6 };
    }

    public void AddPlayer(string teamId, int playerId)
    {
        if (VRCPlayerApi.GetPlayerById(playerId).GetPlayerTag("InTeam").Equals("false"))
        {
            switch (teamId)
            {
                case "Alpha":
                    for (int i = 0; i < alphaTeam.Length; i++)
                    {
                        if (alphaTeam[i] == 0)
                        {
                            switch (i)
                            {
                                case 0:
                                    alpha1 = (byte)playerId;
                                    break;
                                case 1:
                                    alpha2 = (byte)playerId;
                                    break;
                                case 2:
                                    alpha3 = (byte)playerId;
                                    break;
                                case 3:
                                    alpha4 = (byte)playerId;
                                    break;
                                case 4:
                                    alpha5 = (byte)playerId;
                                    break;
                                case 5:
                                    alpha6 = (byte)playerId;
                                    break;
                            }

                            SyncArrays();

                            VRCPlayerApi.GetPlayerById(playerId).SetPlayerTag("CurTeam", "Alpha");
                            break;
                        }
                    }
                    break;
                case "Bravo":
                    for (int i = 0; i < bravoTeam.Length; i++)
                    {
                        if (bravoTeam[i] == 0)
                        {
                            switch(i)
                            {
                                case 0:
                                    bravo1 = (byte)playerId;
                                    break;
                                case 1:
                                    bravo2 = (byte)playerId;
                                    break;
                                case 2:
                                    bravo3 = (byte)playerId;
                                    break;
                                case 3:
                                    bravo4 = (byte)playerId;
                                    break;
                                case 4:
                                    bravo5 = (byte)playerId;
                                    break;
                                case 5:
                                    bravo6 = (byte)playerId;
                                    break;
                            }

                            SyncArrays();

                            VRCPlayerApi.GetPlayerById(playerId).SetPlayerTag("CurTeam", "Bravo");
                            break;
                        }
                    }
                    break;
            }
            VRCPlayerApi.GetPlayerById(playerId).SetPlayerTag("InTeam", "true");
        }
    }

    public void RemovePlayer(int playerId)
    {
        if (VRCPlayerApi.GetPlayerById(playerId).GetPlayerTag("InTeam").Equals("true"))
        {
            switch (VRCPlayerApi.GetPlayerById(playerId).GetPlayerTag("CurTeam"))
            {
                case "Alpha":
                    for (int i = 0; i < alphaTeam.Length; i++)
                    {
                        if (alphaTeam[i] == playerId)
                        {
                            switch (i)
                            {
                                case 0:
                                    alpha1 = 0;
                                    break;
                                case 1:
                                    alpha2 = 0;
                                    break;
                                case 2:
                                    alpha3 = 0;
                                    break;
                                case 3:
                                    alpha4 = 0;
                                    break;
                                case 4:
                                    alpha5 = 0;
                                    break;
                                case 5:
                                    alpha6 = 0;
                                    break;
                            }

                            VRCPlayerApi.GetPlayerById(playerId).SetPlayerTag("InTeam", "false");
                            VRCPlayerApi.GetPlayerById(playerId).SetPlayerTag("CurTeam", "N/A");
                            SyncArrays();
                        }
                    }
                    break;
                case "Bravo":
                    for (int i = 0; i < bravoTeam.Length; i++)
                    {
                        if (bravoTeam[i] == playerId)
                        {
                            switch (i)
                            {
                                case 0:
                                    bravo1 = 0;
                                    break;
                                case 1:
                                    bravo2 = 0;
                                    break;
                                case 2:
                                    bravo3 = 0;
                                    break;
                                case 3:
                                    bravo4 = 0;
                                    break;
                                case 4:
                                    bravo5 = 0;
                                    break;
                                case 5:
                                    bravo6 = 0;
                                    break;
                            }

                            VRCPlayerApi.GetPlayerById(playerId).SetPlayerTag("InTeam", "false");
                            VRCPlayerApi.GetPlayerById(playerId).SetPlayerTag("CurTeam", "N/A");
                            SyncArrays();
                        }
                    }
                    break;
            }
        }
    }

    public void BravoTeam()
    {
        AddPlayer("Bravo", Networking.LocalPlayer.playerId);
    }

    public void AlphaTeam()
    {
        AddPlayer("Alpha", Networking.LocalPlayer.playerId);
    }

    public void Leave()
    {
        RemovePlayer(Networking.LocalPlayer.playerId);
    }
}

