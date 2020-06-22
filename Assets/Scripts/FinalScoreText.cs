using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScoreText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        text.text = $"You collected {ScoreKeeper.Score}/30 goops!";
    }
}
