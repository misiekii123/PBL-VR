using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public int selectedLevel;
    public static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        DontDestroyOnLoad(this);
    }
    public void SelectEasy()
    {
        selectedLevel = 0; // Easy
        SceneManager.LoadScene("Main");
    }

    public void SelectMedium()
    {
        selectedLevel = 1; // Medium
        SceneManager.LoadScene("Main");
    }

    public void SelectHard()
    {
        selectedLevel = 2; // Hard
        SceneManager.LoadScene("Main");
    }
}
