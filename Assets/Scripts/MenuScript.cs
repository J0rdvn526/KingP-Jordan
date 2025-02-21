using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusBehaviour : MonoBehaviour {
    public void gotoGame() {
        SceneManager.LoadScene("MainGame");
    }

    public void gotoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void gotoCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void gotoGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}