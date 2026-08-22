using UnityEngine;
using System.IO;
using Unity.Android.Gradle.Manifest;
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
        string saveFile = UnityEngine.Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save(){
      HandleSaveData(); 
      File.WriteAllText(SaveFilename(),JsonUtility.ToJson(saveData,true));
    }
    private static void HandleSaveData(){
  
      
       CompetitionManager.instance.Save(ref saveData.competitionSaveData);


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
