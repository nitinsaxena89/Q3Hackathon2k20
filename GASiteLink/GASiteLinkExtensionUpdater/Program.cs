using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using System.Collections.Generic;


//https://github.com/nitinsaxena89/Q3Hackathon2k20

namespace GASiteLinkExtensionUpdater
{
    public class Program : Logger
    {
        public static void Main(string[] args)
        {
            string filePath = "";
            ExcelReader excelReader = new ExcelReader();
            GAFeedsHelper gaFeed = new GAFeedsHelper();
            GASiteLinkModifier modifer = new GASiteLinkModifier();

            List<SiteLinkModel> sitelinksToUpdate = excelReader.Read(filePath);

            foreach(var sitelink in sitelinksToUpdate)
            {
                //Set Account ID
                AdWordsAppConfig adWordsAppConfig = new AdWordsAppConfig();
                adWordsAppConfig.ClientCustomerId = sitelink.AccountId;

                //Create Adwords User using Config
                AdWordsUser adwordsUser = new AdWordsUser(adWordsAppConfig);
                
                //Create FeedItem from SiteLinkFeedItem
                FeedItem feedItem = gaFeed.CreateFeedItem(sitelink);

                //Get Resulting Feed Item Details
                FeedItem updatedFeedItem = modifer.UpdateSiteLinkUsingFeedItemService(adwordsUser, feedItem);

                // Print Updated FeedItem Details
                PrintFeedItemInfo(updatedFeedItem);
            }
        }

        private static void PrintFeedItemInfo(FeedItem feedItem)
        {
            //Print Details
            Log(LogType.INFO, feedItem.feedId.ToString());
            Log(LogType.INFO, feedItem.feedItemId.ToString());
            Log(LogType.INFO, feedItem.status.ToString());
            Log(LogType.INFO, feedItem.attributeValues[0].stringValue);
            Log(LogType.INFO, feedItem.attributeValues[1].stringValue);
            Log(LogType.INFO, feedItem.attributeValues[2].stringValue);
            Log(LogType.INFO, feedItem.attributeValues[3].stringValues[0]);

        }
    }


}
