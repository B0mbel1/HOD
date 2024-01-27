using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Database", menuName = "Characters/Database")]
public class CharacterDatabase : ScriptableObject
{
    [SerializeField] private CharacterOnline[] characters = new CharacterOnline[0];

    public CharacterOnline[] GetAllCharactersOnline() => characters;

    public CharacterOnline GetCharacterById(int id)
    {
        foreach(var character in characters)
        {
            if(character.Id == id) return character;
        }
        return null;
    }

    public bool IsValidCharacterId(int id)
    {
        return characters.Any(x=> x.Id == id);
    }
}
