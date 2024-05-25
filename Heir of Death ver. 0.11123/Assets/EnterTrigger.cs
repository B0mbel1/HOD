using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterTrigger : MonoBehaviour
{
    public KeyCode key;
    public Button but;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            but.onClick.Invoke();
        }
    }
}
