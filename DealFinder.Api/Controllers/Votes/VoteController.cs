using DealFinder.Data.Votes.Repository;
using DealFinder.Data.Votes.Service;
using Microsoft.AspNetCore.Mvc;

namespace DealFinder.Api.Controllers.Votes
{
    [Route("api/[controller]")]
    public class VoteController : Controller
    {
        private readonly VoteService _voteService;

        public VoteController()
        {
            _voteService = new VoteService(new VoteRepository());
        }

        [HttpPost("cast")]
        public ActionResult Post([FromBody]CastVoteDetailsRequest request)
        {
            return Ok(_voteService.CastVote(request));
        }
    }
}
