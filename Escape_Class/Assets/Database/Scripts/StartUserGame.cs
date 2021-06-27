using System;
using MongoDB.Bson;
using MongoDB.Driver;

public class StartUserGame : DatabaseAccess
{
    public string pointsTxt = "0";
    public int points = 0;
    

    // Start is called before the first frame update
    private void Start()
    {
        StartGame();
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
}