using System;
using UnityEngine;

public static class PaletteManager
{
    public static event Action<ColorPaletteDataSO> OnPaletteChanged;

    private static ColorPaletteDataSO _current;
    public static ColorPaletteDataSO Current
    {
        get => _current;
        set
        {
            _current = value;
            OnPaletteChanged?.Invoke(_current);
        }
    }
}
