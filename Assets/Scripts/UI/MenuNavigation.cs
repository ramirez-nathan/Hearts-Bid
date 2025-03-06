using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("FullMap");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("FullMap"); // load the game scene (File -> Build Profile -> Scene List)
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
