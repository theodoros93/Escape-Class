using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DatabaseAccess : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://dbUser:ZhdhD1Q5Nyf6sd57@seriouscluster0.oygtk.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    // Start is called before the first frame update
    void Start()
    {
        //var client = new MongoClient("mongodb+srv://dbUser:ZhdhD1Q5Nyf6sd57@seriouscluster0.oygtk.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
        database = client.GetDatabase("escape_class");
        collection = database.GetCollection<BsonDocument>("users");

        String myString = "hello";
        String myString2 = "hello 2";
        var document = new BsonDocument
{
        { "name", myString },
        { "type", myString2 },
        { "count", 1 },
        { "info", new BsonDocument
            {
                { "x", 203 },
                { "y", 102 }
            }}
        };


        var filter = Builders<BsonDocument>.Filter.Gt("count", 50);
        var cursor = collection.Find(filter).ToCursor();
        foreach (var document1 in cursor.ToEnumerable())
        {
            
            Debug.Log(document1);
            //var jss = new JavaScriptSerializer();
            //dynamic d = jss.DeserializeObject((string)document1);
            //Console.WriteLine(d[0]["name"]);
           // Debug.Log(d[0]["name"]);
        }
        //collection.InsertOne(document);
        // await collection.InsertOneAsync(document);
        // GetScoresFromDatabase();


    }



    public async void SaveScoreToDatabase(string userName, int score)
    {
        var document = new BsonDocument { { userName, score } };
    }

    public async Task<List<HighScore>> GetScoresFromDatabase()
    {
        var allScoresTask = collection.FindAsync(new BsonDocument());
        var scoresAwaited = await allScoresTask;

        List<HighScore> highscores = new List<HighScore>();
        foreach (var score in scoresAwaited.ToList())
        {
            highscores.Add(Deserialize(score.ToString()));
        }
        return highscores;
    }

    // under construction
    private HighScore Deserialize(string rawJson)
    {
        var highScore = new HighScore();
        return highScore;
    }

    // inline class
    public class HighScore
    {
        public string UserName { get; set; }
        public int Score { get; set; }
    }
}