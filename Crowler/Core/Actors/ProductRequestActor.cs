using System;
using System.Net;
using Abot.Crawler;
using Abot.Poco;
using Akka.Actor;
using Crowler.Core.Messages;

namespace Crowler.Core.Actors
{
    public class ProductRequestActor : BaseActor
    {
        protected override void OnReceive(object message)
        {
            if (message is ProductRequestMessage)
            {
                var request = (ProductRequestMessage) message;

                var response = Process(request);

                Sender.Tell(response, Self);
            }
            else if (message is Terminated)
            {
                var t = (Terminated) message;
                if (t.ActorRef == null)
                {
                    Context.Stop(Self);
                }
            }
            else
            {
                Unhandled(message);
            }
        }

        private ProductResponseMessage Process(ProductRequestMessage request)
        {
            var crawlConfig = new CrawlConfiguration
            {
                CrawlTimeoutSeconds = 100,
                MaxConcurrentThreads = 10,
                MaxPagesToCrawl = 1000,
                UserAgentString = "abot v1.0 http://code.google.com/p/abot"
            };


            var crawler = new PoliteWebCrawler(crawlConfig, null, null, null, null, null, null, null, null);

            crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
            crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
            crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
            crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

            var result = crawler.Crawl(new Uri(request.Url));

            return new ProductResponseMessage
            (
                id :1,
                price : 1,
                responsedAt : DateTime.UtcNow,
                status : ProductResponseStatus.Succseed
            );
        }

        private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            var pageToCrawl = e.PageToCrawl;
            Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri,
                pageToCrawl.ParentUri.AbsoluteUri);
        }

        private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var crawledPage = e.CrawledPage;


            //crawledPage.Content.Text;

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
            else
                Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                Console.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);
        }

        private void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            var crawledPage = e.CrawledPage;
            Console.WriteLine("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri,
                e.DisallowedReason);
        }

        private void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            var pageToCrawl = e.PageToCrawl;
            Console.WriteLine("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
        }




    }
}