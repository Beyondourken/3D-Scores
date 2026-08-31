using TMPro;
using UnityEngine;

public class FileDisplay : MonoBehaviour
{
      [SerializeField] public TextMeshProUGUI fileText;  

      public void SelectFile(TextMeshProUGUI fileName)
      {
            AppManager.instance.SelectFile(fileName.text);
      }  
}
