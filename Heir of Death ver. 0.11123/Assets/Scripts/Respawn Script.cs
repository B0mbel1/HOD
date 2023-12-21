using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 respawnPoint = new Vector3(120f, 1f, 150f);

    void Start()
    {
        // Funkcja wywo³ana przy uruchomieniu gry
        RespawnPlayer();
    }

    void RespawnPlayer()
    {
        // ZnajdŸ obiekt gracza (zmieñ "Player" na nazwê rzeczywistego obiektu gracza)
        GameObject player = GameObject.FindWithTag("Respawn");

        // SprawdŸ, czy znaleziono gracza
        if (player != null)
        {
            // Ustaw pozycjê gracza na punkt respawnu
            player.transform.position = respawnPoint;
        }
        else
        {
            Debug.LogError("Nie znaleziono obiektu gracza. Upewnij siê, ¿e obiekt gracza ma tag 'Player'.");
        }
    }
}
