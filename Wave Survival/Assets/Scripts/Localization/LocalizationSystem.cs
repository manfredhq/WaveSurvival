using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem
{
    public enum Language
    {
        English,
        French
    }

    public static Language language = Language.English;

    private static Dictionary<string, string> localizedEN;
    private static Dictionary<string, string> localizedFR;
    private static Dictionary<string, string> localizedENQuest;
    private static Dictionary<string, string> localizedFRQuest;

    public static bool isInit;

    public static void ChangeLanguage(Language value)
    {
        language = value;
        Init();
    }

    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localizedEN = csvLoader.GetDictionaryValues("en");
        localizedENQuest = csvLoader.GetQuestDictionaryValues("en");
        localizedFR = csvLoader.GetDictionaryValues("fr");
        localizedFRQuest = csvLoader.GetQuestDictionaryValues("fr");


        isInit = true;
    }

    public static string GetLocalizedValue(string key, bool isQuest = false)
    {
        if (!isInit)
        {
            Init();
        }
        string value = key;

        switch (PlayerPrefs.GetString("language", "en"))
        {

            case "en":
                if (!isQuest)
                    localizedEN.TryGetValue(key, out value);
                else
                    localizedENQuest.TryGetValue(key, out value);
                break;
            case "fr":
                if (!isQuest)
                    localizedFR.TryGetValue(key, out value);
                else
                    localizedFRQuest.TryGetValue(key, out value);
                break;
        }

        return value;
    }


}
