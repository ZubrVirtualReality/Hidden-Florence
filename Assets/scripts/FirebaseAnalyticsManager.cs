using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Analytics;
using System;

public static class FirebaseAnalyticsManager
{
    private static bool isFirebaseAvailable = false;
    private static string currentSceneName;
    private static string nextSceneName;
    private static string latestLoggedSceneName;
    private static string toLogSceneName;

    static public void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                isFirebaseAvailable = true;
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            }
            else
            {
                isFirebaseAvailable = false;
                Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    static public void ExitExperiencePress(string goToSceneName)
    {
        bool hasUserGivenFeedback = PlayerPrefs.GetInt(currentSceneName, 0) != 0;
        if (hasUserGivenFeedback)
        {
            SceneManager.LoadScene(goToSceneName);
        }
        else
        {
            nextSceneName = goToSceneName;
            toLogSceneName = currentSceneName;
            Debug.Log("vedeta: " + toLogSceneName + " " + hasUserGivenFeedback);
            SceneManager.LoadScene("Feedback");
        }
    }

    static public void LogUserFeedbackSkipped()
    {
        string goToSceneName = nextSceneName;
        nextSceneName = null;
        toLogSceneName = null;
        SceneManager.LoadScene(goToSceneName);
    }

    public enum FeedbackType { Negative, Neutral, Positive};
    static public void LogUserFeedback(FeedbackType feedback)
    {
        string itemListID;
        switch (toLogSceneName)
        {
            case "imageAnchor":
                {
                    itemListID = "San Pier - National Gallery";
                    break;
                }
            case "tapToPlace":
                {
                    itemListID = "San Pier - Florence/Elsewhere";
                    break;
                }
            case "imageAnchorInnocenti":
                {
                    itemListID = "Santa Maria - Museo degli Innocenti (Inside)";
                    break;
                }
            case "ChapelDoor":
                {
                    itemListID = "Santa Maria - Museo degli Innocenti (Outside)";
                    break;
                }
            case "ChapelStatues":
                {
                    itemListID = "Santa Maria - V&A Museum";
                    break;
                }
            case "tapToPlaceInnocenti":
                {
                    itemListID = "Santa Maria - Elsewhere";
                    break;
                }
            default:
                return;
        }

        if (isFirebaseAvailable)
        {
            Parameter[] parameters = {
              new Parameter(FirebaseAnalytics.ParameterItemListID, itemListID),
              new Parameter(FirebaseAnalytics.ParameterItemListName, Enum.GetName(typeof(FeedbackType), feedback))
            };
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventViewItemList, parameters);
        }

        PlayerPrefs.SetInt(toLogSceneName, 1);
        string goToSceneName = nextSceneName;
        nextSceneName = null;
        toLogSceneName = null;
        SceneManager.LoadScene(goToSceneName);
    }

    public enum PlatformType { iOS, Android }
    static public void LogDownloadHiddenFlorence(PlatformType platform)
    {
        Parameter[] parameters = {
          new Parameter(FirebaseAnalytics.ParameterContentType, "Download Hidden Florence"),
          new Parameter(FirebaseAnalytics.ParameterItemId, Enum.GetName(typeof(PlatformType), platform))
         };
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSelectContent, parameters);
    }

    static public void LogHotspotView(string hotspotTitle)
    {
        Parameter[] parameters = {
          new Parameter(FirebaseAnalytics.ParameterContentType, "View Hotspot"),
          new Parameter(FirebaseAnalytics.ParameterItemId, hotspotTitle)
         };
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSelectContent, parameters);
    }

    static public void LogOpenURL(string url)
    {
        Parameter[] parameters = {
          new Parameter(FirebaseAnalytics.ParameterContentType, "Open URL"),
          new Parameter(FirebaseAnalytics.ParameterItemId, url)
         };
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSelectContent, parameters);
    }

    static private void LogScreenView(string screenName)
    {
        FirebaseAnalytics.LogEvent(
            FirebaseAnalytics.EventScreenView,
            FirebaseAnalytics.ParameterScreenName,
            screenName
        );
    }

    static private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        string screenName = null;
        switch (currentSceneName)
        {
            case "IntroScene":
                {
                    screenName = "Home";
                    break;
                }
            case "AboutPage":
                {
                    screenName = "About";
                    break;
                }
            case "Credits":
                {
                    screenName = "Credits";
                    break;
                }
            case "Feedback":
                {
                    screenName = "Feedback";
                    break;
                }
            case "SanPier":
            case "SanPierWhereAreYou":
                {
                    screenName = "San Pier Selector";
                    break;
                }
            case "imageAnchor":
                {
                    screenName = "San Pier - National Gallery";
                    break;
                }
            case "tapToPlace":
                {
                    screenName = "San Pier - Florence/Elsewhere";
                    break;
                }
            case "SantaMaria":
            case "MariaWhereAreYou":
                {
                    screenName = "Santa Maria Selector";
                    break;
                }
            case "imageAnchorInnocenti":
                {
                    screenName = "Santa Maria - Museo degli Innocenti (Inside)";
                    break;
                }
            case "ChapelDoor":
                {
                    screenName = "Santa Maria - Museo degli Innocenti (Outside)";
                    break;
                }
            case "ChapelStatues":
                {
                    screenName = "Santa Maria - V&A Museum";
                    break;
                }
            case "tapToPlaceInnocenti":
                {
                    screenName = "Santa Maria - Elsewhere";
                    break;
                }
        }

        if (screenName != null & screenName != latestLoggedSceneName)
        {
            latestLoggedSceneName = screenName; 
            LogScreenView(screenName);
        }
    }
}
