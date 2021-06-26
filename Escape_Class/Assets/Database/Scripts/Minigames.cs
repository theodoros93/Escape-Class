using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class MiniGames : DatabaseAccess
{

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
    }

    public async Task<List<MiniGame>> GetMiniGames(ObjectId subjectId, string level)
    {
        database = client.GetDatabase("escape_class");
        collection = database.GetCollection<BsonDocument>("minigame");

        var filterBuilder = Builders<BsonDocument>.Filter;
        var filter = filterBuilder.Eq("lectureId", subjectId) & filterBuilder.Eq("level", level);

        var miniGamesTask = collection.FindAsync(filter);
        var miniGamesAwaited = await miniGamesTask;
        Debug.Log("all mini games" + miniGamesAwaited);
        List<MiniGame> miniGames = new List<MiniGame>();
        foreach(var miniGame in miniGamesAwaited.ToList()){
            miniGames.Add(Deserialize(miniGame.ToString()));
            

        }
        Debug.Log("There are " + miniGames.Count + " elements in my list");
        return miniGames;
    }

    // string manipulation function
    private MiniGame Deserialize(string rawJson)
    // raw json format: "{ \"_id\" : ObjectId(\"60d72a7a32c6b13d4db993b8\"), \"lectureId\" : ObjectId(\"60c5e98c8f159c9c8587e5dc\"), \"question\" : \"Για να κινηθεί ένα αυτοκίνητο, έχουμε μετατροπή ενέργειας...\", \"a\" : \"από κινητική σε χημική\", \"b\" : \"από χημική σε κινητική\", \"c\" : \"από θερμική/θερμότητα σε κινητική\", \"d\" : \"από χημική σε θερμική/θερμότητα\", \"answer\" : \"b\", \"level\" : \"easy\" }"
    {
        Debug.Log("the raw json: "+rawJson);
        var miniGame = new MiniGame();
        Debug.Log("TEST 1");
        dynamic data= JObject.Parse(rawJson);
        Debug.Log("TEST 2");
        miniGame.level = data.level;
        miniGame.question = data.question;
        Debug.Log("QUESTION: " + miniGame.question);
        Debug.Log("QUESTION: " + data.question);
        miniGame.a = data.a;
        miniGame.b = data.b;
        miniGame.c = data.c;
        miniGame.d = data.d;
        miniGame.answer = data.answer;

        //var dyn = JsonConvert.DeserializeObject<dynamic>(rawJson);
        //miniGame.question = dyn.task.question.Value;
        //Debug.Log("QUESTION: " + miniGame.question);

        //var stringWithoutID = rawJson.Substring(rawJson.IndexOf(")," + 4));
        //var lectureId = stringWithoutID.Substring(0, stringWithoutID.IndexOf(":") - 2);
        return miniGame;
    }


    // inline class
    public class MiniGame
    {
        //public string id { get; set; }
        public string question { get; set; }
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public string answer { get; set; }
        public string level { get; set; }

    }

}