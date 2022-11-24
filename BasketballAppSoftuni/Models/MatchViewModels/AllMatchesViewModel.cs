using BasketballAppSoftuni.Models.TeamsModels;

namespace BasketballAppSoftuni.Models.MatchViewModels
{
	public class AllMatchesViewModel
	{
		public List<MatchTableViewModel> matchModels { get; set; }
		public List<TeamShortInfoViewModel> teamModels { get; set; } 
	}
}
