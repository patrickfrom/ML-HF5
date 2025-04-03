using Avalonia.Media;

namespace ReviewChecker;

public static class BrushUtil
{
    private static Color GetInterpolatedColor(float value)
    {
        Color color1 = Colors.IndianRed;
        Color color2 = Colors.MediumSeaGreen;

        byte r = (byte)(color1.R + (color2.R - color1.R) * value);
        byte g = (byte)(color1.G + (color2.G - color1.G) * value);
        byte b = (byte)(color1.B + (color2.B - color1.B) * value);

        return Color.FromRgb(r, g, b);
    }

    public static Brush GetInterpolatedBrush(float value)
    {
        Color interpolatedColor = GetInterpolatedColor(value);
        return new SolidColorBrush(interpolatedColor);
    }
}