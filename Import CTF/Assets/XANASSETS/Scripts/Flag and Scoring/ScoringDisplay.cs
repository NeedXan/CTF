
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class ScoringDisplay : UdonSharpBehaviour
{
    [SerializeField] TextMeshProUGUI alphaScore;
    [SerializeField] TextMeshProUGUI bravoScore;
    public void UpdateDisplay(int _alphaScore, int _bravoScore)
    {
        alphaScore.text = $"Alpha: {_alphaScore}";
        bravoScore.text = $"Bravo: {_bravoScore}";
    }
}
