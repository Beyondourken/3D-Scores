using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    [SerializeField] RectTransform contents;
    [SerializeField] GameObject FileNames;
    [SerializeField] Button fileButton;
    [SerializeField] GameObject modalPanel;
    [SerializeField] TextMeshProUGUI fileName;
    [SerializeField] TextMeshProUGUI confirmMessage;
  
    void Start()
    {
        modalPanel.SetActive(false);
       
        AppManager.instance.loadButton = fileButton;
        AppManager.instance.SelectedFile = "None";
    
       foreach (Transform child in contents) {
			GameObject.Destroy(child.gameObject);
		}
        
      
      System.Collections.Generic.List<string> files = SaveSystem.ListFiles();
        GameObject newFile;
    foreach (var item in files)
    {
  
            newFile = Instantiate (FileNames) as GameObject;
            FileDisplay localFile = newFile.GetComponent <FileDisplay> ();
            localFile.fileText.text = item;
          
            newFile.transform.SetParent (contents,false);

    }
    }

    public void Delete() {

   
        fileName.text = AppManager.instance.SelectedFile;
        modalPanel.SetActive(true);
     
    }

    public void Cancel()
    {
      
        modalPanel.SetActive(false);
     
    }

    public void ConfirmDelete()
    {
      
        AppManager.instance.DeleteFile();
     
   
        Start();
    }

    public void Back()
    {

        
        AppManager.instance.LoadScene(0);
    }


  
   
}
