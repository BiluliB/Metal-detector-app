using System.Diagnostics.CodeAnalysis;

namespace Magnetify.Data
{
    public class HistoryItem
    {
        [AllowNull, NotNull]
        public string Text { get; set; }

        public double Opacity { get; set; }
    }
}
