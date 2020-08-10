using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace GASiteLinkExtensionUpdater
{
    public class Program : Logger
    {
        public static void Main(string[] args)
        {
            string filePath = ConfigurationManager.AppSettings["SiteLinkFilePath"];
            ExcelReader excelReader = new ExcelReader();
            GAFeedsHelper gaFeed = new GAFeedsHelper();
            GASiteLinkModifier modifer = new GASiteLinkModifier();

            List<SiteLinkModel> sitelinksToUpdate = excelReader.Read(filePath);

            foreach (var sitelink in sitelinksToUpdate)
            {
                if (sitelink.IsDataValid)
                {
                    //Set Account ID
                    AdWordsAppConfig adWordsAppConfig = new AdWordsAppConfig();
                    adWordsAppConfig.ClientCustomerId = sitelink.AccountId;

                    //Create Adwords User using Config
                    AdWordsUser adwordsUser = new AdWordsUser(adWordsAppConfig);

                    //Create FeedItem from SiteLinkFeedItem
                    FeedItem feedItem = gaFeed.CreateFeedItem(sitelink);

                    //Validate FeedId and FeedItemId is Valid
                    FeedItem[] feedItemArray = gaFeed.GetFeedItems(adwordsUser, feedItem.feedId, feedItem.feedItemId);

                    if (feedItemArray != null && feedItemArray.Count() == 1)
                    {
                        sitelink.IsDataValid = true;

                        //Get Resulting Feed Item Details
                        FeedItem updatedFeedItem = modifer.UpdateSiteLinkUsingFeedItemService(adwordsUser, feedItem);

                        if (updatedFeedItem == null)
                        {
                            sitelink.ActionStatus = "Failed to Update the Entry.Please check the logs for errors.";
                        }
                        else
                        {
                            sitelink.ActionStatus = "Data Updated Successful.";
                        }
                    }

                    else
                    {
                        sitelink.IsDataValid = false;
                        sitelink.ActionStatus = "Either of FeedID/FeedItemID/AccountID is incorrect.";
                    }
                }
                else
                {
                    sitelink.ActionStatus = "No Action Performed.";
                }
            }
            //Write Updated Excel
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet ws = excelPackage.Workbook.Worksheets.First();
                int rowCounter = 2;
                int siteLinkModelIterator = 0;
                for (int rc = rowCounter; rc <= ws.Dimension.End.Row; rc++, siteLinkModelIterator++)
                {
                    ws.Cells[rc, 8].Value = sitelinksToUpdate[siteLinkModelIterator].IsDataValid;
                    ws.Cells[rc, 9].Value = sitelinksToUpdate[siteLinkModelIterator].ActionStatus;
                    excelPackage.Save();
                }
            }
            Log(LogType.INFO, "Batch Processing Completed.");
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
