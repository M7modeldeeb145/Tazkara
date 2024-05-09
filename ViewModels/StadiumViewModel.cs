using Tazaker.Models;

namespace Tazkara.ViewModels
{
    public class StadiumViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public string Address { get; set; } = null!;
        public StadiumStatus StadiumStatus { get; set; }
        public int TeamId { get; set; }
    }
}
