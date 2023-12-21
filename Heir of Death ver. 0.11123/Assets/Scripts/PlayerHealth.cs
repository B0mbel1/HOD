using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2;
    public Image frontHealthBar;
    public Image backHealthBar;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        health = Mathf.Clamp(health,0,maxHealth);
        UpdateHealthUI();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(Random.Range(1,29));                  //Set damage depends on gun
        }
    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, fillF, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }
}
