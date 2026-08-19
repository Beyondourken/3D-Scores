using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using JetBrains.Annotations;

public class RoundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] List<GameObject> ImageList;
   
   int currentRoundScore = 0;
    void Start()
    {
        currentRoundScore = 0;
       // scoreText = GetComponentInParent<TextMeshProUGUI>();
    }
    public void EnterScore(int arrowScore) {
       
        int score;
        int.TryParse(scoreText.text, out score);
        score -= currentRoundScore;
        score += arrowScore;
        currentRoundScore = arrowScore;
        scoreText.text = score.ToString();
    }

    public void DisplayImage(int index)
    {
        foreach (GameObject item in ImageList)
        {
            item.SetActive(false);

        }
        ImageList[index].SetActive(true);
    }
}
