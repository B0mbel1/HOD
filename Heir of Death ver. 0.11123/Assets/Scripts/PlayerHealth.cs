using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class PlayerHealth : NetworkBehaviour
{ 


public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] public static CharacterID characterId;

    public static NetworkVariable<float> health = new NetworkVariable<float>();


    private static float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed = 2;

    public Image frontHealthBar;
    public Image backHealthBar;

    //private void Start()
    //{
    ////    health = maxHealth;
    // }
    public override void OnNetworkSpawn()
    {

        health.Value = maxHealth;
    }


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

        if (!IsOwner) { return; }
        //health.Value = Mathf.Clamp(health.Value, 0, maxHealth);
        //UpdateHealthUILocal();
        health.Value = Mathf.Clamp(health.Value, 0, maxHealth);
        UpdateHealthUI();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

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
        float hFraction = health.Value / maxHealth;
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

    [ServerRpc(RequireOwnership = false)]
    public void CmdTakeDamageServerRpc(float damage)
    {
        RpcTakeDamageClientRpc(damage);
    }

    [ClientRpc]
    private void RpcTakeDamageClientRpc(float damage)
    {
        health.Value -= damage;
        lerpTimer = 0f;
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

    public static void RestoreHealth(float healAmount)
    {
        health.Value += healAmount;
        lerpTimer = 0f;
    }

    //public void OnHealthChanged(float oldValue, float newValue)
    //{
    //    UpdateHealthUI();
    //}
}
