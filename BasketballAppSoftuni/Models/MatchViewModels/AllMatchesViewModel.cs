using BasketballAppSoftuni.Models.TeamsModels;

namespace BasketballAppSoftuni.Models.MatchViewModels
{
	public class AllMatchesViewModel
	{
		public IEnumerable<MatchTableViewModel> matchModels { get; set; }
		public IEnumerable<TeamShortInfoViewModel> teamModels { get; set; } 
	}
}
