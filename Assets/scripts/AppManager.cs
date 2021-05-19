using UnityEngine;
using System.Collections;

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
        }
    }

    public ExperienceType GetExperienceType()
    {
        return SelectedExperience;
    }
}