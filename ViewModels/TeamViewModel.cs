namespace Tazkara.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string TeamLogo { get; set; } = null!;
        public int MatchId { get; set; }
    }
}
