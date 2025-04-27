using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomUpgradePanel : RoomPanelBase
{
    [Header("UI")]
    [SerializeField] private Transform cardContainer;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private int numberOfChoices = 3;

    [Header("Audio")]
    [SerializeField] private GameObject upgradeAudioPrefab;

    private readonly List<UpgradeCard> currentCards = new List<UpgradeCard>();

    public override void InitializePanle(LobbyUIManager lobbyManager)
    {
        base.InitializePanle(lobbyManager);
        audioSource = GetComponent<AudioSource>();
        PongManager.Instance.OnGameStateChanged += HandleState;
    }

    private void OnDisable()
    {
        if (PongManager.Instance != null)
            PongManager.Instance.OnGameStateChanged -= HandleState;
    }

    private void OnDestroy()
    {
        if (PongManager.Instance != null)
            PongManager.Instance.OnGameStateChanged -= HandleState;
    }

    private void HandleState(GameState state)
    {
        if (state == GameState.Upgrading)
            ShowPanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        PopulateChoices();
    }

    private void PopulateChoices()
    {
        // clear old cards
        foreach (var card in currentCards)
        {
            card.OnClicked -= HandleClick;
            Destroy(card.gameObject);
        }
        currentCards.Clear();

        // pick one upgrade per category, up to numberOfChoices
        UpgradeDataSO[] pickedCards =
            UpgradeManager.Instance.GetUniqueUpgradesByCategory(numberOfChoices);

        // build new cards
        foreach (var upgrade in pickedCards)
        {
            var go = Instantiate(cardPrefab, cardContainer);
            var card = go.GetComponent<UpgradeCard>();
            card.Setup(upgrade);
            card.OnClicked += HandleClick;
            currentCards.Add(card);
        }
    }

    private void HandleClick(UpgradeDataSO pick)
    {
        // play SFX
        Instantiate(upgradeAudioPrefab, transform.position, Quaternion.identity);

        // apply and advance
        UpgradeManager.Instance.ApplyUpgrade(pick);

        // cleanup UI callbacks
        foreach (var card in currentCards)
            card.OnClicked -= HandleClick;
        currentCards.Clear();

        ClosePanel();
        RoomLoader.Instance.LoadNextRoom();
    }
}
