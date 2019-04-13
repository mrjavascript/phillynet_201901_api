using System.Collections.Generic;
using MlbTeamsApi.Models;

namespace MlbTeamsApi.Services
{
    public interface IFirestoreService
    {
        IEnumerable<TeamModel> GetTeams();
        string CreateTeam(TeamModel teamModel);
        void DeleteTeam(string id);
    }
}