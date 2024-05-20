using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    public static float health;
    public bool czyHost;
    private static float lerpTimer;
    public const float maxHealth = 100;
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

        if (health > 100)
        {
            health = 100;
            Debug.Log("By³o powy¿ej 100 a teraz jest:" + health);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            health -= 20;
            Debug.Log("Zada³em 20 i jest teraz:" + health);
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
    public void SynchroServerRpc()
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { 1 }
            }
        };

        RestoreHealthClientRpc(clientRpcParams);
    }

    [ServerRpc]
    public void RestoreHealthServerRpc()
    {
        health += 100f;
        lerpTimer = 0f;
    }

    [ClientRpc]
    public void RestoreHealthClientRpc(ClientRpcParams clientRpcParams)
    {
        health += 100f;
        lerpTimer = 0f;

        Debug.Log(health);
    }
}
