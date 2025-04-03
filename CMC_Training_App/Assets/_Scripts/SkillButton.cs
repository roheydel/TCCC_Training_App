using UnityEngine;
using UnityEngine.UI;

public sealed class SkillButton : MonoBehaviour
{
    public AppGlobal GlobalObj;

    private Button _button;
    private bool _preparePressed;
    private bool _donePressed;

    private Color _prepareColor;
    private Color _doneColor;
    private Color _startColor;

    public void Start()
    {
        _button = GetComponent<Button>();
        _preparePressed = false;
        _donePressed = false;

        _startColor = Color.white;
        _doneColor = Color.green;
        _prepareColor = Color.yellow;

        Shared.UpdateButtonColor(_button, _startColor);

        AppGlobal.OnRefreshGlobalEvent += OnReset;
        SaveLoadSystem.OnLoadGlobalEvent += OnLoad;
        SaveLoadSystem.OnSaveGlobalEvent += OnSave;
    }

    public void Click()
    {
        if (!_preparePressed && GlobalObj.CurrentState == AppGlobal.State.Prepare)
        {
            PrepareButton();
        }
        else if (_preparePressed & !_donePressed & GlobalObj.CurrentState == AppGlobal.State.Working)
        {
            DoneButton();
        }
        else if (_preparePressed & _donePressed & GlobalObj.CurrentState == AppGlobal.State.Working)
        {
            UndoneButton();
        }
        else if (_preparePressed)
        {
            UnprepareButton();
        }
    }

    public void OnSave(SaveLoadSystem _)
    {
        if (_preparePressed)
        {
            SaveLoadSystem.LoadSaveObject!.PreparedButtons.Add(GetInstanceID());
        }
    }

    private void OnLoad(SaveLoadSystem _)
    {
        var id = GetInstanceID();
        if (!_preparePressed && SaveLoadSystem.LoadSaveObject!.PreparedButtons.Contains(id))
        {
            PrepareButton();
        }
        else if (_preparePressed && !SaveLoadSystem.LoadSaveObject!.PreparedButtons.Contains(id))
        {
            UnprepareButton();
        }
    }

    private void PrepareButton()
    {
        _preparePressed = true;
        Shared.UpdateButtonColor(_button, _prepareColor);
        GlobalObj.IncreaseSkillCount();
    }

    private void UnprepareButton()
    {
        _preparePressed = false;
        Shared.UpdateButtonColor(_button, _startColor);
        GlobalObj.DecreaseSkillCount();
    }

    private void DoneButton()
    {
        _donePressed = true;
        Shared.UpdateButtonColor(_button, _doneColor);
        GlobalObj.IncreaseSkillCount();
    }

    private void UndoneButton()
    {
        _donePressed = false;
        Shared.UpdateButtonColor(_button, _prepareColor);
        GlobalObj.DecreaseSkillCount();
    }

    private void OnReset(AppGlobal _)
    {
        if (_preparePressed)
        {
            Shared.UpdateButtonColor(_button, _prepareColor);
        }
        _donePressed = false;
    }
}
