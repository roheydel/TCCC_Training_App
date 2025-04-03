using TMPro;
using UnityEngine;

public class StudentNamesCloseButton : MonoBehaviour
{
    public AppGlobal GlobalObj;
    public TMP_InputField InputTextField;

    public void OnButtonClick()
    {
        GlobalObj.SetStudentNames(InputTextField.text);
        InputTextField.text = null;
    }
}
