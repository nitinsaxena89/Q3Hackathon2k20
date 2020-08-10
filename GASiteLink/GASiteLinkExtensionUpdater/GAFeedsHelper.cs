using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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
                        Predicate.Contains(Feed.FilterableFields.Name,"sitelink")
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
        public FeedItem[] GetFeedItems(AdWordsUser user, long feedId, [Optional]long feedItemId)
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
                };

                Predicate[] predicates;
                if(feedItemId != 0)
                {
                    predicates = new Predicate[] {
                        Predicate.Equals(FeedItem.FilterableFields.FeedId, feedId),
                        Predicate.Equals(FeedItem.FilterableFields.FeedItemId, feedItemId),
                    };
                }
                else
                {
                    predicates = new Predicate[] {
                        Predicate.Equals(FeedItem.FilterableFields.FeedId, feedId),
                    };
                }

                selector.predicates = predicates;
                selector.paging = Paging.Default;

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
        public FeedItem GetFeedItemByFeedItemId(FeedItem[] feedItemArray, long feedItemId)
        {
            IEnumerable<FeedItem> items = feedItemArray.Where(feedItem => feedItem.feedItemId == feedItemId);
            if (items.Any())
            {
                return items.FirstOrDefault();
            }
            return null;
        }
        
        /// <summary>
        ///Creates FeedItem Object from SiteLinkModel Object 
        /// </summary>
        /// <param name="siteLinkModelObject">SiteLinkModelObject</param>
        /// <returns>Feed Item</returns>
        public FeedItem CreateFeedItem(SiteLinkModel siteLinkModelObject)
        {
            FeedItem feedItem = new FeedItem();
            feedItem.feedId = siteLinkModelObject.FeedId;
            feedItem.feedItemId = siteLinkModelObject.FeedItemId;

            //Feed Attribute ID for Text is 1
            FeedItemAttributeValue text = new FeedItemAttributeValue();
            text.feedAttributeId = 1;
            text.stringValue = siteLinkModelObject.Text;

            //Feed Attribute ID for Description Line#1 is 3
            FeedItemAttributeValue descriptionLine1 = new FeedItemAttributeValue();
            descriptionLine1.feedAttributeId = 3;
            descriptionLine1.stringValue = siteLinkModelObject.DescriptionLine1;

            //Feed Attribute ID for Description Line2 is 4
            FeedItemAttributeValue descriptionLine2 = new FeedItemAttributeValue();
            descriptionLine2.feedAttributeId = 4;
            descriptionLine2.stringValue = siteLinkModelObject.DescriptionLine2;

            //Feed Attribute ID for Final URL is 5
            FeedItemAttributeValue finalUrl = new FeedItemAttributeValue();
            finalUrl.feedAttributeId = 5;
            finalUrl.stringValues = new string[]
                {
                    siteLinkModelObject.Url
                };

            feedItem.attributeValues = new FeedItemAttributeValue[]
            {
                text,
                descriptionLine1,
                descriptionLine2,
                finalUrl
            };

            return feedItem;
        }
    }
}
