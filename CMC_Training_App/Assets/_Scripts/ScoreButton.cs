using UnityEngine;
using TMPro;

public sealed class FinishButton : MonoBehaviour
{
    public AppGlobal GlobalObj;
    public TMP_Text ScoreTextArea;
    public TMP_Text StudentNamesTextArea;

    public void ScoreButtonClick()
    {
        var successRate = GlobalObj.CalculateSuccessRate();

        ScoreTextArea.text = $"{successRate}%";
        StudentNamesTextArea.text = GlobalObj.StudentNames;

        //GlobalObj.RestartScenario();
    }
}
