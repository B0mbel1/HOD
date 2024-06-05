using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabReg : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField password2;
    
    public int InputSelected;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelected++;
            if (InputSelected == 4)
            {
                InputSelected = 0;
            }
            SelectedInputField();
        }
    }

    void SelectedInputField()
    {
        switch (InputSelected)
        {
            case 0:
                username.Select();
                break;
            case 1:
                email.Select();
                break;
            case 2:
                password.Select(); 
                break;
            case 3:
                password2.Select(); 
                break;
        }
    }

    public void usernameSelected() => InputSelected = 0;
    public void emailSelected() => InputSelected = 1;
    public void passwordSelected() => InputSelected = 2;
    public void password2Selected() => InputSelected = 3;
}
