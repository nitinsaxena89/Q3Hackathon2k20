using Google.Api.Ads.AdWords.v201809;

namespace GASiteLinkExtensionUpdater
{
    public class SiteLinkModel
    {
        /// <summary>
        /// Stores Feed Id
        /// </summary>
        public long FeedId { get; set; }

        /// <summary>
        /// Stores Feed Item Id
        /// </summary>
        public long FeedItemId { get; set; }

        /// <summary>
        /// Stores Account ID
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Stores Text Value for SiteLink
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Stores Description Line 1 for SiteLink
        /// </summary>
        public string DescriptionLine1 { get; set; }

        /// <summary>
        /// Stores Description Line 2 for SiteLink
        /// </summary>
        public string DescriptionLine2 { get; set; }

        /// <summary>
        /// Stores URL for SiteLink
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Stores Type of Feed. For SiteLink Item, Feed Type is SITELINK
        /// </summary>
        public FeedType TypeOfFeed { get { return _feedType; } set { _feedType = FeedType.SITELINK; } }

        public bool IsDataValid { get; set; }
        public string ActionStatus { get; set; }
        
        private FeedType _feedType;
        
    }
}
