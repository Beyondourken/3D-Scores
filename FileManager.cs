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
    [SerializeField] GameObject coverImage;

    // String deleteMessage = "Are you sure you want to delete this file?";
    //String archiveMessage = "Are you sure you want to archive this file?";
    //bool deletefile = false;
    //bool archivefile = false;
    void Start()
    {
        modalPanel.SetActive(false);
        coverImage.SetActive(true);
        AppManager.instance.cover = coverImage;
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
    // public void Archive()

    // {
    //     confirmMessage.text = archiveMessage;
    //     fileName.text = AppManager.instance.SelectedFile;
    //     modalPanel.SetActive(true);
    //     archivefile = true;
    //     deletefile = false;
    // }
    public void Delete()

    {
        //confirmMessage.text = deleteMessage;
        fileName.text = AppManager.instance.SelectedFile;
        modalPanel.SetActive(true);
        // deletefile = true;
        // archivefile = false;
    }

    public void Cancel()
    {
        // deletefile = false;
        // archivefile = false;
        modalPanel.SetActive(false);
        coverImage.SetActive(true);
    }

    public void ConfirmDelete()
    {
        // if(deletefile) {
        AppManager.instance.DeleteFile();
        // }
        // if(archivefile) {
        // AppManager.instance.ArchiveFile();
        // }
        // deletefile = false;
        // archivefile = false;
        coverImage.SetActive(true);
        Start();
    }

    public void Back()
    {
      //  deletefile = false;
        
        AppManager.instance.LoadScene(0);
    }


  
   
}
