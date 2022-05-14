using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailModifierText : MonoBehaviour {

    public SpriteRenderer[] singleDigitSprites;
    public SpriteRenderer[] doubleDigitSprites;
    int modifierAmount;

    public Sprite[] spriteDigits;

    SpriteRenderer[] activeArray;

    void Start() {
        modifierAmount = GetComponent<TailModifierTile>().tailAmountModifier;
        SetText();
    }

    void SetText() {
        activeArray = Mathf.Abs(modifierAmount) > 0 ? doubleDigitSprites : singleDigitSprites;
        SetDigit(modifierAmount > 0 ? 10 : 11, 0);
        for (int i = 1; i < activeArray.Length / 2; i++) SetDigit(Mathf.Abs(modifierAmount) / (i == 1 ? 1 : 10) % 10, i * 2);
        foreach (SpriteRenderer spriteRenderer in activeArray) {
            spriteRenderer.gameObject.SetActive(true);
        }
    }

    void SetDigit(int digitIndex, int position) {
        activeArray[position].sprite = spriteDigits[digitIndex];
        activeArray[position + 1].sprite = spriteDigits[digitIndex];
    }
}
