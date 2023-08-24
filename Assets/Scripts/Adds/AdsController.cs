using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour //, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener;
{
    //#region Variables
    public static AdsController instance;

    string _androidGameId;
    string _iOSGameId;
    bool _testMode = true;
    private string _gameId;

    string _Skippable_Video_Id = null;
    string _Rewarded_Video_Id = null;
    string _Banner_Id = null;

    string _androidAdSkippable = "video";
    string _iOSAdSkippable = "video";

    string _androidAdRewarded = "rewardedVideo";
    string _iOSAdRewarded = "reawardedVideo";

    string _androidAdBanner = "banner";
    string _iOSAdBanner = "banner";

}