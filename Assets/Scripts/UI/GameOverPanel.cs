using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : RoomPanelBase
{
    [SerializeField] private Button retryButton;

    public override void InitializePanle(LobbyUIManager uiManager)
    {
        base.InitializePanle(uiManager);
        SceneManager.sceneLoaded += OnSceneLoaded;

        retryButton.onClick.AddListener(OnRetry);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PongManager.Instance.OnGameStateChanged += HandleState;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
