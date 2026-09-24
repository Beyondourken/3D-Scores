using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AppManager : MonoBehaviour
{

[SerializeField] RectTransform contents;
 [SerializeField] GameObject FileNames;
 [SerializeField] public Button loadButton;
 [SerializeField] public GameObject cover;

 [SerializeField] public List<int> HitValues = new List<int>() {20,18,16,14,12,10,0,1};
 public string SelectedFile;





 public static AppManager instance;

 private void Awake()
    {
        if (instance != null && instance != this) 
    { 
        Destroy(this); 
    } 
 
   
	
        instance = this;
       // DontDestroyOnLoad(gameObject);
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

    public int GetHitValueIndex(int hitValue) {
  
        return HitValues.IndexOf (hitValue);
    }

    public void SelectFile(string fileName)
    {
       
        SelectedFile = fileName;
        if(loadButton != null) {
        loadButton.interactable = true;}
        

    }
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    
    }

    public void LoadSceneAdditively()
    {
        SceneManager.LoadScene("Competitor Scene", LoadSceneMode.Additive);
        
    }
  
  
    public void SaveScores()
    {
       
        SaveSystem.Save();
    }
   
 public void LoadScores()
    {
        SaveSystem.Load();
    }

    public void DeleteFile()
    {
        SaveSystem.Delete(SelectedFile);
        SelectedFile = "None";
    }

}


