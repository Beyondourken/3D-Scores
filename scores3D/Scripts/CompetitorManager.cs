using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompetitorManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI competitorName;
    [SerializeField] Transform contents;
     [SerializeField] GameObject resultsPrefab;
      int numberOfTargets = 0;
      List<string> results;
  
    void Start()
    {
        resultsPrefab = Resources.Load<GameObject>("CompetitorResults");
        numberOfTargets = CompetitionManager.instance.GetNumberOfTargets();
        List<string> results = new List<string>{
            "First Kill","First Wound",
            "Second Kill","Second Wound",
            "Third Kill","Third Wound",
            "All Away","Not Recorded"};                                                       
       // competitorName.text = ScoreSheetManager.instance.selectedCompetitor;
       competitorName.text = CompetitionManager.instance.selectedCompetitor;
        GameObject newRound;
        int hitIndex = 0;
        for (int i = 0; i < numberOfTargets; i++)
        {
           newRound = Instantiate (resultsPrefab) as GameObject;
          ResultsDisplay roundDisplay = newRound.GetComponent <ResultsDisplay> ();
          int round = i;
          round++;
            roundDisplay.roundText.text = (round).ToString();
           
       
           hitIndex = AppManager.instance.GetHitValueIndex(CompetitionManager.instance.GetCompetitorScore
                                                        // (ScoreSheetManager.instance.currentCompetitor,round));
                                                         (CompetitionManager.instance.currentCompetitor,round));
        if(hitIndex <0) {hitIndex = 7;}        // catches unrecorded results
            roundDisplay.hitText.text = results[hitIndex];
          
            newRound.transform.SetParent (contents,false);
           
              
        }
    }

    public void PreviousScreen()
    {
        Destroy(this.gameObject);
    }

 
}
