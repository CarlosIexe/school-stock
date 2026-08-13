using System.Collections;

namespace SchoolStock.Models
{
    public class School
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? InepCode { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? PrincipalName { get; set; }

        public int? StudentCount { get; set; }

        public bool Active { get; set; } = true;

        public ICollection<Stock> Stocks { get; set; } = [];
    }
}
