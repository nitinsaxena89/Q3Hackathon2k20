using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

//https://github.com/nitinsaxena89/Q3Hackathon2k20

namespace GASiteLinkExtensionUpdater
{
    public class Program : Logger
    {
        public static void Main(string[] args)
        {
            string accountId = "";
            string feedItemId = "";

            AdWordsAppConfig adWordsAppConfig = new AdWordsAppConfig();
            adWordsAppConfig.ClientCustomerId = accountId;
            AdWordsUser user = new AdWordsUser(adWordsAppConfig);
            GAFeedsHelper gaFeed = new GAFeedsHelper();

            //Get FeedArray for User
            Feed[] feedsArray = gaFeed.GetFeeds(user);

            //Get Feed Item Array
            FeedItem[] feedItemsArray = gaFeed.GetFeedItems(user, feedsArray[0].id);

            //Get FeedItem with specific FeedItemId
            FeedItem feedItem = gaFeed.GetFeedItemByFeedItemId(feedItemsArray, long.Parse(feedItemId));
            PrintFeedItemInfo(feedItem);

            //Modify Feed Item
            GASiteLinkModifier modifer = new GASiteLinkModifier();

            //Create SiteLink Model from Input Values, this Would be from SpreadSheet
            SiteLinkModel modifiedSiteLink = new SiteLinkModel();
            modifiedSiteLink.FeedId = feedItem.feedId;
            modifiedSiteLink.FeedItemId = feedItem.feedItemId;
            modifiedSiteLink.Text = "Ask on JustAnswer Now";
            modifiedSiteLink.DescriptionLine1 = feedItem.attributeValues[1].stringValue;
            modifiedSiteLink.DescriptionLine2 = feedItem.attributeValues[2].stringValue;
            modifiedSiteLink.Url = feedItem.attributeValues[3].stringValues[0];

            //Create SiteLinkFeedItem
            SiteLinkFeedItemHelper siteLinkFeedItemHelper = new SiteLinkFeedItemHelper();
            SitelinkFeedItem sitelinkFeedItem = siteLinkFeedItemHelper.CreateSiteLinkFeedItem(modifiedSiteLink);
            modifer.UpdateSiteLinkAtCustomerLevel(user, sitelinkFeedItem);
            Console.ReadKey();
        }

        private static void PrintFeedItemInfo(FeedItem feedItem)
        {
            //Print Details
            Log(LogType.INFO, feedItem.feedId.ToString());
            Log(LogType.INFO, feedItem.feedItemId.ToString());
            Log(LogType.INFO, feedItem.policySummaries[0].combinedApprovalStatus.ToString());
            Log(LogType.INFO, feedItem.status.ToString());
            Log(LogType.INFO, feedItem.attributeValues[0].stringValue);
            Log(LogType.INFO, feedItem.attributeValues[1].stringValue);
            Log(LogType.INFO, feedItem.attributeValues[2].stringValue);
            Log(LogType.INFO, feedItem.attributeValues[3].stringValues[0]);

        }
    }


}
