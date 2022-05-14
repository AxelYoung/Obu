using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TailModifierUI : MonoBehaviour {

    public InputField amountInput;
    public Toggle positive;
    public Toggle negative;

    public int CalculateAmount() {
        int amount = int.Parse(amountInput.text);
        amount *= positive.isOn ? 1 : -1;
        return amount;
    }

    public void SetAmount(int amount) {
        if (amount < 0) {
            negative.isOn = true;
            amount = -amount;
        } else {
            positive.isOn = true;
        }
        amountInput.text = amount.ToString();
    }
}
