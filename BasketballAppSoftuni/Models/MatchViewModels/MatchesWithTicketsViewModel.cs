using BasketballAppSoftuni.Models.TeamsModels;

namespace BasketballAppSoftuni.Models.MatchViewModels
{
	public class MatchesWithTicketsViewModel
	{
		public List<MatchBuyTicketViewModel> matchModels { get; set; } 

		public List<TeamShortInfoViewModel> teamModels { get; set; }
	}
}
