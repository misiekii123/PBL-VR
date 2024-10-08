using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
    public static GameManager instance;
    public int selectedLevel;
    [Header("List of categories")]
    [SerializeField] private string[] categories = { };
    public string selectedTag;
    [Header("Score")]
    public int points;
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        selectedTag = categories[Random.Range(0, categories.Length)];
        
        points = 0;
        if(text != null) text.text = points.ToString();
    }

    public int getPoints()
    {
        return points;
    }

    public void setPoints(int p)
    {
        points = p;
        if(text != null) text.text = "Poprawne: " + points.ToString();
    }
    public void SelectEasy()
    {
        selectedLevel = 0;
        SceneManager.LoadScene("Main");
    }
    public void SelectMedium()
    {
        selectedLevel = 1;
        SceneManager.LoadScene("Main");
    }
    public void SelectHard()
    {
        selectedLevel = 2;
        SceneManager.LoadScene("Main");
    }
}
