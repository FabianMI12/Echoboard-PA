using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Game/Color Palette")]
public class ColorPalette : ScriptableObject
{
    public Color primary;
    public Color secondary;
    public Color accent;
    public Color background;
    public Color text;
}