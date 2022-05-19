using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static Profile SelectedProfile { get; private set; }
    public static bool Multiplayer { get; set; }

    public const int NumLevels = 3;

    private const int defaultProfileID = 1;

    private void Awake()
    {
        EnforcePersistentSingletonInstance();
        SelectDefaultProfile();
        Multiplayer = false;
    }

    private void EnforcePersistentSingletonInstance()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void SelectDefaultProfile()
    {
        SelectedProfile = SaveManager.LoadProfile(defaultProfileID);
        if (SelectedProfile == null)
        {
            Profile defaultProfile = new Profile(defaultProfileID);
            if (!SaveManager.SaveProfile(defaultProfile))
            {
                Debug.Log("Default profile could not be created");
            }
            SelectedProfile = defaultProfile;
        }
    }
}
