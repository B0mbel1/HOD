using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    public static float health;
    public bool czyHost;
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

        if (IsHost)
        {
            czyHost = true;
        }

        if (IsClient && !IsHost)
        {
            czyHost = false;
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            health = health - 20;
            Debug.Log("health:" + health);
        }

        UpdateHealthUI();
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

    public void Damage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    [ServerRpc]
    public void SynchroServerRpc(float healAmount)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { 1 }
            }
        };

        RestoreHealthClientRpc(healAmount, clientRpcParams);
    }

    [ServerRpc]
    public void RestoreHealthServerRpc(float healAmount)
    {
        health += healAmount;
        healAmount = 0;
        lerpTimer = 0f;
    }

    [ClientRpc]
    public void RestoreHealthClientRpc(float healAmount, ClientRpcParams clientRpcParams)
    {
        health += healAmount;
        healAmount = 0;
        lerpTimer = 0f;
    }
}
