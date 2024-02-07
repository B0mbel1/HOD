using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject disabledOverlay;
    [SerializeField] private Button button;

    private CharacterSelectDisplay characterSelect;

    public CharacterOnline CharacterOnline { get; private set; }

    public bool IsDisabled { get; private set; }

    public void SetCharacter(CharacterSelectDisplay characterSelect, CharacterOnline character)
    {
        iconImage.sprite = character.Icon;
        this.characterSelect = characterSelect;
        CharacterOnline = character;
    }

    public void SelectCharacter()
    {
        characterSelect.Select(CharacterOnline);
    }

    public void SetDisabled()
    {
        IsDisabled = true;
        disabledOverlay.SetActive(true);
        button.interactable = false;
    }
}
