using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FinalScores : MonoBehaviour
{
    [SerializeField] RectTransform contents;
    [SerializeField] TextMeshProUGUI competitionDate;
    [SerializeField] GameObject finalScoreDisplay;
     int numberOfTargets = 0;
     int numberOfCompetitors = 0;
    List<string> competitors;
    List<int> roundScores;
   // private Dictionary<string,int> finalScores =  new Dictionary<string, int>(); 
    List<finalScores> PlayerScores;
    finalScores[] sortedScores;

    void Start()
    {
        finalScoreDisplay = Resources.Load<GameObject>("FinalScoreDisplay");
        competitionDate.text = CompetitionManager.instance.GetDate(); 
        LoadData();
        CalculateFinalScores();
        SortFinalScores();
        DisplayScores();
    }



    void LoadData()
    {
        PlayerScores = new List<finalScores>();
        numberOfTargets = CompetitionManager.instance.GetNumberOfTargets();  
        numberOfCompetitors = CompetitionManager.instance.GetNumberOfCompetitors(); 
        competitors  = CompetitionManager.instance.GetCompetitors(); 
        roundScores = CompetitionManager.instance.GetScores();
     }
    private void CalculateFinalScores()
    {
        int runningTotal = 0;
       
        for (int i = 0; i < numberOfCompetitors; i++)
        {
            for (int j = i; j < roundScores.Count; j+=numberOfCompetitors)
            {
               runningTotal += roundScores[j];
         
            }
             PlayerScores.Add(new finalScores() { Competitor = competitors[i], Score = runningTotal });
         
           
            runningTotal = 0;
        }
    }
        private void SortFinalScores()
    {
        sortedScores = GetHighScores();
        
    }

      finalScores[] GetHighScores()
    {
        return PlayerScores.OrderByDescending(ps => ps.Score).ToArray();
       
    }
 
        void DisplayScores()
    {
       
        GameObject newScoreDisplay;
     
       
        foreach (var person in sortedScores) {
            newScoreDisplay = Instantiate (finalScoreDisplay) as GameObject;
            FinalScoreDisplay competitorScore = newScoreDisplay.GetComponent <FinalScoreDisplay> ();
            competitorScore.nameText.text = person.Competitor;
            competitorScore.scoreText.text = person.Score.ToString();
          
            newScoreDisplay.transform.SetParent (contents,false);

        }
    }
  

   
}
public struct finalScores {
   
    public string Competitor;
    public int Score;

}
