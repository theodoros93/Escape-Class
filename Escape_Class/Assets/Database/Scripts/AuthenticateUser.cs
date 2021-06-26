using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class AuthenticateUser : DatabaseAccess
{

    // Start is called before the first frame update
    private void Start()
    {
 
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public async Task<bool> Authenticate(string username, string password)
    {
        database = client.GetDatabase("escape_class");
        collection = database.GetCollection<BsonDocument>("users");

        var filterBuilder = Builders<BsonDocument>.Filter;
        var filter = filterBuilder.Eq("username", username) & filterBuilder.Eq("password", password);

        var authenticateTask = collection.FindAsync(filter);

        var results = await collection.Find(filter).ToListAsync();
        Debug.Log("Result COUNT: " + results.Count);
        Debug.Log("Results: " + results);
        if (results.Count == 0)
        {
            return false;
        }
        else return true;
        //var authenticateAwaited = await authenticateTask;


        //var authenticatedString = authenticateAwaited.ToString();
        // Debug.Log("String ="+authenticatedString);
        //bool isEmpty = !authenticatedString.;
        //IsBsonNull
        //if (!authenticateAwaited)
       // {
            //return true;
       // }
      //  else return false;
        

    }

}