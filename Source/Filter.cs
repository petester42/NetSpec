using System.Collections.Generic;

namespace NetSpec
{
    public sealed class FilterFlags : Dictionary<string, bool>
    {

    }

    public sealed class Filter
    {
        public static string focused
        {
            get { return "focused"; }
        }

        public static string pending
        {
            get { return "pending"; }
        }
    }
}