using UnityEngine;

[CreateAssetMenu(fileName = "RoomDataSO", menuName = "Room/RoomDataSO")]
public class RoomDataSO : ScriptableObject
{
    [Header("-------- Room Name --------")]
    [Tooltip("Name of the room")]
    public string roomName;
    //--------------------------------------------------------------------------
    [Header("-------- Room Color Palette --------")]
    [Tooltip("Color palette for the room")]
    public ColorPaletteDataSO colorPalette;
    //--------------------------------------------------------------------------
    [Header("-------- Room Duration --------")]
    [Tooltip("Duration of the room in seconds")]
    public float roomDuration;
    //--------------------------------------------------------------------------
    [Header("-------- Ball Speed --------")]
    [Tooltip("Initial speed of the ball")]
    public float ballSpeed;
    //--------------------------------------------------------------------------
    [Header("-------- Ball Speed Increase --------")]
    [Tooltip("Speed increase of the ball on collision")]
    public float ballSpeedIncrease;
    //--------------------------------------------------------------------------
    [Header("-------- Max Ball Speed --------")]
    [Tooltip("Maximum speed of the ball")]
    public float maxBallSpeed;
    //--------------------------------------------------------------------------
    [Header("-------- Ball Count --------")]
    [Tooltip("Number of balls in the room")]
    public int ballCount;
}
