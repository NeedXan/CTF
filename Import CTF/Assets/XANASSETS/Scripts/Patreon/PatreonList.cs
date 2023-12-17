
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;

public class PatreonList : UdonSharpBehaviour
{
    public static PatreonList instance()
    {
        return GameObject.Find("PatreonList").GetComponent<PatreonList>();
    }

    // pastebin format should be "name1,name2,name3..."
    public VRCUrl pastebinUrl;

    // list of patrons from a pastebin raw url
    public string[] patrons;

    void Start()
    {
        UpdateString();
    }

    public override void OnStringLoadSuccess(IVRCStringDownload result)
    {
        patrons = result.Result.Split(',');

        foreach (string patron in patrons)
        {
            Debug.Log(patron);
        }
    }

    public void UpdateString()
    {
        VRCStringDownloader.LoadUrl(pastebinUrl, gameObject.GetComponent<UdonBehaviour>());
    }
}
