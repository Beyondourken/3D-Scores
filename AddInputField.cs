using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AddInputField : MonoBehaviour
{
    [SerializeField] public TMP_InputField input;
    [SerializeField] public string originalValue;  


    void Start()
    {
        originalValue = input.text;
    } 
   public void AddField() {
   CompetitionManager.instance.GenerateInputField(input);
   
   }
    public void AddEmptyField() {
    CompetitionManager.instance.GenerateEmptyInputField();
   
   }
   public void NameEdited()
    { 
        if(input.text != originalValue){
        CompetitionManager.instance.NameEdited();}
    }
}
