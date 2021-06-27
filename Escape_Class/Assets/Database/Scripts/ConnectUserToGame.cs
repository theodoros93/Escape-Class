using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class ConnectUserToGame : DatabaseAccess
{

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void StartGame()
    {
        //*****find a way to get userid*****

        database = client.GetDatabase("escape_class");
        collection = database.GetCollection<BsonDocument>("connection_users_class");

        var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

        var filter = Builders<BsonDocument>.Filter.Eq("userid", 1);
        var update = Builders<BsonDocument>.Update.Set("startedtime", Timestamp);
        var myresult = collection.UpdateOne(filter, update);
    }

    public async Task<long> SaveFinishedGame(int studentId, ObjectId gameId, int score, int time, int wrongAnswers)
    {

        database = client.GetDatabase("escape_class");
        collection = database.GetCollection<BsonDocument>("connection_users_class");

        //var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

        var filterBuilder = Builders<BsonDocument>.Filter;
        var filter = filterBuilder.Eq("userid", studentId) & filterBuilder.Eq("classid", gameId);

        var update = Builders<BsonDocument>.Update.Set("score", score)
                                                  .Set("time", time)
                                                  .Set("wrong_answers", wrongAnswers)
                                                  .Set("played", 1);
        var result = await collection.UpdateOneAsync(filter, update);
        if (result.IsModifiedCountAvailable)
        {
            Debug.Log("UPDATE ASYNC" + result.ModifiedCount);
            return result.ModifiedCount;
        }
        else return 0;
    }

}

