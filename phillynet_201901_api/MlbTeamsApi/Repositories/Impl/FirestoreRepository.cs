using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using MlbTeamsApi.Models;
using Serilog;

namespace MlbTeamsApi.Repositories.Impl
{
    public class FirestoreRepository : IFirestoreRepository
    {
        private const string PROJECT_ID = "phillynet-mlbteams";
        private readonly FirestoreDb _db;

        public FirestoreRepository()
        {
            var credential = GoogleCredential
                .FromFile(Path.Combine(AppContext.BaseDirectory + "/google.json"));
            var channelCredentials = credential.ToChannelCredentials();
            var channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            var firestoreClient = FirestoreClient.Create(channel);
            _db = FirestoreDb.Create(PROJECT_ID, client: firestoreClient);
            Log.Logger.Information("Created Firestore connection");
        }

        public IEnumerable<TeamModel> GetTeams()
        {
            var usersRef = _db.Collection("teams");
            var snapshot = usersRef.GetSnapshotAsync().Result;

            return snapshot.Documents
                .Select(document =>
                {
                    ;
                    var dictionary = document.ToDictionary();
                    return new TeamModel
                    {
                        Id = document.Id,
                        Team = dictionary["team"].ToString(),
                        City = dictionary["city"].ToString(),
                        Foreground = dictionary["foreground"].ToString(),
                        Background = dictionary["background"].ToString()
                    };
                })
                .ToList();
        }

        public string CreateTeam(TeamModel value)
        {
            var docRef = _db.Collection("teams").Document();
            var team = new Dictionary<string, object>
            {
                {"city", value.City},
                {"team", value.Team},
                {"foreground", value.Foreground},
                {"background", value.Background}
            };
            var result = docRef.SetAsync(team).Result;
            return docRef.Id;
        }

        public void DeleteTeam(string id)
        {
            var docRef = _db.Collection("teams").Document(id);
            var result = docRef.DeleteAsync().Result;
        }
    }
}