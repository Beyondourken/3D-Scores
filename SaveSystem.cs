using UnityEngine;
using System.IO;
using Unity.Android.Gradle.Manifest;
using UnityEngine.InputSystem.Interactions;
using System;
using System.Collections.Generic;

public class SaveSystem
{
    private static SaveData saveData = new SaveData();
    [System.Serializable]
    
    public struct  SaveData
    {
        public CompetitionSaveData competitionSaveData;
    }

    public static string SaveFilename()
    {
        string saveFile = "";
       if(AppManager.instance.SelectedFile == "None") {
       string today = DateTime.Now.Day.ToString() +  "-"  + DateTime.Now.Month.ToString() + "-"  + DateTime.Now.Year.ToString();
       
      
        saveFile = UnityEngine.Application.persistentDataPath + "/" + today + ".save";
       } else
        {
            saveFile = UnityEngine.Application.persistentDataPath + "/" + AppManager.instance.SelectedFile + ".save";
        }
        return saveFile;
    }

    public static void Save(){
      HandleSaveData(); 


      File.WriteAllText(SaveFilename(),JsonUtility.ToJson(saveData,true));
    }
    private static void HandleSaveData(){
  
      
      
    CompetitionManager.instance.Save(ref saveData.competitionSaveData);

    }
    public static List<string> ListFiles()
    {
        List<string> fileNames = new List<string>();
        string[] files  = Directory.GetFiles(UnityEngine.Application.persistentDataPath,"*.save");
      
      foreach (string file in files)
      {
       fileNames.Add(Path.GetFileNameWithoutExtension(file));
      } 
        
        return fileNames;
    }
    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFilename());
        saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }
    private static void HandleLoadData()
    {
        CompetitionManager.instance.Load(saveData.competitionSaveData);

    }



}
