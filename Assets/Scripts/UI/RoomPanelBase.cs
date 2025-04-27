using UnityEngine;

public class RoomPanelBase : MonoBehaviour
{
    [field: Header("---------------------- Base Panel Settings ----------------------")]
    [field: SerializeField] public RoomPanelType PanelType { private set; get; } = RoomPanelType.None;

    protected LobbyUIManager lobbyUIManager;
    protected AudioSource audioSource;

    public virtual void InitializePanle(LobbyUIManager lobbyUIManager)
    {
        this.lobbyUIManager = lobbyUIManager;
    }

    public virtual void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    protected void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
