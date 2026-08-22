using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;
public class CompetitionManager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI NOTText;
    [SerializeField] List<string> Competitors;
     [SerializeField] List<TMP_InputField> Names;
    int NoOfTargets;

    public static CompetitionManager instance;

 private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    } 
  void Update()
    {
        NOTText.text = slider.value.ToString();
        NoOfTargets = (int)slider.value;
    }
      public int GetNumberOfTargets()
    {
      
       return NoOfTargets;
    }

    public void LoadScene(int scene)
    {
        AppManager.instance.LoadScene(scene);
    }
    public void StoreCompetitors()
    {
        string comp;
        foreach (var item in Names)
        {
           
             comp = item.GetComponentInChildren<TextMeshProUGUI>().text;
            if( comp.Length <= 1 )
            {
                
            } else {
                Competitors.Add(comp);
                print(comp);
            }
           
        }
      SaveScores();
     
      
    }

    public void Save(ref CompetitionSaveData data)
    {
      
        data.Targets = NoOfTargets;
        data.CompetitorList = Competitors;
      
    }
    public void Load(CompetitionSaveData data)
    {
        NoOfTargets = data.Targets;
        Competitors = data.CompetitorList;
    }
    public void SaveScores()
    {
      
        AppManager.instance.SaveScores(); 
    }
}

[System.Serializable]
public struct CompetitionSaveData
{
    public int Targets;
    public List<string> CompetitorList;
}
