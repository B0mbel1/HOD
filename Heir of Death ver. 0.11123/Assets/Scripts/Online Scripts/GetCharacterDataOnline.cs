using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GetCharacterDataOnline : NetworkBehaviour

{
    public static int selectedCharacter = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    CharacterOnline choose = new CharacterOnline();

    public void SetChar1()
    {
        selectedCharacter = 1;
        choose.CmdChangeCharacterServerRpc(selectedCharacter);
    }

    public void SetChar2()
    {
        selectedCharacter = 2;
        choose.CmdChangeCharacterServerRpc(selectedCharacter);
    }

    public void SetChar3()
    {
        selectedCharacter = 3;
        choose.CmdChangeCharacterServerRpc(selectedCharacter);
    }

    public void SetChar4()
    {
        selectedCharacter = 4;
        choose.CmdChangeCharacterServerRpc(selectedCharacter);
    }

    public void SetChar5()
    {
        selectedCharacter = 5;
        choose.CmdChangeCharacterServerRpc(selectedCharacter);
    }

    */
}
