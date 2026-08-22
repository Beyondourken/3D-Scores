using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;

public class AppManager : MonoBehaviour
{

// [SerializeField] TextMeshProUGUI day;
// [SerializeField] TextMeshProUGUI month;
// [SerializeField] TextMeshProUGUI year;
[SerializeField] int dd;
[SerializeField] int mm;
[SerializeField] int yyyy;
// [SerializeField] Slider slider;
// [SerializeField] TextMeshProUGUI NOTText;
// int NoOfTargets;
List<Text> names;



 public static AppManager instance;
//public CompetitionManager competitionManager{get; set;}
 private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        dd = DateTime.Now.Day;
        mm = DateTime.Now.Month;
        yyyy = DateTime.Now.Year;
        // day.text = DateTime.Now.Day.ToString();
        // month.text = DateTime.Now.Month.ToString();
        // year.text = DateTime.Now.Year.ToString();
        //StartCoroutine(UpdateDate(day,dd.ToString()));

    }

// void Update()
//     {
//         NOTText.text = slider.value.ToString();
//         NoOfTargets = (int)slider.value;
//     }
//    IEnumerator UpdateDate(TextMeshProUGUI obj, string value)
//         {
//             obj.gameObject.SetActive(false);
//             obj.text = DateTime.Now.Day.ToString();
//             yield return new WaitForEndOfFrame();
//             obj.gameObject.SetActive(true);
//         }
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        // SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }
    // public int GetNumberOfTargets()
    // {
    //     return NoOfTargets;
    // }
  
    public void SaveScores()
    {
       
        SaveSystem.Save();
    }
   
 public void LoadScores()
    {
        SaveSystem.Load();
    }
}
