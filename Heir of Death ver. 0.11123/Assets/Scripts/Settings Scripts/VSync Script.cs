using UnityEngine;
using UnityEngine.UI;

public class VSyncScript : MonoBehaviour
{
    [SerializeField] private Slider vSyncSlider;
    private const string vSyncPrefsKey = "vSyncQuality";

    private void Start()
    {
        LoadvSyncQuality();
    }

    public void OnvSyncValueChanged()
    {
        UpdatevSync();
    }

    private void UpdatevSync()
    {
        int qualityLevel = (int)vSyncSlider.value;
        QualitySettings.vSyncCount = qualityLevel;
        SavevSyncQuality(qualityLevel);
    }

    private void SavevSyncQuality(int qualityLevel)
    {
        PlayerPrefs.SetInt(vSyncPrefsKey, qualityLevel);
        PlayerPrefs.Save();
    }

    private void LoadvSyncQuality()
    {
        if (PlayerPrefs.HasKey(vSyncPrefsKey))
        {
            int savedQualityLevel = PlayerPrefs.GetInt(vSyncPrefsKey);
            vSyncSlider.value = savedQualityLevel;
            QualitySettings.vSyncCount = savedQualityLevel;
        }
    }

    private void OnApplicationQuit()
    {
        SavevSyncQuality((int)vSyncSlider.value);
    }
}
