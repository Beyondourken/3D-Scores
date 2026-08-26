using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using System.Linq;
public class CompetitionManager : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI NOTText;

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI DateText;
    [SerializeField] List<string> Competitors;
     [SerializeField] List<TMP_InputField> Names;
    string competitionDate;
    int NoOfTargets = 18;
    List<int> RoundScores;

    public static CompetitionManager instance;

 private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        DateText.text= DateTime.Now.Day.ToString() + "/"  + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();  //TODO load option
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
      
       return Competitors.Count();
       
    }
        public List<string> GetCompetitors()
    {
      
       return Competitors;
       
    } 
     public List<int> GetScores()
    {
      
       return RoundScores;
       
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
        competitionDate = DateTime.Now.Day.ToString() + "/"  + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
        string comp;
        foreach (var item in Names)
        {
           
             comp = item.GetComponentInChildren<TextMeshProUGUI>().text;
            if( comp.Length <= 1 )
            {
                
            } else {
                Competitors.Add(comp);
                
            }
           
        }
    
     
      
    }

    public void Save(ref CompetitionSaveData data)
    {
        data.CompetitionDate = competitionDate;
        data.Targets = NoOfTargets;
        data.CompetitorList = Competitors;
        data.RoundScores = RoundScores;
      
    }
    public void Load(CompetitionSaveData data)
    {
        NoOfTargets = data.Targets;
        Competitors = data.CompetitorList;
        RoundScores = data.RoundScores;
    }
    public void SaveScores(List<int> scores)
    {
        RoundScores  = scores;
        AppManager.instance.SaveScores(); 
    }
}

[System.Serializable]
public struct CompetitionSaveData
{
    public string CompetitionDate;
    public int Targets;
    public List<string> CompetitorList;
    public List<int> RoundScores;
}


// [System.Serializable]
// public class Container
// {
//     public string name;
//     public List<Cell> map;
// }

// [System.Serializable]
// public class Cell
// {
//     public string groundTexture;
//     public string cellType;
//     public Vector2 masterField;
//     public List<WorldObject> worldObjects;
// }

// [System.Serializable]
// public class WorldObject
// {
//     public string worldObjectType;
//     public string rotation;
// }
