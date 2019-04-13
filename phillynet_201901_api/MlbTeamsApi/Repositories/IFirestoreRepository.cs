using System.Collections.Generic;
using MlbTeamsApi.Models;

namespace MlbTeamsApi.Repositories
{
    public interface IFirestoreRepository
    {
        IEnumerable<TeamModel> GetTeams();
        string CreateTeam(TeamModel teamModel);
        void DeleteTeam(string id);
    }
}
