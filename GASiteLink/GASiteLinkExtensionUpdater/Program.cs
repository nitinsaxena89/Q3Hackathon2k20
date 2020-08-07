using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GASiteLinkExtensionUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            AdWordsUser adwordsUser = new AdWordsUser();
            CampaignExtensionSettingService campaignExtensionSettingService = (CampaignExtensionSettingService)adwordsUser.GetService(AdWordsService.v201809.CampaignExtensionSettingService);
            //retrivew siteLink
            Selector selector = new Selector()
            {
                fields = new string[] {
                    CampaignExtensionSetting.Fields.ExtensionType,
                    ExtensionSetting.Fields.Extensions
                },
                predicates = new Predicate[] {
                    Predicate.Equals(CampaignExtensionSetting.Fields.ExtensionType,
                    FeedType.SITELINK.ToString()),
  },
                paging = Paging.Default
            };

            CampaignExtensionSettingPage page = campaignExtensionSettingService.get(selector);
            CampaignExtensionSetting campaignExtensionSetting = page.entries[0];
        }
    }
}
