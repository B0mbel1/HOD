using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tab : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField password;
    public int InputSelected;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelected++;
            if(InputSelected == 2)
            {
                InputSelected = 0;
            }
            SelectedInputField();
        }
    }

    void SelectedInputField()
    {
        switch(InputSelected)
        {
            case 0: login.Select();
                break;
            case 1: password.Select(); 
                break;
        }
    }

    public void loginSelected() => InputSelected = 0;
    public void passwordSelected() => InputSelected = 1;

}
