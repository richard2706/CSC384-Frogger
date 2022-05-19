using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action<Profile> OnProfileChanged;
    public static Profile SelectedProfile { get; private set; }
    public static bool Multiplayer { get; set; }

    public const int NumLevels = 3;

    private const int defaultProfileID = 1;

    public bool SelectProfile(int profileID)
    {
        bool profileChanged = false;
        SelectedProfile = SaveManager.LoadProfile(profileID);
        if (SelectedProfile == null)
        {
            Profile profile = new Profile(profileID);
            if (SaveManager.SaveProfile(profile))
            {
                SelectedProfile = profile;
                profileChanged = true;
            }
            else
            {
                Debug.Log("Profile " + profileID + " could not be created");
            }
        }
        else
        {
            profileChanged = true;
        }
        if (profileChanged) OnProfileChanged?.Invoke(SelectedProfile);
        return profileChanged;
    }

    private void Awake()
    {
        EnforcePersistentSingletonInstance();
        SelectProfile(defaultProfileID);
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
}
