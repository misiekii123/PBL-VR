using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
    public static GameManager instance;
    [Header("List of categories")]
    [SerializeField] private string[] categories = { };
    public string selectedTag;
    [Header("Score")]
    public int points;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text tagText;

    private MenuManager menuManager;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        selectedTag = categories[Random.Range(0, categories.Length)];
        
        points = 0;
        if(text != null) text.text = points.ToString();
    }

    private void Start()
    {
        tagText.text = selectedTag;
        StartCoroutine(HideTextAfterDelay(5f));
        menuManager = FindObjectOfType<MenuManager>();
    }
    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tagText.text = "";
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

    public void exitToMenu()
    {
        Destroy(menuManager);
        SceneManager.LoadScene("Menu");
    }
}
