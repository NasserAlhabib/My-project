using UnityEngine;

[CreateAssetMenu(fileName = "RoomColorPaletteDataSO", menuName = "RoomColorPalette/ColorPalette")]
public class ColorPaletteDataSO : ScriptableObject
{
    [Header("World Colors")]
    public Color backgroundColor = Color.black;
    public Color paddleColor = Color.white;
    public Color ballColor = Color.white;
    public Color wallColor = Color.white;
    public Color obstaclesColor = Color.white;
}
