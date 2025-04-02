using System.Globalization;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

// StreamReader sr = new("./one-tweet.jsonl");
StreamReader sr = new("./favorite-tweets.jsonl");

Dictionary<string, List<Tweet>> userTweets = [];
Dictionary<string, int> wordCount = [];
Dictionary<string, double> wordIDF = [];


List<Tweet> ListOfTweets = [];

while (!sr.EndOfStream) {
    Tweet? tweet = JsonSerializer.Deserialize<Tweet>(sr.ReadLine() ?? "");
    if (tweet != null){
        ListOfTweets.Add(tweet);
        if (userTweets.TryGetValue(tweet.UserName, out List<Tweet>? value)) {
            value.Add(tweet);
        } else {
            userTweets.Add(tweet.UserName, new List<Tweet>([tweet]));
        }
        HashSet<string> uniqueWords = [];
        foreach (string word in tweet.Text.Split(" ")) {
            string lowerWord = word.ToLower();
            uniqueWords.Add(word);
            if (!wordCount.TryAdd(lowerWord, 1)) {
                wordCount[lowerWord] += 1;
            }
        }
        foreach (string uniqueWord in uniqueWords) {
            if (!wordIDF.TryAdd(uniqueWord, 1)) {
                wordIDF[uniqueWord] += 1;
            }
        }
    }
}

foreach (var kvp in wordIDF) {
    wordIDF[kvp.Key] = Math.Log10((Double)ListOfTweets.Count/(Double)kvp.Value);
}

void writeXML(List<Tweet> list, string path) {
    XElement tweetsListElement = new XElement("TweetList");
    foreach (Tweet tweet in list) {
        XElement tweetElement = new XElement("Tweet",
            new XElement("Text", tweet.Text),
            new XElement("UserName", tweet.UserName),
            new XElement("LinkToTweet", tweet.LinkToTweet),
            new XElement("FirstLinkUrl", tweet.FirstLinkUrl),
            new XElement("CreatedAt", tweet.CreatedAt),
            new XElement("TweetEmbedCode", tweet.TweetEmbedCode)
        );
        tweetsListElement.Add(tweetElement);
    }
    tweetsListElement.Save(path);
}

List<Tweet>? readXML(string path) {
    try
    {
        XmlSerializer serializer = new XmlSerializer(typeof(TweetList));
        
        using (StreamReader reader = new StreamReader(path))
        {
            object? obj = serializer.Deserialize(reader);
            if (obj != null) return ((TweetList)obj).Tweets.ToList<Tweet>();
            else return null;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Błąd podczas odczytu XML: {ex.Message}");
        return null;
    }
}

List<Tweet> sortByUserName(List<Tweet> list) {
    return [.. list.OrderBy((tweet) => tweet.UserName)];
}
List<Tweet> sortByTweetDate(List<Tweet> list) {
    return [.. list.OrderBy((tweet) => DateTime.ParseExact(tweet.CreatedAt, "MMMM dd, yyyy 'at' hh:mmtt", CultureInfo.InvariantCulture))];
}

writeXML(ListOfTweets, "./one-tweet.xml");
// writeXML(ListOfTweets, "./favorite-tweets.xml");

// List<Tweet>? testList = readXML("./one-tweet.xml");
List<Tweet> testList = readXML("./favorite-tweets.xml")!;

List<Tweet> sortedByUserName = sortByUserName(testList);
// Console.WriteLine(sortedByUserName.First());
// Console.WriteLine(sortedByUserName.Last());

List<Tweet> sortedByTweetDate = sortByTweetDate(testList);
Console.WriteLine("\nNajstarszy Tweet: "+sortedByTweetDate.First());
Console.WriteLine("\nNajnowszy Tweet: "+sortedByTweetDate.Last());

Console.WriteLine("\nOhNoSheTwitnt tweets count: "+userTweets["OhNoSheTwitnt"].Count()); 
Console.WriteLine("\"sorry\" count: "+wordCount["sorry"]); 

Console.WriteLine("\nwords count: "+wordCount.Count());
Console.WriteLine("words >= 5 count: "+wordCount.Where(kvp=>kvp.Key.Length >= 5).Count());

Console.WriteLine("\nWord Count:");
foreach (var kvp in wordCount.Where(kvp=>kvp.Key.Length >= 5).OrderByDescending(kvp => kvp.Value).Take(10)) {
    Console.WriteLine(kvp.Key+": "+kvp.Value);
}

Console.WriteLine("\nWord IDF:");
foreach (var kvp in wordIDF.OrderByDescending((kvp)=>kvp.Value).Take(10)){
    Console.WriteLine(kvp.Key+": "+kvp.Value);
}
