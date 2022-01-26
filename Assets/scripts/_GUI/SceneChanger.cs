using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject background;
	public Animator animator;

    private string previousScene;

    private void Awake()
    {
        background.SetActive(true);

    }

    public void FadeToPreviousScene()
	{
		string sceneName = SceneManager.GetActiveScene().name;

		switch(sceneName) {
			case "ExperienceLoader":
            case "imageAnchor":
            case "tapToPlace":
				previousScene = "ExperienceSelector";
				break;
            default:
				previousScene = "IntroScene";
				break;
		}

		animator.SetTrigger("FadeOut");
	}

    public void FadeToScene(string sceneName)
	{
		previousScene = sceneName;
		animator.SetTrigger("FadeOut");
	}

	public void OnFadeComplete()
	{
		SceneManager.LoadScene(previousScene);
		previousScene = null;
	}

    public void SelectExperience(string experienceName)
	{
        Debug.Log(experienceName);
		AppManager.Instance.ChangeExperience(experienceName);
	}

    public void OpenURL(string url)
    {
		FirebaseAnalyticsManager.LogOpenURL(url);

		Application.OpenURL(url);
    }
}
