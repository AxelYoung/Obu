using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    bool animating = false;

    public AudioClip whooshSound;
    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void MoveAlongXAxis(int units) {
        if (!animating) {
            animating = true;
            LeanTween.moveLocalX(transform.gameObject, transform.localPosition.x + units, 0.5f).setEase(LeanTweenType.easeInOutQuint).setOnComplete(() => animating = false);
            audioSource.PlayOneShot(whooshSound);
        }
    }

    public void ExitGame() {
        Application.Quit();
    }
}
