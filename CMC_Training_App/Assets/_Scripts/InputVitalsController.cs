using TMPro;
using UnityEngine;

public sealed class InputVitalsController : MonoBehaviour
{
    public TMP_InputField PulseRateField;
    public TMP_InputField BreathingField;
    public TMP_InputField SpO2Field;
    public TMP_InputField AVPUField;
    public TMP_InputField PainField;
    public TMP_InputField LegendField;

    public void Start()
    {
        SaveLoadSystem.OnLoadGlobalEvent += OnLoad;
        SaveLoadSystem.OnSaveGlobalEvent += OnSave;
    }

    private void OnLoad(SaveLoadSystem _)
    {
        PulseRateField.text = SaveLoadSystem.LoadSaveObject!.InputVitals.PulseRate;
        BreathingField.text = SaveLoadSystem.LoadSaveObject!.InputVitals.Breathing;
        SpO2Field.text = SaveLoadSystem.LoadSaveObject!.InputVitals.SpO2;
        AVPUField.text = SaveLoadSystem.LoadSaveObject!.InputVitals.AVPU;
        PainField.text = SaveLoadSystem.LoadSaveObject!.InputVitals.Pain;
        LegendField.text = SaveLoadSystem.LoadSaveObject!.InputVitals.Legend;
    }

    private void OnSave(SaveLoadSystem _)
    {
        SaveLoadSystem.LoadSaveObject!.InputVitals.PulseRate = PulseRateField.text;
        SaveLoadSystem.LoadSaveObject!.InputVitals.Breathing = BreathingField.text;
        SaveLoadSystem.LoadSaveObject!.InputVitals.SpO2 = SpO2Field.text;
        SaveLoadSystem.LoadSaveObject!.InputVitals.AVPU = AVPUField.text;
        SaveLoadSystem.LoadSaveObject!.InputVitals.Pain = PainField.text;
        SaveLoadSystem.LoadSaveObject!.InputVitals.Legend = LegendField.text;
    }
}
