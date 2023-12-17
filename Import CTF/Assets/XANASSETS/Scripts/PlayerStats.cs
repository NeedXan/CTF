
using Miner28.UdonUtils.Network;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerStats : NetworkInterface
{
    // WIP
    /// <summary>
    /// players will confirm stats while in a pregame section. The code for pregame is there, just need to activate it. 
    /// player stats will affect gameplay
    /// players have classes with preset stats (tank, dmg, scout, balanced, etc.)
    /// then players have 5 extra stat points, which can be spent to upgrade stats or buy a special ability (feats)
    /// feat example: on kill, reload
    ///  - on kill, speed up
    ///  - passive healing
    ///  - cloaking?
    ///  - etc.
    /// </summary>
    /// <returns></returns>

    public static PlayerStats instance()
    {
        return GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    float baseHp = 100;
    float maxHP;
    float curHP;

    public float strength;
    public float agility;
    public float gunControl;
    public float fireRateMult;
    public float vitality;

    float baseRunSpeed; // 6
    float baseWalkSpeed; // 4
    float baseStrafeSpeed; // 4

    public float baseReloadTime;
    public float baseFireRate;
    public float baseDamage;

    float reloadTime;
    float fireRate;
    float damage;

    void Start()
    {
        Networking.LocalPlayer.SetPlayerTag("InTeam", "false");
        Networking.LocalPlayer.SetPlayerTag("CurTeam", "N/A");
        Networking.LocalPlayer.SetPlayerTag("HasFlag", "false");

        baseRunSpeed = Networking.LocalPlayer.GetRunSpeed();
        baseWalkSpeed = Networking.LocalPlayer.GetWalkSpeed();
        baseStrafeSpeed = Networking.LocalPlayer.GetStrafeSpeed();
        baseReloadTime = 3f;
        baseFireRate = .5f;
        baseDamage = 20f;
    }

    public void PickupGun(float gunDmg, float gunFireRate, float gunReloadTime)
    {

    }

    void ConfirmStats()
    {
        maxHP = baseHp + (vitality * 20f);                                              // lv.0 = 100 || lv.5 = 200
        curHP = maxHP;

        Networking.LocalPlayer.SetRunSpeed(baseRunSpeed + (agility * .5f));             // lv.0 = 6 || lv.5 = 8.5
        Networking.LocalPlayer.SetWalkSpeed(baseWalkSpeed + (agility * .25f));          // lv.0 = 4 || lv.5 = 5.25
        Networking.LocalPlayer.SetStrafeSpeed(baseStrafeSpeed + (agility * .25f));      // lv.0 = 4 || lv.5 = 5.25

        reloadTime = baseReloadTime - (gunControl * .3f);                               // lv.0 = 3s || lv.5 = 1.5s
        fireRate = baseFireRate - (fireRateMult * .05f);                                // lv.0 = .5s || lv.5 = .25s
        damage = baseDamage - (strength * 3);                                           // lv.0 = 20 || lv.5 = 35
    }

    [NetworkedMethod]
    public void TakeDamage(float damage, int idTaking, int idGiving)
    {
        if (Networking.LocalPlayer.playerId == idTaking)
        {
            curHP -= damage;

            if (curHP <= 0)
            {
                SendMethodNetworked(nameof(OnKillFeat), SyncTarget.All, idTaking, idGiving);
            }
        }
    }

    [NetworkedMethod]
    public void OnKillFeat(int idKilled, int idKiller)
    {
        if (Networking.LocalPlayer.playerId == idKiller)
        {
            // do things
        }
    }
}
