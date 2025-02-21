using UnityEngine;
using TMPro;

// [CreateAssetMenu(fileName = "Pins", menuName = "Scriptable Objects/Pins")]
public class CharacterManager
{
    public Pins pinsDB;

    public static int selection = 0;
    public SpriteRenderer sprite;
    public TMP_Text nameLabel;

    void updateCharacter() {
    Pin current = pinsDB.getPin(selection);
    sprite.sprite = current.prefab.GetComponent<SpriteRenderer>().sprite;
    nameLabel.SetText(current.name);
    }

    public void next() {
        int numberPins = pinsDB.getCount();
        if (selection < numberPins - 1) {
            selection = selection + 1;
        }
        updateCharacter();
    }

    public void previous() {
        if (selection > 0) {
            selection = selection - 1;
        }
        updateCharacter();
    }
}
