using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterData", menuName = "Potato/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    public string CharacterName;
    public Sprite CharacterIcon;
    public string Description;
    public Color CharacterColor = Color.white;
    public GameObject CharacterPrefab;
}
