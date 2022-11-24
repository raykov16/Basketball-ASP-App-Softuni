namespace BasketballAppSoftuni.Models.MatchViewModels
{
    public class MatchBuyTicketViewModel
    {
        public int MatchId { get; set; }
        public int ArenaId { get; set; }
        public string ArenaName { get; set;}
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeamLogo { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamLogo { get; set; }
        public string AwayTeamName { get; set; }
    }
}
