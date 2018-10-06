using System.Collections.Generic;

namespace ShopService.Common.Infrastructure
{
    public class EnumContainer
    {
        public IDictionary<int, string> Data { get; set; }
        public bool IsMask { get; set; }

        public EnumContainer(IDictionary<int, string> data, bool isMask = false)
        {
            Data = data;
            IsMask = isMask;
        }
    }
}
