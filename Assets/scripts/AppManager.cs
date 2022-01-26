using UnityEngine;

public enum ExperienceType{ FLORENCE, ELSEWHERE, INNOCENTI_ELSEWHERE, NATIONAL_GALLERY, NONE };

public class AppManager : MonoBehaviour {

    public static AppManager Instance { get; private set; }

    [SerializeField] private ExperienceType SelectedExperience;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FirebaseAnalyticsManager.Initialize();
    }

    public void ChangeExperience(string _experience)
    {
        switch (_experience)
        {
            case "":
                {
                    break;
                }
            case "NATIONAL_GALLERY":
                {
                    SelectedExperience = ExperienceType.NATIONAL_GALLERY;
                    break;
                }
            case "FLORENCE":
                {
                    SelectedExperience = ExperienceType.FLORENCE;
                    break;
                }
            case "ELSEWHERE":
                {
                    SelectedExperience = ExperienceType.ELSEWHERE;
                    break;
                }
            case "INNOCENTI_ELSEWHERE":
                {
                    SelectedExperience = ExperienceType.INNOCENTI_ELSEWHERE;
                    break;
                }
        }
    }

    public ExperienceType GetExperienceType()
    {
        return SelectedExperience;
    }
}