using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.BoughtM
{
    public class BoughtAddModel
    {
        public DateTime? timeBought { get; set; }
        public Guid userId { get; set; }
    }
}
