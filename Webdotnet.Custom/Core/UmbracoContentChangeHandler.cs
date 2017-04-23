using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevTrends.MvcDonutCaching;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;

namespace Webdotnet.Custom.Core
{
    public class UmbracoEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentService.Published += UmbracoContentChangeHandler.OnPublished;
            ContentService.Created += UmbracoContentChangeHandler.OnCreated;
            ContentService.Saved += UmbracoContentChangeHandler.OnSaved;
            ContentService.Published += UmbracoContentChangeHandler.OnPublished;
            ContentService.UnPublished += UmbracoContentChangeHandler.OnUnpublished;
            ContentService.Moved += UmbracoContentChangeHandler.OnMoved;
            ContentService.Trashed += UmbracoContentChangeHandler.OnTrashed;
            ContentService.Deleted += UmbracoContentChangeHandler.OnDeleted;
        }
    }
    public class UmbracoContentChangeHandler
    {
        public static void OnCreated(IContentService sender, NewEventArgs<IContent> args)
        {
            RemoveItemsFromCache();
        }

        public static void OnSaved(IContentService sender, SaveEventArgs<IContent> args)
        {
            RemoveItemsFromCache();
        }

        public static void OnPublished(IPublishingStrategy sender, PublishEventArgs<IContent> args)
        {

            RemoveItemsFromCache();
        }

        public static void OnUnpublished(IPublishingStrategy sender, PublishEventArgs<IContent> args)
        {
            RemoveItemsFromCache();
        }

        public static void OnMoved(IContentService sender, MoveEventArgs<IContent> args)
        {
            RemoveItemsFromCache();
        }

        public static void OnTrashed(IContentService sender, MoveEventArgs<IContent> args)
        {
            RemoveItemsFromCache();
        }

        public static void OnDeleted(IContentService sender, DeleteEventArgs<IContent> e)
        {
            RemoveItemsFromCache();
        }

        private static void RemoveItemsFromCache()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems();
        }
    }
}
