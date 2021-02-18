using System.Collections.Generic;

namespace ISUCorp.Test.Api.Data.Mapping.Helpers
{
    public class PagerBase<T>
    {
        public List<T> Items { get; set; }
        public int Count { get; set; }
    }
}
