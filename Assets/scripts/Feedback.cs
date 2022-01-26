using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static FirebaseAnalyticsManager;
using UnityEngine.SceneManagement;

public class Feedback : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Button submitButton;

    private void Update()
    {
        if (toggleGroup.AnyTogglesOn())
        {
            submitButton.interactable = true;
        }
        else
        {
            submitButton.interactable = false;
        }
    }

    public void BackButtonPress()
    {
        LogUserFeedbackSkipped();
    }

    public void SubmitButtonPress()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        FeedbackType feedback;
        switch (toggle.name)
        {
            case "Toggle Negative":
                feedback = FeedbackType.Negative;
                break;
            case "Toggle Neutral":
                feedback = FeedbackType.Neutral;
                break;
            case "Toggle Positive":
                feedback = FeedbackType.Positive;
                break;
            default:
                return;
        }

        LogUserFeedback(feedback);
    }
}
