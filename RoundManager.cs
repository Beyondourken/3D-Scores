using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class RoundManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI nameText;    
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] List<GameObject> ImageList;
   
   public int currentRoundScore = 0;
    void Start()
    {
        if(AppManager.instance.SelectedFile == "None")
        {
   
            currentRoundScore = 0;
        }
        
      
    }

    public void LoadCompetitorScene()
    {
        AppManager.instance.LoadSceneAdditively();
    }
  

    public void EnterScore(int arrowScore) {
       int localArrowScore = AppManager.instance.HitValues[arrowScore];
       

        int score;
        int.TryParse(scoreText.text, out score);
       

        score -= currentRoundScore;
        score += localArrowScore;
       
        currentRoundScore = localArrowScore;
        scoreText.text = score.ToString();
        DisplayImage(arrowScore);
    }

    public void DisplayImage(int index)
    {
        foreach (GameObject item in ImageList)
        {
            item.SetActive(false);

        }
        if (index < ImageList.Count) {
        ImageList[index].SetActive(true);
        }
    }

    public void SelectedCompetitor()
    {
        ScoreSheetManager.instance.SetCompetitor(nameText.text.ToString());
    }
}
