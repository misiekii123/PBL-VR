using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class DataSaver
{
    public float reactionTime;
    public string selectedTag;
    public string correctTag;
    private string timestamp;
    public string startTimestamp;

    public static string GetTimestamp(DateTime value)
    {
        return value.ToString("yyyy-MM-dd_HH-mm-ss");
    }

    public object GenerateDataEntry()
    {
        timestamp = GetTimestamp(DateTime.Now); 

        return new
        {
            timestamp = timestamp,
            reactionTime = this.reactionTime,
            selectedTag = this.selectedTag,
            correctTag = this.correctTag
        };
    }

    public void SaveToFile()
    {
        string fileName = $"{startTimestamp}.json";
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        List<object> existingData = new List<object>();

        if (File.Exists(fullPath))
        {
            string existingJson = File.ReadAllText(fullPath);

            if (!string.IsNullOrWhiteSpace(existingJson))
            {
                existingData = JsonConvert.DeserializeObject<List<object>>(existingJson);
            }
        }

        existingData.Add(GenerateDataEntry());

        string updatedJson = JsonConvert.SerializeObject(existingData, Formatting.Indented);

        File.WriteAllText(fullPath, updatedJson);

        Debug.Log($"Dane zapisane w pliku: {fullPath}");
    }
}
