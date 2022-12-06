namespace BasketballAppSoftuni.DTOs.MatchDTOs
{
    public class MatchTableDTO
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeamLogo { get; set;}
        public string HomeTeamName { get; set;}
        public string AwayTeamLogo { get; set;}
        public string AwayTeamName { get; set;}
        public int? HomeTeamPoints { get; set; }
        public int? AwayTeamPoints { get; set; }
    }
}
