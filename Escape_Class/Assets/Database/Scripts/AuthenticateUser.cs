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

    public async Task<ReturnAuthenticate> Authenticate(string username, string password)
    {
        database = client.GetDatabase("escape_class");
        collection = database.GetCollection<BsonDocument>("users");

        var filterBuilder = Builders<BsonDocument>.Filter;
        var filter = filterBuilder.Eq("username", username) & filterBuilder.Eq("password", password);

        var authenticateTask = collection.FindAsync(filter);

        var results = await collection.Find(filter).ToListAsync();
        Debug.Log("Result COUNT: " + results.Count);

        var toReturn = new ReturnAuthenticate();

        if (results.Count == 0)
        {
            toReturn.found = false;
            toReturn.role = null; 
            return toReturn;
        }
        else
        toReturn.found = true;
        toReturn.id = results[0].GetValue("id").ToInt32();
        Debug.Log(results[0].GetValue("categoryId"));
        if (results[0].GetValue("categoryId").ToString() == "60c5d4ce8f159c9c8587e5d0")
        {
            toReturn.role = "Teacher";
            Debug.Log("Role: " + toReturn.role);
            return toReturn;
        }
        else
        {
            toReturn.role = "Student";
            Debug.Log("Role: " + toReturn.role);
            return toReturn;
        }

    }

    // The return type of Authenticate
    public class ReturnAuthenticate
    {
        //public string id { get; set; }
        public bool found { get; set; }
        public string role { get; set; }
        public int id { get; set; }

    }

}