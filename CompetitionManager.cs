using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;

using System.Linq;

using static CompetitionSaveData;
using JetBrains.Annotations;


public class CompetitionManager : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI NOTText;

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI DateText;

    [SerializeField] Transform contents;
  
    [SerializeField] GameObject inputPrefab;
    [SerializeField] List<TMP_InputField> Names;
    [SerializeField] CompetitionSaveData data;
     public int currentCompetitor = 0;
    public string selectedCompetitor;
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
         //  ClearCompetitorInput(contents); 
            GenerateEmptyInputField();

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
                GameObject newPerson;
                newPerson = Instantiate (inputPrefab) as GameObject;
                TMP_InputField inputField = newPerson.GetComponent<TMP_InputField>();
               
               // TMP_InputField inputField = Names[i];
               inputField.text  = data.competitorList[i].CompetitorName;
               Names.Add(inputField);
               newPerson.transform.SetParent (contents,false);
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

    void ClearCompetitorInput(Transform transform)
    {
       foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
        for (int i = 0; i < 7; i++)
        {
            GenerateEmptyInputField();
        }
        }

    public void GenerateEmptyInputField()
    {
         GameObject newPerson;
        newPerson = Instantiate (inputPrefab) as GameObject;
  

        newPerson.transform.SetParent (contents,false);
    }

    public  void GenerateInputField(TMP_InputField input)
    { 
        Names.Add(input);
        GenerateEmptyInputField();
  

        }
        
 
   


    public void StoreCompetitors()
    {
          if (AppManager.instance.SelectedFile != "None")
        {
         return;
        

         } else {
           string today = DateTime.Now.Day.ToString() +  "-"  + DateTime.Now.Month.ToString() + "-"  + DateTime.Now.Year.ToString();
            AppManager.instance.SelectedFile = today;   
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
        
    
     
      
    }

    // public void NameEdited(string newName)
    // {
        
    //     // string comp;
    //     // for (int i = 0; i < Names.Count-1; i++)
        
    //     // {
         
    //     //      comp = Names[i].GetComponentInChildren<TextMeshProUGUI>().text;
    //     //      print("comp names i " + comp + " = Newname " + newName + " i " + i);
            
                
    //     //       //  if(comp == newName) {
    //     //    //      if(   String.Equals(comp,newName)) {
    //     //     print("yay ");
    //     //        CompetitorList editedCompetitor = new CompetitorList();
               
    //     //      //   editedCompetitor.CompetitorName = newName;
    //     //      editedCompetitor.CompetitorName  = "name Changed";
    //     //         print(" edited name " +editedCompetitor.CompetitorName );
    //     //         data.competitorList[i] = editedCompetitor;
    //     //     //    }
                
            
        
    //     // }
    // }

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

      public void LoadCompetitorScene()
    {
        AppManager.instance.LoadSceneAdditively();
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
