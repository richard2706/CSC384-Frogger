using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    private const string profileFileName = "profile{0}";

    public static Profile LoadProfile(int profileID)
    {
        if (!ProfileExists(profileID)) return null;

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetSavePath(profileID), FileMode.Open))
        {
            try
            {
                return formatter.Deserialize(stream) as Profile;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public static bool SaveProfile(Profile profile)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(GetSavePath(profile.ProfileID), FileMode.Create))
        {
            try
            {
                formatter.Serialize(stream, profile);
            }
            catch (Exception)
            {
                return false;
            }
        }
        return true;
    }

    public static bool DeleteProfile(int profileID)
    {
        try
        {
            File.Delete(GetSavePath(profileID));
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    private static bool ProfileExists(int profileID)
    {
        return File.Exists(GetSavePath(profileID));
    }

    private static string GetSavePath(int profileID)
    {
        string fileName = string.Format(profileFileName, profileID);
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}