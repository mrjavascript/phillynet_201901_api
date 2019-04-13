using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MlbTeamsApi.Models;
using MlbTeamsApi.Services;

namespace MlbTeamsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirestoreController : ControllerBase
    {
        private readonly IFirestoreService _firestoreService;

        public FirestoreController(IFirestoreService firestoreService)
        {
            _firestoreService = firestoreService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public ActionResult<List<TeamModel>> Get()
        {
            var teams = _firestoreService.GetTeams();
            return teams.ToList();
        }

        [HttpPost]
        public string Post([FromBody] TeamModel teamModel)
        {
            return _firestoreService.CreateTeam(teamModel);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _firestoreService.DeleteTeam(id);
        }
    }
}