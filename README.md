# phillynet_201901_api
Using .NET Core to connect to firestore API

(1) Create Service Credentials for your Firestore database here:

https://console.cloud.google.com/projectselector2/apis/credentials 

(2) In `google.json`, copy your credentials you downloaded from above

(3) In `Repositories/Impl/FirestoreRepository.cs`, change:

`private const string PROJECT_ID = "miketest-409fc";`

To reflect your Firestore database.

Change the queries which do the following:

`var usersRef = _db.Collection("teams");`

to use your own collection.
