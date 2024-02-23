using UnityEngine;
using UnityEngine.UI;

public class Damaging : MonoBehaviour, obrazeniaOK
{
    public int maxHealth;
    private int currentHealth;
    public GameObject Can;
    public static int wynik;

    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Color maxHealthColor;
    [SerializeField] private Color zeroHealthColor;
    
    void Start()
    {
        currentHealth = maxHealth;
        SetHealtbarUI();
        wynik = 0;
    }
    
    public void sprawdzCzyDead()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Can.SetActive(false);
            wynik++;
        }
    }

    public void zadajObrazenia(int obrazenia)
    {
        currentHealth -= obrazenia;
        sprawdzCzyDead();
        SetHealtbarUI();
    }

    private void SetHealtbarUI()
    {
        float healthPercentage = CalculateHealthPercentage();
        healthBarSlider.value = healthPercentage;
        healthBarFillImage.color = Color.Lerp(zeroHealthColor,maxHealthColor,healthPercentage/100);
    }

    private float CalculateHealthPercentage()
    {
        return ((float)currentHealth / (float)maxHealth) * 100;
    }
}
