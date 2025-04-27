using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Upgrades/UpgradeData")]
public class UpgradeDataSO : ScriptableObject
{
    [Header("-------- Upgrade Name --------")]
    [Tooltip("Name of the upgrade")]
    public string upgradeName;
    //--------------------------------------------------------------------------
    [Header("-------- Upgrade Type --------")]
    [Tooltip("Type of the upgrade")]
    public UpgradeType type;
    //--------------------------------------------------------------------------
    [Header("-------- Upgrade Value --------")]
    [Tooltip("Value of the upgrade")]
    public float value;
    //--------------------------------------------------------------------------
    [Header("-------- Upgrade Description --------")]
    [TextArea] public string description;
}