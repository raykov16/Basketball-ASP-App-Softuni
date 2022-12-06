using BasketballAppSoftuni.DTOs.TeamDTOs;

namespace BasketballAppSoftuni.DTOs.MatchDTOs
{
	public class MatchesWithTicketsDTO
	{
		public List<MatchBuyTicketDTO> matchModels { get; set; } 

		public List<TeamShortInfoDTO> teamModels { get; set; }
	}
}
