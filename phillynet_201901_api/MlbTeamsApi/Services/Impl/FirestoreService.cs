using System.Collections.Generic;
using MlbTeamsApi.Models;
using MlbTeamsApi.Repositories;

namespace MlbTeamsApi.Services.Impl
{
    public class FirestoreService : IFirestoreService
    {
        private readonly IFirestoreRepository _firestoreRepository;

        public FirestoreService(IFirestoreRepository firestoreRepository)
        {
            _firestoreRepository = firestoreRepository;
        }

        public IEnumerable<TeamModel> GetTeams()
        {
            return _firestoreRepository.GetTeams();
        }

        public string CreateTeam(TeamModel teamModel)
        {
            return _firestoreRepository.CreateTeam(teamModel);
        }

        public void DeleteTeam(string id)
        {
            _firestoreRepository.DeleteTeam(id);
        }
    }
}