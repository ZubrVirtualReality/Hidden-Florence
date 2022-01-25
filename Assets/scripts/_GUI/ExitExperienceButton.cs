using UnityEngine;
using UnityEngine.UI;

public class ExitExperienceButton : MonoBehaviour
{
    public string goToSceneName;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(onPress);
    }

    public void onPress()
    {
        FirebaseAnalyticsManager.ExitExperiencePress(goToSceneName);
    }
}
