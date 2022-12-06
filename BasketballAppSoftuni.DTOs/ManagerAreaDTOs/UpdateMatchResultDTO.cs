namespace BasketballAppSoftuni.DTOs.ManagerAreaDTOs
{
    public class UpdateMatchResultDTO
    {
        public int MatchId { get; set; }
        public string MatchDate { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int HomeTeamPoints { get; set;}
        public int AwayTeamPoints { get; set;}
    }
}