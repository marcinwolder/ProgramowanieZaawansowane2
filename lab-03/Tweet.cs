class Tweet(string text, string userName, string linkToTweet, string firstLinkUrl, string createdAt, string tweetEmbedCode)
{
    public required string Text { get; set; } = text;
    public required string UserName { get; set; } = userName;
    public required string LinkToTweet { get; set; } = linkToTweet;
    public required string FirstLinkUrl { get; set; } = firstLinkUrl;
    public required string CreatedAt { get; set; } = createdAt;
    public required string TweetEmbedCode { get; set; } = tweetEmbedCode;
}