using UnityEngine;
using UnityEngine.UI;

public sealed class StartButton : MonoBehaviour
{
    public AppGlobal GlobalObj;

    private Button _button;

    public void Start()
    {
        _button = GetComponent<Button>();
        AppGlobal.OnRefreshGlobalEvent += ResetButton;
    }

    public void StartButtonClick()
    {
        GlobalObj.StartScenario();
        Shared.UpdateButtonColor(_button, Color.grey);
        _button.interactable = false;
    }

    public void ResetButton (AppGlobal _)
    {
        Shared.UpdateButtonColor(_button, Color.white);
        _button.interactable = true;
    }
}
