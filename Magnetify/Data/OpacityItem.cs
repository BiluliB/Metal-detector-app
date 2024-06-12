using System.Diagnostics.CodeAnalysis;

namespace Magnetify.Data
{
    public class OpacityItem
    {
        [AllowNull, NotNull]
        public string Text { get; set; }

        public double Opacity { get; set; }
    }
}
