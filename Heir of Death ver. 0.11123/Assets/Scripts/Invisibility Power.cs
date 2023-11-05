using UnityEngine;
using UnityEngine.UI;

public class InvisibilityPower : MonoBehaviour
{
    private Renderer playerRenderer;
    private bool isInvisible = false;
    private float cooldownTime = 10.0f;
    private float invisibleDuration = 5.0f;
    private float cooldownTimer = 0.0f;
    public Text PowerTimer;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        // Upewnij siê, ¿e gracz jest widoczny na pocz¹tku.
        SetVisibility(true);
        PowerTimer.GetComponent<Text>().enabled = false;
    }

    private void Update()
    {
        if (isInvisible)
        {
            // Odliczanie czasu niewidzialnoœci.
            invisibleDuration -= Time.deltaTime;

            if (invisibleDuration <= 0)
            {
                // Niewidzialnoœæ skoñczy³a siê, w³¹cz cooldown.
                isInvisible = false;
                cooldownTimer = cooldownTime;
                SetVisibility(false);
            }
        }
        else if (cooldownTimer > 0)
        {
            // Odliczanie czasu do zakoñczenia cooldownu.
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                // Koniec cooldownu, gracz mo¿e ponownie staæ siê niewidzialny.
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    isInvisible = true;
                    SetVisibility(true);
                    invisibleDuration = 5.0f;
                }
            }
        }
        else
        {
            // Gracz mo¿e staæ siê niewidzialny, jeœli klawisz "Q" zosta³ naciœniêty.
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isInvisible = true;
                SetVisibility(true);
                invisibleDuration = 5.0f;
            }
        }
    }

    private void SetVisibility(bool isVisible)
    {
        // W³¹cz/wy³¹cz renderowanie gracza, aby go uczyniæ widzialnym/niewidzialnym.
        playerRenderer.enabled = isVisible;
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        PowerTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
