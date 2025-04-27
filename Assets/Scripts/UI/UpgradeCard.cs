// UpgradeCard.cs
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public event Action<UpgradeDataSO> OnClicked;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button selectButton;

    private UpgradeDataSO data;

    public void Setup(UpgradeDataSO upgradeData)
    {
        data = upgradeData;
        nameText.text = data.upgradeName;
        descriptionText.text = data.description;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => OnClicked?.Invoke(data));
    }
}
