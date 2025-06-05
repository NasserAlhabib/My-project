using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterCardController : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private List<CharacterDataSO> charactersData = new List<CharacterDataSO>();
    [Header("UI References")]
    [SerializeField] private TMP_Text characterNameTxt;
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterDescription;
    [SerializeField] private GameObject[] readyStatus; // UnReady = 0 , Ready = 1

    private bool isReady;
    private int currentIndex;

    public void OnNavigate(InputAction.CallbackContext context)
    {
        float direction = context.ReadValue<Vector2>().x;

        if (direction == -1)
        {
            ChangeCurrentSelection(-1);
        }

        if (direction == 1)
        {
            ChangeCurrentSelection(1);
        }
    }

    private void ChangeCurrentSelection(int dir)
    {
        currentIndex = (currentIndex + dir + charactersData.Count) % charactersData.Count;

        DisplayCharacter();
    }

    private void DisplayCharacter()
    {
        CharacterDataSO currentCharacter = charactersData[currentIndex];

        characterNameTxt.text = currentCharacter.CharacterName;
        characterImage.sprite = currentCharacter.CharacterIcon;
        characterImage.color = currentCharacter.CharacterColor;
        characterDescription.text = currentCharacter.Description;
    }

}
