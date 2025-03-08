using ChessinatorDomain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ChessinatorInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private record CountByTimeControlResponseItem(string Type, int Count);
        private record CountByResultResponseItem(string Result, int Count);

        private readonly ChessdbContext chessContext;

        public ChartsController(ChessdbContext chessContext)
        {
            this.chessContext = chessContext;
        }

        [HttpGet("countByType")]
        public async Task<JsonResult> GetCountByTimeControlAsync(CancellationToken cancellationToken)
        {
            var responseItems = await chessContext
                .Tournaments
                .GroupBy(tour => tour.TimeControl.Type.Name) 
                .Select(group => new CountByTimeControlResponseItem(
                    group.Key.ToString(), 
                    group.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }

        [HttpGet("countByResult")]
        public async Task<JsonResult> GetCountByResultAsync(CancellationToken cancellationToken)
        {
            var responseItems = await chessContext
                .ChessMatches
                .GroupBy(match => match.MatchResult.Description) 
                .Select(group => new CountByResultResponseItem(
                    group.Key.ToString(), 
                    group.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }

    }

}
