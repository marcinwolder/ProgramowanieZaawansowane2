using System.Xml.Serialization;

[XmlRoot("TweetList")]
public class TweetList
{
    [XmlElement("Tweet")]
    public required Tweet[] Tweets { get; set; }
}

public class Tweet
{
    [XmlElement("Text")]
    public required string Text { get; set; }
    
    [XmlElement("UserName")]
    public required string UserName { get; set; }
    
    [XmlElement("LinkToTweet")]
    public required string LinkToTweet { get; set; }
    
    [XmlElement("FirstLinkUrl")]
    public required string FirstLinkUrl { get; set; }
    
    [XmlElement("CreatedAt")]
    public required string CreatedAt { get; set; }
    
    [XmlElement("TweetEmbedCode")]
    public required string TweetEmbedCode { get; set; }


    public override string ToString()
    {
        return $"(Text: {Text}, Username: {UserName}, LinkToTweet: {LinkToTweet}, Date: {CreatedAt})";
    }
}