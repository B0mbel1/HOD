using UnityEngine;
using UnityEngine.UI;

public class ShadowsScript : MonoBehaviour
{
    [SerializeField] private Slider shadowSlider;
    private const string shadowPrefsKey = "shadowQuality";

    private void Start()
    {
        LoadShadowQuality();
    }

    public void OnShadowValueChanged()
    {
        UpdateShadow();
    }

    private void UpdateShadow()
    {
        int qualityLevel = (int)shadowSlider.value;
        switch (qualityLevel)
        {
            case 0:
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
            case 1:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
            case 2:
                QualitySettings.shadowResolution = ShadowResolution.High;
                break;
            case 3:
                QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                break;
        }
        SaveShadowQuality(qualityLevel);
    }

    private void SaveShadowQuality(int qualityLevel)
    {
        PlayerPrefs.SetInt(shadowPrefsKey, qualityLevel);
        PlayerPrefs.Save();
    }

    private void LoadShadowQuality()
    {
        if (PlayerPrefs.HasKey(shadowPrefsKey))
        {
            int savedQualityLevel = PlayerPrefs.GetInt(shadowPrefsKey);
            shadowSlider.value = savedQualityLevel;

            switch (savedQualityLevel)
            {
                case 0:
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    break;
                case 1:
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    break;
                case 2:
                    QualitySettings.shadowResolution = ShadowResolution.High;
                    break;
                case 3:
                    QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                    break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveShadowQuality((int)shadowSlider.value);
    }
}
