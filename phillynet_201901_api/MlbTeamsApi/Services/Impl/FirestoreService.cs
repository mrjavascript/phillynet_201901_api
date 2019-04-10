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
    }
}