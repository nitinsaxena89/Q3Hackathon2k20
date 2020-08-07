using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;

namespace GASiteLinkExtensionUpdater
{
    class SiteLinkDetails
    {
        /// <summary>
        /// Below Method Does API Request and Fetches All FeedItems from an Account with Account Id
        /// </summary>
        /// <param name="adwordsUser">Object for Adwords User</param>
        /// <param name="accountId">Account ID/Client ID</param>
        /// <returns>Feed Item Array</returns>
        public FeedItem[] GetExistingFeedItemsArrayByAccountIdAndFeedItemIdFromGAApi(AdWordsUser adwordsUser, string accountId)
        {
            //Set CustomerId to Adwords User
            ((AdWordsAppConfig)adwordsUser.Config).ClientCustomerId = accountId;
            FeedItemService feedItemService = (FeedItemService)adwordsUser.GetService(AdWordsService.v201809.FeedItemService);
            //Set Which Fields to Fetch
            Selector selector = new Selector()
            {
                fields = new string[] {
                    FeedItem.Fields.FeedId,
                    FeedItem.Fields.FeedItemId,
                    FeedItem.Fields.AttributeValues,
                    FeedItem.Fields.UrlCustomParameters
                },
                predicates = new Predicate[] {
                    //Can Add Predicate Here
                },
                paging = Paging.Default
            };
            //Fire API Request
            FeedItemPage page = feedItemService.get(selector);//Able to Get Feed ID, FeedItemID and Status
            
            if (page.totalNumEntries > 0)
            {
                return page.entries;
            }
            else
            {
                return null;
            }
        }
   
        public FeedItem GetFeedItemDetailsFromFeedItemId(string feedItemID)
        {
            return new FeedItem();

        }
    }
}
