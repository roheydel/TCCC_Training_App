using System;
using UnityEngine;

public sealed class AppGlobal : MonoBehaviour
{
    private int _requiredSkillsCount = 0;
    private int _doneSkillsCount = 0;
    private State _currentState;

    private string _studentNames;

    public static event Action<AppGlobal> OnRefreshGlobalEvent;

    public void Start()
    {
        Application.targetFrameRate = 120;
        _currentState = State.Prepare;
    }

    public void StartScenario()
    {
        _currentState = State.Working;
    }

    public State CurrentState => _currentState;

    public void SetStudentNames(string value)
    {
        _studentNames = value;
    }

    public string StudentNames => _studentNames;

    public void IncreaseSkillCount()
    {
        if (_currentState == State.Prepare)
        {
            _requiredSkillsCount++;
        }
        if (_currentState == State.Working)
        {
            _doneSkillsCount++;
        }
        Debug.Log($"Increasing done, current state is '{_currentState}' \n" +
            $"Prepared skills: {_requiredSkillsCount}, Done skills: {_doneSkillsCount}.");
    }

    public void DecreaseSkillCount()
    {
        if (_currentState == State.Prepare)
        {
            _requiredSkillsCount--;
        }
        if (_currentState == State.Working)
        {
            _doneSkillsCount--;
        }
        Debug.Log($"Decreasing done, current state is '{_currentState}' \n" +
        $"Prepared skills: {_requiredSkillsCount}, Done skills: {_doneSkillsCount}.");
    }

    public string CalculateSuccessRate()
    {
        return ((int)(_doneSkillsCount * 1.0 / _requiredSkillsCount * 100)).ToString();
    }

    public void RefreshScenario()
    {
        _doneSkillsCount = 0;
        _currentState = State.Prepare;
        OnRefreshGlobalEvent?.Invoke(this);
    }

    public enum State
    {
        Prepare,
        Working
    };
}


