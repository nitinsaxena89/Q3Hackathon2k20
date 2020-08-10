using Google.Api.Ads.AdWords.v201809;

namespace GASiteLinkExtensionUpdater
{
    public class SiteLinkModel
    {
        private FeedType _feedType;
        
        public string AccountId { get; set; }
        public FeedType TypeOfFeed { get { return _feedType; } set { _feedType = FeedType.SITELINK; } }
        public long FeedId { get; set; }
        public long FeedItemId { get; set; }
        public string Text { get; set; }
        public string DescriptionLine1 { get; set; }
        public string DescriptionLine2 { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
    }
}
