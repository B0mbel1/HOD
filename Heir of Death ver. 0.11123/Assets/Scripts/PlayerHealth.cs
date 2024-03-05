using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] public static CharacterID characterId;

    public static float health;
    private static float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed = 2;

    public Image frontHealthBar;
    public Image backHealthBar;

    private void Start()
    {
        health = maxHealth;
    }

    public void OnHealthChanged(float oldValue, float newValue)
    {
        UpdateHealthUI();
    }

    private void Update()
    {
        //if (!IsOwner) { return; }


        health = Mathf.Clamp(health,0,maxHealth);
        UpdateHealthUI();
  
    }

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

    //public /*static*/ void TakeDamage(float damage)
    //{
    //    health -= damage;
    //    lerpTimer = 0f;
    //}

    public static void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    [ServerRpc(RequireOwnership = false)]
    public void CmdTakeDamageServerRpc(float damage)
    {
        RpcTakeDamageClientRpc(damage);
    }

    [ClientRpc]
    private void RpcTakeDamageClientRpc(float damage)
    {
        if (IsOwner)
        {
            health -= damage;
            lerpTimer = 0f;
        }
        if (IsClient)
        {

        }
        
    }

    //[ServerRpc]
    //public void CmdRestoreHealth(float healAmount)
    //{
    //    RpcRestoreHealth(healAmount);
    //}

    //[ClientRpc]
    //private void RpcRestoreHealth(float healAmount)
    //{
    //    health += healAmount;
    //    lerpTimer = 0f;
    //}
}
