using BasketballAppSoftuni.DTOs.TeamDTOs;

namespace BasketballAppSoftuni.DTOs.MatchDTOs
{
	public class AllMatchesDTO
	{
		public List<MatchTableDTO> matchModels { get; set; }
		public List<TeamShortInfoDTO> teamModels { get; set; } 
	}
}
