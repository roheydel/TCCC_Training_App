using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public sealed class SaveLoadSystem : MonoBehaviour
{
    private string _saveLoadDirectoryPath;

    public TMP_InputField ScenarioNameInputField;
    public TMP_Dropdown SavedScenariosDropdown;

    public static event Action<SaveLoadSystem> OnSaveGlobalEvent;
    public static event Action<SaveLoadSystem> OnLoadGlobalEvent;

    public static LoadSaveData LoadSaveObject { get; set; } = new();

    public void Start()
    {
        _saveLoadDirectoryPath = Path.Combine(Application.persistentDataPath, "Scenarios");
        if (!Directory.Exists(_saveLoadDirectoryPath))
        {
            Directory.CreateDirectory(_saveLoadDirectoryPath);
        }

        UpdateDropdown();
    }

    public void Save()
    {
        OnSaveGlobalEvent?.Invoke(this);

        var scenarioNameFixed = ScenarioNameInputField.text.Replace(' ', '_');

        //var scenarioFilePath = Path.Combine(_saveLoadDirectoryPath,
        //    $"{_scenarioNameFixed}.json");

        //if (File.Exists(scenarioFilePath))
        //{
        //    // popup warning message
        //}
        //else
        //{
        //using var fileStream = File.OpenWrite(scenarioFilePath);
        //using var writer = new StreamWriter(fileStream);
        var dataJson = JsonConvert.SerializeObject(LoadSaveObject);
        //writer.Write(dataJson);
        var savedFiles = PlayerPrefs.GetString("fileNames") + scenarioNameFixed + ";";
        PlayerPrefs.SetString("fileNames", savedFiles);
        PlayerPrefs.SetString(scenarioNameFixed, dataJson);
        PlayerPrefs.Save();
        //}
    }

    public void Load()
    {
        var selectedFileName = SavedScenariosDropdown.options[SavedScenariosDropdown.value].text;
        //var loadPath = Path.Combine(_saveLoadDirectoryPath, selectedFileName);

        //if (File.Exists(loadPath))
        //{
        //    using var fileStream = File.OpenRead(loadPath);
        //    using var reader = new StreamReader(fileStream);
        //    LoadSaveObject = JsonConvert.DeserializeObject<LoadSaveData>(reader.ReadToEnd());
        //    OnLoadGlobalEvent?.Invoke(this);
        //}
        //else
        if (PlayerPrefs.HasKey(selectedFileName))
        {
            var save = PlayerPrefs.GetString(selectedFileName);
            LoadSaveObject = JsonConvert.DeserializeObject<LoadSaveData>(save);
            OnLoadGlobalEvent?.Invoke(this);
        }
    }

    public void UpdateDropdown()
    {
        SavedScenariosDropdown.ClearOptions();

        //var dirInfo = new DirectoryInfo(_saveLoadDirectoryPath);
        //var savedFiles = dirInfo.GetFiles();
        var savedFiles = PlayerPrefs.GetString("fileNames").Split(';').ToList();
        var optionsData = savedFiles
            .Select(x => new TMP_Dropdown.OptionData { text = x })
            .ToList();
        SavedScenariosDropdown.AddOptions(optionsData);
    }
}

public sealed class LoadSaveData
{
    public List<int> PreparedButtons { get; set; } = new();
    public InputVitals InputVitals { get; set; } = new();
    public List<InputLine> InputLines { get; set; } = new();
}

public sealed class InputVitals
{
    public string PulseRate { get; set; }
    public string Breathing { get; set; }
    public string SpO2 { get; set; }
    public string AVPU { get; set; }
    public string Pain { get; set; }
    public string Legend { get; set; }
}

public sealed class InputLine
{
    public string Color { get; set; }
    public List<LinePoint> Points { get; set; }
}

public sealed class LinePoint
{
    public float X { get; set; }
    public float Y { get; set; }
}