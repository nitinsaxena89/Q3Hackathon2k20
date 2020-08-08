using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.Selectors;
using Google.Api.Ads.AdWords.v201809;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace GASiteLinkExtensionUpdater
{
    class GAFeedsHelper : Program
    {

        /// <summary>
        /// Gets the feeds.
        /// </summary>
        /// <param name="user">The user for which feeds are retrieved.</param>
        /// <returns>The list of feeds.</returns>
        public Feed[] GetFeeds(AdWordsUser user)
        {
            using (FeedService feedService = (FeedService)user.GetService(AdWordsService.v201809.FeedService))
            {
                Selector selector = new Selector()
                {
                    fields = new string[]
                    {
                        Feed.Fields.Id,
                        Feed.Fields.Name,
                        Feed.Fields.Origin,
                        Feed.Fields.Attributes,
                        Feed.Fields.SystemFeedGenerationData,
                        Feed.Fields.FeedStatus
                    },
                    predicates = new Predicate[]
                    {
                        //Predicate.Contains(Feed.FilterableFields.Name,"sitelink")
                    },
                    paging = Paging.Default
                    
                };

                FeedPage page = feedService.get(selector);
                if (page.totalNumEntries > 0)
                {
                    return page.entries;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the feed items in a feed.
        /// </summary>
        /// <param name="user">The user that owns the feed.</param>
        /// <param name="feedId">The feed ID.</param>
        /// <returns>The list of feed items in the feed.</returns>
        public FeedItem[] GetFeedItems(AdWordsUser user, long feedId)
        {
            using (FeedItemService feedItemService = (FeedItemService)user.GetService(AdWordsService.v201809.FeedItemService))
            {
                Selector selector = new Selector()
                {
                   fields = new string[] { 
                    FeedItem.Fields.FeedId,
                    FeedItem.Fields.FeedItemId,
                    FeedItem.Fields.AttributeValues,
                    FeedItem.Fields.PolicySummaries,
                    FeedItem.Fields.StartTime,
                    FeedItem.Fields.EndTime,
                    FeedItem.Fields.Status,
                    FeedItem.Fields.UrlCustomParameters,
                   },
                    predicates = new Predicate[] {
                    Predicate.Equals(FeedItem.FilterableFields.FeedId,feedId)
                },
                    paging = Paging.Default
                };
                FeedItemPage page = feedItemService.get(selector);
                if (page.totalNumEntries > 0)
                {
                    return page.entries;
                }
                return null;
            }

        }

        /// <summary>
        /// Gets the Feed Item in FeedItem Array
        /// </summary>
        /// <param name="feedItemArray">FeedItem Array</param>
        /// <param name="feedItemId">The Feed Item Id</param>
        /// <returns></returns>
        public FeedItem GetFeedItemByFeedItemId(FeedItem[] feedItemArray,long feedItemId)
        {
            IEnumerable<FeedItem> items = feedItemArray.Where(feedItem => feedItem.feedItemId == feedItemId);
            if (items.Any())
            {
                return items.FirstOrDefault();
            }
            return null;
        }

    }
}
