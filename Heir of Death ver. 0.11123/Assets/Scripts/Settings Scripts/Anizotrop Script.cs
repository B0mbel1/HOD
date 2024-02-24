using UnityEngine;
using UnityEngine.UI;

public class RealTimeAnisotropicFiltering : MonoBehaviour
{
    [SerializeField] private Slider anisotropySlider;
    private const string AnisotropicPrefsKey = "AnisotropicQuality";

    private void Start()
    {
        LoadAnisotropicQuality();
    }

    public void OnAnisotropyValueChanged()
    {
        UpdateAnisotropy();
    }

    private void UpdateAnisotropy()
    {
        int qualityLevel = (int)anisotropySlider.value;

        switch (qualityLevel)
        {
            case 0:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                break;
            case 1:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                break;
            case 2:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                break;
        }

        SaveAnisotropicQuality(qualityLevel);
    }

    private void SaveAnisotropicQuality(int qualityLevel)
    {
        PlayerPrefs.SetInt(AnisotropicPrefsKey, qualityLevel);
        PlayerPrefs.Save();
    }

    private void LoadAnisotropicQuality()
    {
        if (PlayerPrefs.HasKey(AnisotropicPrefsKey))
        {
            int savedQualityLevel = PlayerPrefs.GetInt(AnisotropicPrefsKey);
            anisotropySlider.value = savedQualityLevel;

            switch (savedQualityLevel)
            {
                case 0:
                    QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                    break;
                case 1:
                    QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                    break;
                case 2:
                    QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                    break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveAnisotropicQuality((int)anisotropySlider.value);
    }
}
