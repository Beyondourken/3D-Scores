using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [SerializeField] List<GameObject> rows;    
 int numberOfTargets = 0;

void Start ()
    {
       numberOfTargets = CompetitionManager.instance.GetNumberOfTargets();
       for (int i = numberOfTargets; i < 6; i++)
       {
            rows[i].SetActive(false);
       }
    }
}
