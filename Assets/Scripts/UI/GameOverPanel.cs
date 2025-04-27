using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : RoomPanelBase
{
    [SerializeField] private Button retryButton;

    public override void InitializePanle(LobbyUIManager uiManager)
    {
        base.InitializePanle(uiManager);
        PongManager.Instance.OnGameStateChanged += HandleState;

        retryButton.onClick.AddListener(OnRetry);
    }

    private void OnDisable()
    {
        if (PongManager.Instance != null)
            PongManager.Instance.OnGameStateChanged -= HandleState;
    }

    private void HandleState(GameState state)
    {
        if (state == GameState.GameOver)
            ShowPanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    private void OnRetry()
    {
        // reset stats & health
        PlayerStats.Instance.ResetAll();
        PongManager.Instance.ResetHealth();
        RoomLoader.Instance.RestartFirstRoom(); //StartFirstRoom

        // hide UI
        ClosePanel();
    }

}
