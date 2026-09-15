using UnityEngine;
using System.IO;

using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.Interactions;

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
        
        files  = Directory.GetFiles(UnityEngine.Application.persistentDataPath,"*.archive");
      
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

    public static void Delete(String filename)
    {
        File.Delete (Application.persistentDataPath + "/"  + filename + ".SAVE");
    }

    // public static void SaveArchive(String filename){  
     
    //     string saveContent = File.ReadAllText(SaveFilename());
       
    //     saveData = JsonUtility.FromJson<SaveData>(saveContent);
    // string ArchiveFilename = UnityEngine.Application.persistentDataPath + "/" + filename + ".archive";           

    //   File.WriteAllText(ArchiveFilename,JsonUtility.ToJson(saveData,true));
    //   // File.SetAttributes from System.IO readonly
    // //   File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
    // //         Console.WriteLine("The {0} file is now hidden.", path);
    // }

}
