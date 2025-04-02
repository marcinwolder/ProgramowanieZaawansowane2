using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

// StreamReader sr = new("./one-tweet.jsonl");
StreamReader sr = new("./favorite-tweets.jsonl");

List<Tweet> ListOfTweets = [];

while (!sr.EndOfStream) {
    Tweet? tweet = JsonSerializer.Deserialize<Tweet>(sr.ReadLine() ?? "");
    if (tweet != null){
        ListOfTweets.Add(tweet);
    }
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

List<Tweet> readXML(string path) {
    List<Tweet> listOfTweets = [];

    XmlDocument doc = new();
    doc.Load(path);

    foreach (XmlNode node in doc.DocumentElement!.ChildNodes){
        Tweet tweet = new(
            node.Attributes["Text"].InnerText,
            node.Attributes["UserName"].InnerText,
            node.Attributes["LinkToTweet"].InnerText,
            node.Attributes["FirstLinkUrl"].InnerText,
            node.Attributes["CreatedAt"].InnerText,
            node.Attributes["TweetEmbedCode"].InnerText
        );
        listOfTweets.Add(tweet);
    }

    return listOfTweets;
}

// writeXML(ListOfTweets, "./one-tweet.xml");
writeXML(ListOfTweets, "./favorite-tweets.xml");