using Google.Api.Ads.AdWords.v201809;

namespace GASiteLinkExtensionUpdater
{
    public class SiteLinkFeedItemHelper
    {
        public SitelinkFeedItem CreateSiteLinkFeedItem(SiteLinkModel siteLinkModel)
        {
            SitelinkFeedItem sitelinkFeedItem = new SitelinkFeedItem();
            sitelinkFeedItem.feedId = siteLinkModel.FeedId;
            sitelinkFeedItem.feedItemId = siteLinkModel.FeedItemId;
            sitelinkFeedItem.feedType = siteLinkModel.TypeOfFeed;
            sitelinkFeedItem.sitelinkText = siteLinkModel.Text;
            sitelinkFeedItem.sitelinkLine2 = siteLinkModel.DescriptionLine1;
            sitelinkFeedItem.sitelinkLine3 = siteLinkModel.DescriptionLine2;
            sitelinkFeedItem.sitelinkFinalUrls = new UrlList()
            {
                urls = new string[]
                        {
                            siteLinkModel.Url
                        }
            };
            return sitelinkFeedItem;
        }


    }
}
