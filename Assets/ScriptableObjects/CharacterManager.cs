using UnityEngine;
using TMPro;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
    public Pins pinsDB;

    public static int selection = 0;
    public SpriteRenderer sprite;
    public TMP_Text nameLabel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateCharacter();
    }

    void updateCharacter() {
    Pin current = pinsDB.getPin(selection);
    sprite.sprite = current.prefab.GetComponent<SpriteRenderer>().sprite;
    nameLabel.SetText(current.name);
    }

    private IEnumerator WaitForSoundAndTransition() {
    AudioSource source = GetComponentInChildren<AudioSource>();
    source.Play();
    yield return new WaitForSeconds(source.clip.length); // wait for sound to finish
    }

    public void next() {
        StartCoroutine(WaitForSoundAndTransition());
        int numberPins = pinsDB.getCount();
        if (selection < numberPins - 1) {
            selection = selection + 1;
        } else {
            selection = 0;
        }
        
        updateCharacter();
    }

    public void previous() {
        StartCoroutine(WaitForSoundAndTransition());
        if (selection > 0) {
            selection = selection - 1;
        } else {
            selection = pinsDB.getCount() - 1;
        }
        
        updateCharacter();
    }
}
