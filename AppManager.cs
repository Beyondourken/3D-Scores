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


List<Text> names;



 public static AppManager instance;

 private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
     
    
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
