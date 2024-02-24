using UnityEngine;
using UnityEngine.UI;

public class RealTimeAntialiasing : MonoBehaviour
{
    [SerializeField] private Slider antialiasingSlider;
    private const string AntiAliasingPrefsKey = "AntialiasingQuality";

    private void Start()
    {
        LoadAntialiasingQuality();
    }

    public void OnAntialiasingValueChanged()
    {
        UpdateAntialiasing();
    }

    private void UpdateAntialiasing()
    {
        int qualityLevel = (int)antialiasingSlider.value;
        QualitySettings.antiAliasing = qualityLevel;
        SaveAntialiasingQuality(qualityLevel);
    }

    private void SaveAntialiasingQuality(int qualityLevel)
    {
        PlayerPrefs.SetInt(AntiAliasingPrefsKey, qualityLevel);
        PlayerPrefs.Save();
    }

    private void LoadAntialiasingQuality()
    {
        if (PlayerPrefs.HasKey(AntiAliasingPrefsKey))
        {
            int savedQualityLevel = PlayerPrefs.GetInt(AntiAliasingPrefsKey);
            antialiasingSlider.value = savedQualityLevel;
            QualitySettings.antiAliasing = savedQualityLevel;
        }
    }

    private void OnApplicationQuit()
    {
        SaveAntialiasingQuality((int)antialiasingSlider.value);
    }
}

