using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damaging : MonoBehaviour, obrazeniaOK
{

    public int maxHealth;
    private int currentHealth;
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Color maxHealthColor;
    [SerializeField] private Color zeroHealthColor;
    public GameObject Can;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        SetHealtbarUI();
    }

    
    public void sprawdzCzyDead()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Can.SetActive(false);
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
