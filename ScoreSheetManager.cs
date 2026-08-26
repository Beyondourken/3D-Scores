using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class ScoreSheetManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundNumber;
    [SerializeField] Transform contents;
  
   [SerializeField] GameObject roundPrefab;
    [SerializeField] List<int> roundScores;    
 int numberOfTargets = 0;
 int numberOfCompetitors = 0;
 List<string> competitors;
 int currentRound = 1;

void Start ()
    {
        roundPrefab = Resources.Load<GameObject>("RoundPrefab");
        currentRound  = 1;
        roundNumber.text = currentRound.ToString();
        numberOfTargets = CompetitionManager.instance.GetNumberOfTargets();
        competitors = CompetitionManager.instance.GetCompetitors();
       GenerateScoreSheet();
   
    }

    public void NextRound()
    {
        StoreScores(contents);
        ClearScoreSheet(contents);
        currentRound ++;
        if(currentRound > numberOfTargets) {
            CompetitionManager.instance.LoadScene(3) ;
        } else{
        roundNumber.text = currentRound.ToString();
        GenerateScoreSheet();
        }
    }

    public void PreviousRound()
    {
        StoreScores(contents);
    }
    void StoreScores(Transform transform)
    {
        int score;
        
        foreach (Transform child in transform) {
			RoundManager roundPerson = child.GetComponent <RoundManager> ();
             
            int.TryParse(roundPerson.scoreText.text, out score);
            roundScores.Add(score);
         
		}
		CompetitionManager.instance.SaveScores(roundScores);
        
    }
    void ClearScoreSheet(Transform transform)
    {
       foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
        
        }

    void GenerateScoreSheet()
    {
        numberOfCompetitors = CompetitionManager.instance.GetNumberOfCompetitors();
        GameObject newPerson;
     
       
        foreach (var person in competitors) {
            newPerson = Instantiate (roundPrefab) as GameObject;
            RoundManager roundPerson = newPerson.GetComponent <RoundManager> ();
            roundPerson.nameText.text = person;
          
            newPerson.transform.SetParent (contents,false);

        }
    }
}
