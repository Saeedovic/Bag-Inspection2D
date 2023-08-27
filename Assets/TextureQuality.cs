using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureQuality : MonoBehaviour
{
    Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.options.Clear();

        foreach (string qualityName in QualitySettings.names)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = qualityName });
        }

        dropdown.onValueChanged.AddListener(delegate { SetQuality(dropdown); });
    }

    public void SetQuality(Dropdown change)
    {
        int newQualityLevel = Mathf.Clamp(change.value, 0, QualitySettings.names.Length - 1);
        QualitySettings.SetQualityLevel(newQualityLevel, true);
    }
}
