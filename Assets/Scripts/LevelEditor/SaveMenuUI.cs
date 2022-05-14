using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenuUI : MonoBehaviour {
    [SerializeField] GameObject saveMenu;
    bool activeState = false;

    public void OpenCloseSaveMenu() {
        activeState = !activeState;
        saveMenu.SetActive(activeState);
    }

}
