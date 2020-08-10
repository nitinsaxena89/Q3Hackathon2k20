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
        public CustomerExtensionSetting UpdateSiteLinkAtCustomerLevelUsingCustExtSetService(AdWordsUser user, SitelinkFeedItem sitelinkFeedItem)
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
                    Logger.Log(Logger.LogType.INFO, "Modification Successful");
                    return modifiedExtensionSetting;
                }
                else
                {
                    Logger.Log(Logger.LogType.WARNING, "Nothing Modified");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.LogType.EXCEPTION, Environment.NewLine + ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Update Site Link using Feed Item Service
        /// </summary>
        /// <param name="user"></param>
        /// <param name="feedItem"></param>
        /// <returns></returns>
        public FeedItem UpdateSiteLinkUsingFeedItemService(AdWordsUser user, FeedItem feedItem)
        {
            FeedItemService feedItemService = (FeedItemService)user.GetService(AdWordsService.v201809.FeedItemService);
            FeedItemOperation feedItemOperation = new FeedItemOperation();
            feedItemOperation.operand = feedItem;
            feedItemOperation.@operator = Operator.SET;
            try
            {
                FeedItemReturnValue feedItemReturnValue = feedItemService.mutate(new[] { feedItemOperation });
                if (feedItemReturnValue.value != null && feedItemReturnValue.value.Length > 0)
                {
                    FeedItem modifiedFeedItem = feedItemReturnValue.value[0];
                    Logger.Log(Logger.LogType.INFO, "Modification Successful");
                    return modifiedFeedItem;
                }
                else
                {
                    Logger.Log(Logger.LogType.WARNING, "Nothing Modified");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.LogType.EXCEPTION, Environment.NewLine + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Updates Site Link Extension at Campaign Level
        /// </summary>
        /// <param name="user">Adwords User</param>
        /// <param name="sitelinkFeedItem"></param>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public CampaignExtensionSetting UpdateSiteLinkAtCampaignLevel(AdWordsUser user, SitelinkFeedItem sitelinkFeedItem,long campaignId)
        {
            CampaignExtensionSettingService campaignExtensionSettingService = (CampaignExtensionSettingService)user.GetService(AdWordsService.v201809.CampaignExtensionSettingService);

            ExtensionFeedItem[] extensionFeedItems = new ExtensionFeedItem[] { sitelinkFeedItem };

            ExtensionSetting extensionSetting = new ExtensionSetting();
            extensionSetting.extensions = extensionFeedItems;

            CampaignExtensionSetting campaignExtensionSetting = new CampaignExtensionSetting();
            campaignExtensionSetting.campaignId = campaignId;
            campaignExtensionSetting.extensionType = FeedType.SITELINK;
            campaignExtensionSetting.extensionSetting = extensionSetting;

            CampaignExtensionSettingOperation campaignExtensionSettingOperation = new CampaignExtensionSettingOperation();
            campaignExtensionSettingOperation.operand = campaignExtensionSetting;
            campaignExtensionSettingOperation.@operator = Operator.SET;

            try
            {
                // Add the extensions.
                CampaignExtensionSettingReturnValue campaignExtensionSettingReturnValue =  campaignExtensionSettingService.mutate(new [] { campaignExtensionSettingOperation  });
                if (campaignExtensionSettingReturnValue.value != null && campaignExtensionSettingReturnValue.value.Length > 0)
                {
                    CampaignExtensionSetting modifiedExtensionItem = campaignExtensionSettingReturnValue.value[0];
                    Logger.Log(Logger.LogType.INFO, "Modification Successful");
                    return modifiedExtensionItem;
                }
                else
                {
                    Logger.Log(Logger.LogType.WARNING, "Nothing Modified");
                    return null;
                }

            }
            catch (Exception ex)
            {
                Logger.Log(Logger.LogType.EXCEPTION, Environment.NewLine + ex.Message);
                return null;
            }

        }
    
        /// <summary>
        /// Restrict a Feed Item to an AdGroup 
        /// </summary>
        /// <param name="user">Adwords User</param>
        /// <param name="feedItem">Feed Item</param>
        /// <param name="adGroupId">Ad Group ID</param>
        /// <returns></returns>
        public FeedItemAdGroupTarget RestrictFeedItemToAdGroup(AdWordsUser user,FeedItem feedItem, long adGroupId)
        {
            FeedItemTargetService feedItemTargetService = (FeedItemTargetService)user.GetService(AdWordsService.v201809.FeedItemTargetService);

            FeedItemAdGroupTarget feedItemAdGroupTarget = new FeedItemAdGroupTarget();
            feedItemAdGroupTarget.feedId = feedItem.feedId;
            feedItemAdGroupTarget.feedItemId = feedItem.feedItemId;
            feedItemAdGroupTarget.targetType = FeedItemTargetType.AD_GROUP;
            feedItemAdGroupTarget.adGroupId = adGroupId;

            FeedItemTargetOperation feedItemTargetOperation = new FeedItemTargetOperation();
            feedItemTargetOperation.operand = feedItemAdGroupTarget;
            feedItemTargetOperation.@operator = Operator.SET;

            try
            {
                FeedItemTargetReturnValue feedItemTargetReturnValue = feedItemTargetService.mutate(new[] { feedItemTargetOperation });
                if (feedItemTargetReturnValue.value != null && feedItemTargetReturnValue.value.Length > 0)
                {
                    FeedItemAdGroupTarget modifiedTargetItem = (FeedItemAdGroupTarget)feedItemTargetReturnValue.value[0];
                    Logger.Log(Logger.LogType.INFO, "Modification Successful");
                    return modifiedTargetItem;
                }
                else
                {
                    Logger.Log(Logger.LogType.WARNING, "Nothing Modified");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.LogType.EXCEPTION, Environment.NewLine + ex.Message);
                return null;
            }
        }
    }
}
