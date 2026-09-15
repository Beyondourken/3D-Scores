using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddInputField : MonoBehaviour
{
    [SerializeField] public TMP_InputField input;
   
   public void AddField() {
   CompetitionManager.instance.GenerateInputField(input);
   
   }
public void AddEmptyField() {
    CompetitionManager.instance.GenerateEmptyInputField();
   
   }
   public void NameEdited()
    { 
        CompetitionManager.instance.NameEdited();
    }
}
