using System;
using System.Configuration;
using System.Diagnostics; 
using TweetSharp;

namespace TweetingIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterClientInfo twitterClientInfo = new TwitterClientInfo();
            twitterClientInfo.ConsumerKey = ConsumerKey;
            twitterClientInfo.ConsumerSecret = ConsumerSecret;

            TwitterService twitterService = new TwitterService(twitterClientInfo); 

            if (string.IsNullOrEmpty(AccessToken) || string.IsNullOrEmpty(AccessTokenSecret))
            {
                //First need to access the request token and authorization url link 
                OAuthRequestToken requestToken = twitterService.GetRequestToken();
                string authorizationURL = twitterService.GetAuthorizationUri(requestToken).ToString();

                //authorizationURL is a URL, so need to paste it in any web browser 
                Console.WriteLine("Please allow this app to send tweets on your behalf");

                //authorize the app 
                Console.WriteLine("Enter the PIN from the browser");
                string pin = Console.ReadLine();

                //get the access token written by you
                OAuthAccessToken accessToken = twitterService.GetAccessToken(requestToken);
                string token = accessToken.Token; //Note: set the breakpoint here
                string tokenSecret = accessToken.TokenSecret; //another one here

                Console.WriteLine("Write down the accessToken: " + token);
                Console.WriteLine("Write down the accessTokenSecret: " + tokenSecret);
            }
            //otherwise use the existed accessToken and accessTokenSecret
            twitterService.AuthenticateWith(AccessToken, AccessTokenSecret);

            Console.WriteLine("Enter a tweet:");
            string tweetMessage = Console.ReadLine();
            //an API change: SendTweetOptions is kinda a wrapper obj takes mutiple computed properties
            TwitterStatus twitterStatus = twitterService.SendTweet(new SendTweetOptions { Status = tweetMessage });
        }

        /*Main theme issue here: ConfigurationManager not existed. Then, need to add
         * the reference inside the AssemplyInfo file
         * (idea: http://stackoverflow.com/questions/1274852/the-name-configurationmanager-does-not-exist-in-the-current-context)
         */
        private static string ConsumerKey
        {
            get { return ConfigurationManager.AppSettings["ConsumerKey"]  ;  } 
        }

        private static string ConsumerSecret
        {
            get { return ConfigurationManager.AppSettings["ConsumerSecret"]; }
        }

        private static string AccessToken
        {
            get { return ConfigurationManager.AppSettings["AccessToken"]; }
        }

        private static string AccessTokenSecret
        {
            get { return ConfigurationManager.AppSettings["AccessTokenSecret"]; }
        }
    }
}
