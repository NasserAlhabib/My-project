using System.Collections;
using UnityEngine;

public class RoomConfig : MonoBehaviour
{
    [Header("Room Config")]
    [SerializeField] private RoomDataSO roomData;

    private void Awake()
    {
        // 1) Tell the palette system which palette to use:
        PaletteManager.Current = roomData.colorPalette;

        // 2) Now your PaletteApplier components will recolor everything.

        IEnumerator roomSequence = RoomLoader.Instance.FadeOutThen(StartRoomFlow);
        StartCoroutine(roomSequence);
    }
    private void StartRoomFlow()
    {
        PongManager.Instance.SetCurrentRoomData(roomData);
        PongManager.Instance.StartRoom(roomData);
        PongManager.Instance.SetGameState(GameState.Room);
    }
}
