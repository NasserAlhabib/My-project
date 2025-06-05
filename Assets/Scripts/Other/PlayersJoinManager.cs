using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersJoinManager : MonoBehaviour
{
    [SerializeField] private RectTransform cardContainer;

    public void OnPlayerJoin(PlayerInput input)
    {
        input.transform.SetParent(cardContainer);
    }
}
