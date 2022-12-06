namespace BasketballAppSoftuni.DTOs.ManagerAreaDTOs
{
	public class RescheduleMatchDTO
	{
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public string HomeTeamLogo { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamLogo { get; set; }
        public string AwayTeamName { get; set; }
    }
}
