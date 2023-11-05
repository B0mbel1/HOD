using UnityEngine;

public class ChangeShaderOnClick : MonoBehaviour
{
    public Shader XrayShader;
    public Shader standardShader;

    public float TimeLeft;
    public bool PowerOn;
    public float CooldownValue;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Zapamiêtaj oryginalny shader
            standardShader = renderer.material.shader;
        }
        PowerOn = false;
    }

    private void FixedUpdate()
    {
        if (Main.Power == 3)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ChangeObjectShader();
                PowerOn = true;
            }
            if (PowerOn)
            {
                if (TimeLeft > 0)
                {
                    TimeLeft -= Time.fixedDeltaTime;
                    ChangeObjectShader();
                }
                else
                {
                    PowerOn = false;
                    TimeLeft = CooldownValue;
                    ChangeObjectShaderToDefault();
                }
            }
        }

    }

    void ChangeObjectShader()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && XrayShader != null)
        {
            renderer.material.shader = XrayShader;
        }
        else
        {
            Debug.LogError("Renderer or myCustomShader is not set.");
        }
    }
    void ChangeObjectShaderToDefault()
    {
        Renderer renderer2 = GetComponent<Renderer>();

        if (renderer2 != null && standardShader != null)
        {
            renderer2.material.shader = standardShader;
        }
        else
        {
            Debug.LogError("Renderer or myCustomShader is not set.");
        }
    }

}

//using UnityEngine;

//public class ChangeShaderOnClick : MonoBehaviour
//{
//    public Shader myCustomShader; // Przypisz w³asny shader w inspektorze
//    private Shader originalShader; // Ta zmienna przechowa oryginalny shader

//    private void Start()
//    {
//        Renderer renderer = GetComponent<Renderer>();
//        if (renderer != null)
//        {
//            // Zapamiêtaj oryginalny shader
//            originalShader = renderer.material.shader;
//        }
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Q))
//        {
//            ToggleShader();
//        }
//    }

//    void ToggleShader()
//    {
//        Renderer renderer = GetComponent<Renderer>();

//        if (renderer != null && myCustomShader != null)
//        {
//            if (renderer.material.shader == myCustomShader)
//            {
//                // Jeœli aktualny shader to myCustomShader, przywróæ oryginalny shader
//                renderer.material.shader = originalShader;
//            }
//            else
//            {
//                // W przeciwnym razie, ustaw shader na myCustomShader
//                renderer.material.shader = myCustomShader;
//            }
//        }
//        else
//        {
//            Debug.LogError("Renderer or myCustomShader is not set.");
//        }
//    }
//}

