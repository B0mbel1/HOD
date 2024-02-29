using UnityEngine;
using UnityEngine.UI;

public class TextureScript : MonoBehaviour
{
    [SerializeField] private Slider textureSlider;
    private const string TexturePrefsKey = "textureQuality";

    private void Start()
    {
        LoadTextureQuality();
    }

    public void OnTextureValueChanged()
    {
        UpdateTexture();
    }

    private void UpdateTexture()
    {
        int qualityLevel = (int)textureSlider.value;
        QualitySettings.globalTextureMipmapLimit = qualityLevel;
        SaveTextureQuality(qualityLevel);
    }

    private void SaveTextureQuality(int qualityLevel)
    {
        PlayerPrefs.SetInt(TexturePrefsKey, qualityLevel);
        PlayerPrefs.Save();
    }

    private void LoadTextureQuality()
    {
        if (PlayerPrefs.HasKey(TexturePrefsKey))
        {
            int savedQualityLevel = PlayerPrefs.GetInt(TexturePrefsKey);
            textureSlider.value = savedQualityLevel;
            QualitySettings.globalTextureMipmapLimit = savedQualityLevel;
        }
    }

    private void OnApplicationQuit()
    {
        SaveTextureQuality((int)textureSlider.value);
    }
}
