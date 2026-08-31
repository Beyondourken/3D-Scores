using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using System.Linq;
using NUnit.Framework;
using static CompetitionSaveData;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor.Rendering;
public class CompetitionManager : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI NOTText;

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI DateText;

  
 
     [SerializeField] List<TMP_InputField> Names;
     [SerializeField] CompetitionSaveData data;
    string competitionDate;
    int NoOfTargets = 18;


    public static CompetitionManager instance;

 private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    } 
    void Start()
     {
        
         if (AppManager.instance.SelectedFile == "None") {
           
            DateText.text= DateTime.Now.Day.ToString() + "/"  + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();     
            data.competitorList = new List<CompetitorList>();
        } else
        {
            AppManager.instance.LoadScores();
            NoOfTargets = data.Targets;
            slider.value = (float)NoOfTargets;
            NOTText.text = data.Targets.ToString();
          
            DateText.text = data.CompetitionDate;
            for (int i = 0;i < data.competitorList.Count;i++)
            {
                TMP_InputField inputField = Names[i];
               inputField.text  = data.competitorList[i].CompetitorName;
            }

        }
    }
  
  public void UpdateSlider()
    {
        NOTText.text = slider.value.ToString();
        
        NoOfTargets = (int)slider.value;
    
    }
      public int GetNumberOfTargets()
    {
      
       return NoOfTargets;
    }
       public int GetNumberOfCompetitors()
    {
      
      
       return data.competitorList.Count();
       


    }
        public List<string> GetCompetitors()
    {
        List<string> names = new List<string>();
       
       foreach (var item in  data.competitorList)
        {
            names.Add(item.CompetitorName);
           
        }
        
       return names;
       
     } 
     public int GetCompetitorScore(int index,int roundIndex)
    {
   
       int score = data.competitorList[index].scores[roundIndex-1];
  
        
       return score;
       
    } 

     public int GetCompetitorTotalScore(int index)
    {
      
       int score = 0;
    
      for (int i = 0; i < data.competitorList[index].scores.Count; i++)
      {
        
        score += data.competitorList[index].scores[i];
      }
        
       return score;
       
    } 
    public string GetDate()
    {
        return competitionDate;
    }
  

    public void LoadScene(int scene)
    {
        AppManager.instance.LoadScene(scene);
    }

    public void StoreCompetitors()
    {
         if (AppManager.instance.SelectedFile != "None") { return;}

        //TODO check if competitors have been added (allow deleted?) to existing file
        competitionDate = DateTime.Now.Day.ToString() + "/"  + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
        data.Targets = NoOfTargets;
        data.CompetitionDate = competitionDate;
        List<int> scoresList = new List<int>();
        for (int i = 0; i < NoOfTargets  ;i++)
        {
            scoresList.Add(1);
        }
        string comp;
      
        for (int i = 0; i < Names.Count; i++)
        
        {
           
             comp = Names[i].GetComponentInChildren<TextMeshProUGUI>().text;
            if( comp.Length <= 1 )
            {
                
            } else {
                CompetitorList newCompetitor = new CompetitorList();

                newCompetitor.scores =  new List<int>(scoresList);
                newCompetitor.CompetitorName = comp;
                
               
                data.competitorList.Add(newCompetitor);
                
            }
        
        }
    
     
      
    }

    public void Save(ref CompetitionSaveData saveData)
    {
        saveData = data;
  
      
    }
    public void Load(CompetitionSaveData loadData)
    {
        NoOfTargets = loadData.Targets;
        competitionDate = loadData.CompetitionDate;
     
       data = loadData;
      

       
        


        
    }
    public void SaveRoundScores(List<int> scores, int roundNumber)
    {
     
        for (int i = 0; i < scores.Count; i++)
        {
           
            data.competitorList[i].scores[roundNumber-1] = scores[i];
        }
        scores.Clear();
        AppManager.instance.SaveScores(); 
    }
}






[System.Serializable]
public class CompetitionSaveData
{
    public string CompetitionDate;
    public int Targets;
    public List<CompetitorList> competitorList;


 [System.Serializable]
 public struct CompetitorList
 {
     public string CompetitorName;
     public List<int> scores;
 }



}
