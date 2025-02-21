using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenusBehaviour : MonoBehaviour {

    private IEnumerator WaitForSoundAndTransition(string sceneName) {
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
        yield return new WaitForSeconds(source.clip.length); // wait for sound to finish
        SceneManager.LoadScene(sceneName); // Load next scene
    }
    
    public void gotoGame() {
        StartCoroutine(WaitForSoundAndTransition("MainGame"));
    }

    public void gotoMenu()
    {
        StartCoroutine(WaitForSoundAndTransition("MainMenu"));
    }

    public void gotoCharacterSelect()
    {
        StartCoroutine(WaitForSoundAndTransition("Charactermenu"));
    }

    public void gotoGameOver()
    {
        StartCoroutine(WaitForSoundAndTransition("GameOver"));
    }
}