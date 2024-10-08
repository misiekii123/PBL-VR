using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void SelectEasy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.selectedLevel = 0; // Easy
        }
        SceneManager.LoadScene("Main");
    }

    public void SelectMedium()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.selectedLevel = 1; // Medium
        }
        SceneManager.LoadScene("Main");
    }

    public void SelectHard()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.selectedLevel = 2; // Hard
        }
        SceneManager.LoadScene("Main");
    }
}
