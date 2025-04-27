using UnityEngine;
using UnityEngine.UI;

public class PaletteApplier : MonoBehaviour
{

    [Tooltip("Which color from the palette to apply here")]
    [SerializeField] private ColorTarget targetColor;

    // cache components you might recolor:
    private SpriteRenderer targetSpriteRenderer;
    // cach camera component if you want to recolor the background:
    private Camera targetCamera;

    private void Awake()
    {
        targetSpriteRenderer = GetComponent<SpriteRenderer>();

        if (targetSpriteRenderer == null)
            targetSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        targetCamera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        PaletteManager.OnPaletteChanged += ApplyPalette;
        // also apply immediately if we already have a palette
        if (PaletteManager.Current != null)
            ApplyPalette(PaletteManager.Current);
    }

    private void OnDisable()
    {
        PaletteManager.OnPaletteChanged -= ApplyPalette;
    }

    private void ApplyPalette(ColorPaletteDataSO colorPalette)
    {
        Color color = Color.white;
        switch (targetColor)
        {
            case ColorTarget.Background: color = colorPalette.backgroundColor; break;
            case ColorTarget.Paddle: color = colorPalette.paddleColor; break;
            case ColorTarget.Ball: color = colorPalette.ballColor; break;
            case ColorTarget.Wall: color = colorPalette.wallColor; break;
            case ColorTarget.Obstacles: color = colorPalette.obstaclesColor; break;
            default: color = Color.white; break;
        }

        if (targetSpriteRenderer != null) targetSpriteRenderer.color = color;
        if (targetCamera != null) targetCamera.backgroundColor = color;
    }
}
