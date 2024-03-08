using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{ 
    public Health Health;

    private static float health;
    private static float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2;

    public Image frontHealthBar;
    public Image backHealthBar;

    //private void Start()
    //{
    ////    health = maxHealth;
    // }
    public override void OnNetworkSpawn()
    {
        Health = FindObjectOfType<Health>();

        if (IsHost)
        {
            health = 100;
        }
        if(IsClient && !IsHost)
        {
            health = 50;
        }
        
        base.OnNetworkSpawn();
    }
    private void Update()
    {
        if (!IsOwner) return;
        health = Mathf.Clamp(health,0,maxHealth);
        UpdateHealthUI();
       // if (Input.GetKeyDown(KeyCode.F))
        //{
           // TestServerRpc();
            
        //}
    }

    //[ServerRpc(RequireOwnership = false)]
    //private void TestServerRpc()
    //{
        // Update client's health based on the health value from Health component
     //   health = Health.health1;
    //}


    public void UpdateHealthUI()
    {
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
        if(fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF,backHealthBar.fillAmount,percentComplete);
        }
    }

    public static void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public static void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
