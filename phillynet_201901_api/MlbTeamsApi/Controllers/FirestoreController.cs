using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        public string[] Get()
//        public async Task<ActionResult<List<string>>> Get()
        {
            return new string[] {"value1", "value2"};
//            var values = await _valuesService.GetValues();
//            return values.ToList();
        }
    }
}