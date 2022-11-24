namespace BasketballAppSoftuni.Models.MatchViewModels
{
    public class MyMatchesViewModel
    {
        public string HomeTeamLogo { get; set; }
        public string HomeTeamName { get; set; }
        public int? HomeTeamPoints { get; set; }
        public string AwayTeamLogo { get; set; }
        public string AwayTeamName { get; set; }
        public int? AwayTeamPoints { get; set; }
        public string ArenaName { get; set; }
        public string ArenaLocation { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
