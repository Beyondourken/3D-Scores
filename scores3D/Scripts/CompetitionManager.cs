using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;

using System.Linq;

using static CompetitionSaveData;
using JetBrains.Annotations;
using Unity.VisualScripting;


public class CompetitionManager : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI NOTText;

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI DateText;

    [SerializeField] Transform contents;
  
    [SerializeField] GameObject inputPrefab;
    [SerializeField] List<TMP_InputField> Names;
    [SerializeField] GameObject modalPanel;
    [SerializeField] TextMeshProUGUI confirmMessage;
    [SerializeField] CompetitionSaveData data;
    public int currentCompetitor = 0;
    public string selectedCompetitor;
    string competitionDate;
    int NoOfTargets = 18;
    bool nameEdited = false;




    public static CompetitionManager instance;

 private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    } 
    void Start()
     {
        
        


         if (AppManager.instance.SelectedFile == "None") {
         
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
         if(NoOfTargets > data.Targets)
            {
                confirmMessage.text = "Are you sure you want to increase the number of targets to " + NoOfTargets + "?";
                DisplayConfirmation();
            }
        if(NoOfTargets < data.Targets)
            {
                confirmMessage.text = "Are you sure you want to decrease the number of targets to " + NoOfTargets + "? This may result in a loss of data.";
                DisplayConfirmation();
            }
            
           if(NoOfTargets == data.Targets) {
            CheckNames();
            LoadScene(2);
            }
         

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
        LoadScene(2);

       }
        
    
     
      
    }

    private void DisplayConfirmation()
    {
       
        modalPanel.SetActive(true);
    }

    public void CancelAction()
    {
        NoOfTargets  = data.Targets;
        slider.value = (float)NoOfTargets;
         modalPanel.SetActive(false);
         CheckNames();
         LoadScene(2);
    }
    public void AmendNoOfTargets()
    {
        if(NoOfTargets > data.Targets) {
         
            foreach (var competitor in data.competitorList)
            {
                for (int i = data.Targets; i < NoOfTargets; i++)
                {
                   
                    competitor.scores.Add(1);
                    
                }
            }
            
            
        } else if(NoOfTargets < data.Targets) {
            print("Targets <");
            foreach (var competitor in data.competitorList)
            {
                print("competitor");
               print(data.Targets-1 + " " + NoOfTargets);
                for (int i = data.Targets-1; i > NoOfTargets-1
                
                ; i--)
                {
                   print(competitor.scores[i] + " " +i);
                    competitor.scores.RemoveAt(i);
                   
                }
            }
            }
        data.Targets = NoOfTargets;     
        modalPanel.SetActive(false);
        CheckNames();
        LoadScene(2);
    }

    public void NameEdited()
    {
        nameEdited = true;
       
    }
    void CheckNames() {
        if( !nameEdited) return;
        nameEdited = false;
           
        List<int> deletedNames = new List<int>();

        for (int i = 0; i < data.competitorList.Count; i++)

        {                   

            if(data.competitorList[i].CompetitorName != Names[i+1].text.ToString())
            {
                 if(Names[i+1].text == "")
                {
                    deletedNames.Add(i);
                } else {
                 CompetitorList editedCompetitor = new CompetitorList();
                 editedCompetitor.scores = data.competitorList[i].scores;
                 editedCompetitor.CompetitorName = Names[i+1].text.ToString();;
             
                data.competitorList[i] = editedCompetitor;
                }
            }
        }
            if(Names.Count-1 > data.competitorList.Count )
            {
                for (int j = data.competitorList.Count+1; j < Names.Count-1; j++)
                {
                    if(Names[j+1].text.ToString() != "")
                    {
                 
                        CompetitorList addedCompetitor = new CompetitorList();
                       
                       
                        addedCompetitor.CompetitorName = Names[j+1].text.ToString();;
                      
                        List<int> emptyScores = new List<int>();
                        for (int x = 0; x < NoOfTargets; x++)
                            {
                                emptyScores.Add(1);
                         }
                         addedCompetitor.scores = emptyScores;
                        data.competitorList.Add(addedCompetitor);
                    }
                }
                if (deletedNames.Count > 0)
            {
                for (int z = 0; z < deletedNames.Count; z++)
                {
                    data.competitorList.RemoveAt(deletedNames[z]);
                }
                 
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
