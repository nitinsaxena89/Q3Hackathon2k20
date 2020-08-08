using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using System;

namespace GASiteLinkExtensionUpdater
{
    public class GASiteLinkModifier
    {
        /// <summary>
        /// Modifies SiteLink Extension at Customer Level
        /// </summary>
        /// <param name="user">Customer User Id</param>
        /// <param name="sitelinkFeedItem">SiteLinkFeedItem with Details</param>
        /// <returns>Updated CustomerExtensionSettings</returns>
        public CustomerExtensionSetting UpdateSiteLinkAtCustomerLevel(AdWordsUser user,SitelinkFeedItem sitelinkFeedItem)
        {
            CustomerExtensionSettingService customerExtensionSettingService = (CustomerExtensionSettingService)user.GetService(AdWordsService.v201809.CustomerExtensionSettingService);

            ExtensionFeedItem[] extensionFeedItems = new[] { sitelinkFeedItem };
            ExtensionSetting extensionSetting = new ExtensionSetting();
            extensionSetting.extensions = extensionFeedItems;

            CustomerExtensionSetting customerExtensionSetting = new CustomerExtensionSetting();
            customerExtensionSetting.extensionType = FeedType.SITELINK;
            customerExtensionSetting.extensionSetting = extensionSetting;

            CustomerExtensionSettingOperation modifyOperation = new CustomerExtensionSettingOperation();
            modifyOperation.operand = customerExtensionSetting;
            modifyOperation.@operator = Operator.SET;

            try
            {
                CustomerExtensionSettingReturnValue returnValue = customerExtensionSettingService.mutate(new[] { modifyOperation });
                if (returnValue.value != null && returnValue.value.Length > 0)
                {
                    CustomerExtensionSetting modifiedExtensionSetting = returnValue.value[0];
                    Logger.Log(Logger.LogType.INFO, "Modification Successfull");
                    return modifiedExtensionSetting;
                }
                else
                {
                    Logger.Log(Logger.LogType.WARNING,"Nothing Modified");
                    return null;
                }
            }catch (Exception ex)
            {
                Logger.Log(Logger.LogType.EXCEPTION,Environment.NewLine + ex.Message);
                return null;
            }

        }
    }
}
