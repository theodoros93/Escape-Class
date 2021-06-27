using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DatabaseAccess : MonoBehaviour
{
    public MongoClient client = new MongoClient("mongodb+srv://dbUser:ZhdhD1Q5Nyf6sd57@seriouscluster0.oygtk.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
    public IMongoDatabase database;
    public IMongoCollection<BsonDocument> collection;

    // Start is called before the first frame update
    private void Start()
    {
        var newgame = new StartUserGame();
        newgame.StartGame();

        var authenticateNow = new AuthenticateUser();
        authenticateNow.Authenticate("emaznis", "emaznis");

        var miniGames = new MiniGames();
        miniGames.GetMiniGames(new ObjectId("60c5e98c8f159c9c8587e5dc"), 1, "easy");

        
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

    private void CreateUser()
    {
        database = client.GetDatabase("escape_class");
        collection = database.GetCollection<BsonDocument>("users");

        String name = "Thrasivoulos";
        String lastname = "Tsiatsos";
        String username = "ttsiatsos";
        String password = "ttsiatsos";
        ObjectId classId = new ObjectId("60c5d45d8f159c9c8587e5ce") ;
        ObjectId categoryId = new ObjectId("60c5d4ce8f159c9c8587e5d0") ;

        var document = new BsonDocument
        {
            { "name", name },
            { "lastname", lastname },
            { "username", username },
            { "password", password},
            { "classId",classId },
            { "categoryId",categoryId }
        };
        collection.InsertOne(document);
    }

    // inline class
    public class HighScore
    {
        public string UserName { get; set; }
        public int Score { get; set; }
    }
}