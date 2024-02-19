namespace TM.Desktop
{
    public static class TMColor
    {
        public static string ColorToHexDecimal(this System.Drawing.Color color, string prefix = "#")
        {
            return $"{prefix}{color.R:X2}{color.G:X2}{color.B:X2}";
        }
        public static System.Drawing.Color HexDecimalToColor(this string hexDecimal)
        {
            return System.Drawing.ColorTranslator.FromHtml(hexDecimal);
        }
    }
}
