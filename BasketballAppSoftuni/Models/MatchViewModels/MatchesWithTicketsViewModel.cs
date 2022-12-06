using BasketballAppSoftuni.Models.TeamsModels;

namespace BasketballAppSoftuni.Models.MatchViewModels
{
	public class MatchesWithTicketsViewModel
	{
		public IEnumerable<MatchBuyTicketViewModel> matchModels { get; set; } 

		public IEnumerable<TeamShortInfoViewModel> teamModels { get; set; }
	}
}
