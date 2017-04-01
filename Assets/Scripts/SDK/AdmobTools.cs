using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdmobTools
{
    public class Banner
    {
        public static BannerView BannerView;

        public static AdPosition AdPos;

        public static void RequestBanner(AdSize size = null, AdPosition pos = AdPosition.Bottom)
        {

#if UNITY_EDITOR
            string adUnitId = "ca-app-pub-6544265543071714/8307143984";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-6544265543071714/8307143984";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-6544265543071714/8307143984";
#else
            string adUnitId = "ca-app-pub-6544265543071714/8307143984";
#endif
            AdSize bannerSize = null;
            if (size == null)
            {
                bannerSize = AdSize.Banner;
            }
            else
            {
                bannerSize = size;
            }

            if (BannerView != null)
            {
                BannerView.Destroy();
                BannerView = null;
            }

            // Create a 320x50 banner at the top of the screen.
            BannerView = new BannerView(adUnitId, bannerSize, pos);

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder()
                .Build();

            AdPos = pos;

            // Load the banner with the request.
            BannerView.LoadAd(request);

            BannerView.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            BannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            BannerView.OnAdOpening += HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            BannerView.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            BannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        }

        public static void HandleOnAdLoaded(object sender,EventArgs args)
        {
            Debug.Log("[Admob] HandleOnAdLoaded ");
        }

        public static void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.Log("[Admob] HandleOnAdFailedToLoad " + args.Message);
        }

        public static void HandleOnAdOpened(object sender, EventArgs args)
        {
            Debug.Log("[Admob] HandleOnAdOpened ");
        }

        public static void HandleOnAdClosed(object sender, EventArgs args)
        {
            Debug.Log("[Admob] HandleOnAdClosed " + args);
        }

        public static void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            Debug.Log("[Admob] HandleOnAdClosed ");
        }
    }

    public class Interstitial
    {
        public static void RequestInterstitial()
        {

#if UNITY_ANDROID
            string adUnitId = "INSERT_ANDROID_INTERSTITIAL_AD_UNIT_ID_HERE";
#elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif

            // Initialize an InterstitialAd.
            InterstitialAd interstitial = new InterstitialAd(adUnitId);
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            interstitial.LoadAd(request);
        }
    }

}
