using UnityEngine;

using System.Collections.Generic;

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
 public int currentRound = 1;
 public int currentCompetitor = 0;
 public string selectedCompetitor;
public static ScoreSheetManager instance;

private void Awake()
    {
        instance = this;
       
        
    } 
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
        
        currentRound ++;
        if(currentRound > numberOfTargets) {
            CompetitionManager.instance.LoadScene(3) ; 
        } else{
        ClearScoreSheet(contents);
        roundNumber.text = currentRound.ToString();
        GenerateScoreSheet();
        }
    }

    public void PreviousRound()
    {
        StoreScores(contents);
       
        currentRound --;
        if(currentRound < 1) {
            CompetitionManager.instance.LoadScene(1) ; 

        } else{
          ClearScoreSheet(contents);   
        roundNumber.text = currentRound.ToString();
        GenerateScoreSheet();
        }
    }

    void StoreScores(Transform transform)
    {
        int score = 0;
        
        foreach (Transform child in transform) {
			RoundManager roundPerson = child.GetComponent <RoundManager> ();
             
            int.TryParse(roundPerson.scoreText.text, out score);
            roundScores.Add(score);
        
		}
     
		CompetitionManager.instance.SaveRoundScores(roundScores, currentRound);
        
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
        currentCompetitor = 0;
       
        foreach (var person in competitors) {
            newPerson = Instantiate (roundPrefab) as GameObject;
            RoundManager roundPerson = newPerson.GetComponent <RoundManager> ();
            roundPerson.nameText.text = person;
             if (AppManager.instance.SelectedFile != "None")
            {
                roundPerson.scoreText.text = CompetitionManager.instance.GetCompetitorScore(currentCompetitor,currentRound).ToString();
                if(CompetitionManager.instance.GetCompetitorScore(currentCompetitor,currentRound) < 10) {roundPerson.scoreText.text = "0";}
                roundPerson.currentRoundScore = CompetitionManager.instance.GetCompetitorScore(currentCompetitor,currentRound);
                roundPerson.DisplayImage(AppManager.instance.GetHitValueIndex(CompetitionManager.instance.GetCompetitorScore(currentCompetitor,currentRound)));
            }
          
            newPerson.transform.SetParent (contents,false);
            if (currentCompetitor < numberOfCompetitors-1) {currentCompetitor ++;}
            

        }
    }

    public void SetCompetitor(string name)
    {
        

        CompetitionManager.instance.selectedCompetitor = name;
        CompetitionManager.instance.currentCompetitor = competitors.IndexOf(name);
    }
}
