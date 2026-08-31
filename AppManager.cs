using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;
using UnityEngine.Rendering.LookDev;

public class AppManager : MonoBehaviour
{

[SerializeField] RectTransform contents;
 [SerializeField] GameObject FileNames;

 [SerializeField] public List<int> HitValues = new List<int>() {20,18,16,14,12,10,0};
 public string SelectedFile;





 public static AppManager instance;

 private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    { 
     SelectedFile = "None";
    
       foreach (Transform child in contents) {
			GameObject.Destroy(child.gameObject);
		}
        
      
      List<string> files = SaveSystem.ListFiles();
        GameObject newFile;
    foreach (var item in files)
    {
  
            newFile = Instantiate (FileNames) as GameObject;
            FileDisplay localFile = newFile.GetComponent <FileDisplay> ();
            localFile.fileText.text = item;
          
            newFile.transform.SetParent (contents,false);

    }
      
    }

    public int GetHitValueIndex(int hitValue)
    {
        return HitValues.IndexOf (hitValue);
    }

    public void SelectFile(string fileName)
    {
       
        SelectedFile = fileName;
   

    }
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    
    }
  
  
    public void SaveScores()
    {
       
        SaveSystem.Save();
    }
   
 public void LoadScores()
    {
        SaveSystem.Load();
    }
}


