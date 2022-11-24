using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Models.TicketViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BasketballAppSoftuni.Controllers
{
	[Authorize]
	public class TicketController : Controller
	{
		private readonly ITicketService _ticketService;
		public TicketController(ITicketService ticketService)
		{
			_ticketService = ticketService;
		}

		[HttpGet]
		public async Task<IActionResult> BuyTickets(int matchId)
		{
			var ticketModel = await _ticketService.CreateTicketAsync(matchId);

			return View(ticketModel);
		}

		[HttpPost]
		public async Task<IActionResult> BuyTickets(TicketViewModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			await _ticketService.BuyTicketsAsync(userId, model.MatchId, model.Quantity);

            return RedirectToAction("MyMatches", "Match");
		}
	}
}
