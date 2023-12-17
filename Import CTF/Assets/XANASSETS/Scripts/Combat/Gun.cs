
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

[RequireComponent(typeof(VRCPickup))]
public class Gun : UdonSharpBehaviour
{
    float baseDmg, baseReloadTime, baseFireRate;
    int maxAmmo, curAmmo;
    float reloadTimer;

    bool isPatron;

    VRCPickup pickup;

    void Awake()
    {
        pickup = GetComponent<VRCPickup>();

        foreach (string patron in PatreonList.instance().patrons)
        {
            if (Networking.GetOwner(gameObject).displayName.Equals(patron))
                isPatron = true;
        }
    }

    public override void OnPickup()
    {
        Networking.SetOwner(pickup.currentPlayer, gameObject);
        PlayerStats.instance().PickupGun(baseDmg, baseFireRate, baseReloadTime);
    }
}
