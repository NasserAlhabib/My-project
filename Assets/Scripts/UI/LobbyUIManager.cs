using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUIManager : MonoBehaviour
{
    [Header("---------------------- Lobby UI Manager Settings ----------------------")]
    [SerializeField] private RoomPanelBase[] roomPanels;

    private void OnEnable()
    {
        PongManager.Instance.OnGameStateChanged += HandleGameStateChanged;
        SceneManager.sceneLoaded += InitializePanelsForCurrentScene;
    }

    private void OnDisable()
    {
        if (PongManager.Instance != null)
            PongManager.Instance.OnGameStateChanged -= HandleGameStateChanged;

        SceneManager.sceneLoaded -= InitializePanelsForCurrentScene;
    }

    private void OnDestroy()
    {
        if (PongManager.Instance != null)
            PongManager.Instance.OnGameStateChanged -= HandleGameStateChanged;

        SceneManager.sceneLoaded -= InitializePanelsForCurrentScene;
    }

    private void InitializePanelsForCurrentScene(Scene arg0, LoadSceneMode arg1)
    {
        foreach (var panel in roomPanels)
            panel.InitializePanle(this);
    }

    private void HandleGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Room:
                CloseAllPanels();
                ShowPanel(RoomPanelType.Room);
                break;
            case GameState.Upgrading:
                CloseAllPanels();
                ShowPanel(RoomPanelType.Upgrade);
                break;
            case GameState.Victory:
                CloseAllPanels();
                ShowPanel(RoomPanelType.Victory);
                break;
            case GameState.GameOver:
                CloseAllPanels();
                ShowPanel(RoomPanelType.GameOver);
                break;
        }
    }

    public void ShowPanel(RoomPanelType panelType)
    {
        foreach (RoomPanelBase panel in roomPanels)
        {
            if (panel.PanelType == panelType)
            {
                panel.ShowPanel();
                break;
            }
        }
    }

    private void CloseAllPanels()
    {
        foreach (RoomPanelBase panel in roomPanels)
        {
            panel.gameObject.SetActive(false);
        }
    }


}
