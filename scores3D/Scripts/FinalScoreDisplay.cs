using TMPro;
using UnityEngine;

public class FinalScoreDisplay : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI nameText;    
    [SerializeField] public TextMeshProUGUI scoreText;

    public void LoadCompetitorScene()
    {
     
       FinalScores.instance.LoadCompetitorScene(nameText.text);
    }

}
