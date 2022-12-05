using BasketballAppSoftuni.Areas.Manager.Models;
using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Models.MatchViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAppSoftuni.Areas.Manager.Controllers
{
    public class MatchController : ManagerBaseController
    {
        private readonly IManagerService _managerService;
        public MatchController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        public async Task<IActionResult> Schedule()
        {
            var model = new ScheduleMatchModel()
            {
                AllTeams = await _managerService.GetAllTeamNamesAsync(),
                MatchDate = DateTime.Now.AddDays(1)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Schedule(ScheduleMatchModel model)
        {
            model.AllTeams = await _managerService.GetAllTeamNamesAsync();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool invalidTeams = AreTeamsValid(model);
            bool invalidDate = IsDateValid(model);

            if (invalidDate || invalidTeams)
            {
                return View(model);
            }
            else
            {
                await _managerService.AddMatchAsync(model);
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Reschedule()
        {
            var models = await _managerService.GetUnplayedMatchesAsync();
            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> Reschedule(RescheduleMatchModel model)
        {
            if (model.MatchDate < DateTime.Now)
            {
                ModelState.AddModelError("", "Enter a date that is not earlier than today!");
            }
            else
            {
                await _managerService.RescheduleMatchAsync(model.MatchId, model.MatchDate);
            }

            var models = await _managerService.GetUnplayedMatchesAsync();
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateResult()
        {
            var models = await _managerService.GetMatchesForUpdateAsync();
            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateResult(UpdateMatchResultModel model)
        {
            await _managerService.UpdateMatchScoreAsync(model.MatchId, model.HomeTeamPoints, model.AwayTeamPoints);

            var models = await _managerService.GetMatchesForUpdateAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveMatch()
        {
            var models = await _managerService.GetUnplayedMatchesAsync();
            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMatch(int matchId)
        {
            await _managerService.RemoveMatch(matchId);

            var models = await _managerService.GetUnplayedMatchesAsync();
            return View(models);
        }

        private bool AreTeamsValid(ScheduleMatchModel model)
        {
            bool invalidTeam = false;

            if (model.HomeTeamId == model.AwayTeamId)
            {
                invalidTeam = true;
                ModelState.AddModelError("AwayTeamId", "Home team and away team can not be the same!");
            }

            return invalidTeam;
        }

        private bool IsDateValid(ScheduleMatchModel model)
        {
            double[] time = model.MatchTime.Split(":").Select(double.Parse).ToArray();
            model.MatchDate = model.MatchDate.AddHours(time[0]);
            model.MatchDate = model.MatchDate.AddMinutes(time[1]);

            bool invalidDate = false;
            if (model.MatchDate < DateTime.Now)
            {
                invalidDate = true;
                ModelState.AddModelError("MatchDate", "Match date should not be earlier than today!");
            }

            return invalidDate;
        }
    }
}
