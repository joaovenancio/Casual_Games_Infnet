/*
 * Unity Documentation
 * https://docs.unity.com/ads/UnityDeveloperIntegrations.html
 */

using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    #region Variables
    public static AdsController instance;

    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] string _Skippable_Video_Id = null;
    [SerializeField] string _Rewarded_Video_Id = null;
    [SerializeField] string _Banner_Id = null;

    [SerializeField] string _androidAdSkippable = "video";
    [SerializeField] string _iOSAdSkippable = "video";

    [SerializeField] string _androidAdRewarded = "rewardedVideo";
    [SerializeField] string _iOSAdRewarded = "rewardedVideo";

    [SerializeField] string _androidAdBanner = "banner";
    [SerializeField] string _iOSAdBanner = "banner";

    string _adUnitId = null; // This will remain null for unsupported platforms
    #endregion

    #region Unity Methods
    void Awake()
    {
        instance = this;
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _Skippable_Video_Id = _iOSAdSkippable;
        _Rewarded_Video_Id = _iOSAdRewarded;
        _Banner_Id = _iOSAdBanner;
#elif UNITY_ANDROID
        _Skippable_Video_Id = _androidAdSkippable;
        _Rewarded_Video_Id = _androidAdRewarded;
        _Banner_Id = _androidAdBanner;
#endif        
        InitializeAds();
    }
    #endregion

    #region Initialization
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadBanner();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
    #endregion

    #region SkippableVideo
    public void LoadSkippableVideo()
    {
        _adUnitId = _Skippable_Video_Id;
        Advertisement.Load(_adUnitId, this);
        ShowSkippableVideo();
    }
    public void ShowSkippableVideo()
    {
        Advertisement.Show(_Skippable_Video_Id, this);
    }
    #endregion

    #region RewardedVideo

    public void LoadRewardedVideo()
    {
        _adUnitId = _Rewarded_Video_Id;
        Advertisement.Load(_adUnitId, this);
        ShowRewardedVideo();
    }
    public void ShowRewardedVideo()
    {
        Advertisement.Show(_Rewarded_Video_Id, this);
    }
    #endregion

    #region BannerVideo
    public void LoadBanner()
    {
        _adUnitId = _Banner_Id;

        // Set up options to notify the SDK of load events:
        BannerLoadOptions _options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(_adUnitId, _options);
    }
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBanner();
    }
    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }
    // Implement a method to call when the Hide Banner button is clicked:
    public void ShowBanner()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions _options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(_Banner_Id, _options);
    }
    void OnBannerClicked()
    {

    }
    void OnBannerShown()
    {
    }
    public void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }
    void OnBannerHidden()
    {
    }
    #endregion

    #region Others Methods
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_Skippable_Video_Id))
        {
            Debug.Log("OnUnityAdsAdLoaded - _Skippable_Video Loaded");
        }
        else if (adUnitId.Equals(_Rewarded_Video_Id))
        {
            Debug.Log("OnUnityAdsAdLoaded - _Rewarded_Video Loaded");
        }
        else if (adUnitId.Equals(_Banner_Id))
        {
            Debug.Log("OnUnityAdsAdLoaded - _Banner Loaded");

        }
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {

            if (adUnitId.Equals(_Rewarded_Video_Id))
            {
                Debug.Log("Unity Ads Rewarded Video Completed");
                Debug.Log("Player rewarded");
                // Grant a reward.

            }
            if (adUnitId.Equals(_Skippable_Video_Id))
            {
                Debug.Log("Unity Ads Skippable Video Completed");
                Debug.Log("Player rewarded");
                // Grant a reward.

            }
        }
        else
        {
            Debug.Log("Unity Ads Video Not Completed");

        }


    }
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }


    public bool IsShowing()
    {
        return Advertisement.isShowing;
    }
    #endregion
}