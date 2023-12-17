
using System.Net.NetworkInformation;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class Flag : UdonSharpBehaviour
{
    //                              THIS SCRIPT IS BROKEN - OBJECTS DO NOT SYNC, LOGIC STILL WORKS                                      //

    [SerializeField] GameObject fakeFlag;

    [SerializeField, UdonSynced] string team;
    [UdonSynced] int curPlayerId = 0;

    VRCPlayerApi curPlayer = null;
    VRCPickup pickup;
    Vector3 homePos;
    float timer = 3f;
    bool timerOn = false; 
    bool isInVr;

    private void Start()
    {
        pickup = GetComponent<VRCPickup>();
        isInVr = Networking.LocalPlayer.IsUserInVR();
        homePos = transform.position;
    }

    public override void OnPickup()
    {
        timerOn = false;
        timer = 3f;

        curPlayer = pickup.currentPlayer;

        Networking.SetOwner(curPlayer, gameObject);
        Networking.SetOwner(curPlayer, fakeFlag);

        if (!isInVr)
        {
            pickup.Drop();

            GetComponent<MeshRenderer>().enabled = false;
            fakeFlag.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public override void OnDrop()
    {
        if (isInVr)
            DropFlag();
    }

    void DropFlag()
    {
        if (!isInVr && curPlayer != null && Networking.LocalPlayer.Equals(curPlayer))
        {
            transform.position = curPlayer.GetPosition() + Vector3.up;

            GetComponent<MeshRenderer>().enabled = true;
            fakeFlag.GetComponent<MeshRenderer>().enabled = false;
        }

        curPlayer.SetPlayerTag("HasFlag", "false");
        curPlayer = null;

        Respawn();
    }

    private void Update()
    {
        if (Networking.LocalPlayer.GetPlayerTag("CurTeam").Equals(team))
            pickup.pickupable = false;
        else
            pickup.pickupable = true;

        if (curPlayer != null && !isInVr)
        {
            if (curPlayer.GetPlayerTag("HasFlag").Equals("false"))
                curPlayer.SetPlayerTag("HasFlag", "true");

            fakeFlag.transform.position = Networking.GetOwner(fakeFlag).GetPosition() + (Vector3.up * 2);

            if (Networking.LocalPlayer.Equals(curPlayer) && Input.GetKeyDown(KeyCode.F))
                DropFlag();
        }

        if (curPlayer == null && timerOn)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
                transform.position = homePos;
        }

        if (curPlayerId != 0)
            if (!curPlayer.Equals(VRCPlayerApi.GetPlayerById(curPlayerId)))
                curPlayer = VRCPlayerApi.GetPlayerById(curPlayerId);
    }

    void Respawn()
    {
        timer = 3f;
        timerOn = true;
    }

    public void Score()
    {
        DropFlag();

        transform.position = new Vector3(0, -100, 0);
    }
}
