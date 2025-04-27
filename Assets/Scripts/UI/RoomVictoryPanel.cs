using UnityEngine;
using UnityEngine.UI;

public class RoomVictoryPanel : RoomPanelBase
{
    [SerializeField] private Button restartButton;


    public override void InitializePanle(LobbyUIManager lobbyUIManager)
    {
        base.InitializePanle(lobbyUIManager);

        restartButton.onClick.AddListener(OnRestart);

    }

    private void OnRestart()
    {
        // reset stats & health
        PlayerStats.Instance.ResetAll();
        PongManager.Instance.ResetHealth();
        RoomLoader.Instance.RestartFirstRoom(); //StartFirstRoom
        ClosePanel();
    }
}
