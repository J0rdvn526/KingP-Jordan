using UnityEngine;

public class MenusBehaviour : MonoBehaviour {
    public void gotoGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void gotoMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void gotoCharacterSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelect");
    }

    public void gotoGameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}