using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
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

    private void Update()
    {
        if (!IsOwner) return;



        //Dzia³aj¹cy skrypt z zadawaniem obra¿eñ/////////////////////////
        if (Input.GetKeyDown(KeyCode.E) && IsHost)
        {
            DamageClientRpc(12f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
            
        }

        if (Input.GetKeyDown(KeyCode.T) && IsClient && !IsHost)
        {
            DamageServerRpc(12f);
            
        }

        //////////////////////////////////////////////

        UpdateHealthUI();
        //health = Mathf.Clamp(health, 0, maxHealth);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, fillF, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    [ServerRpc]
    public void DamageServerRpc(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    [ClientRpc]
    public void DamageClientRpc(float damage, ClientRpcParams clientRpcParams)
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
