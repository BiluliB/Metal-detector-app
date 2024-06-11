using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnetify.Data
{
    public class RecentItem
    {
        [AllowNull, NotNull]
        public string Name { get; set; }

        [AllowNull, NotNull]
        public string Description { get; set; }
    }
}
