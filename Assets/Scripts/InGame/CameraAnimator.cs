using UnityEngine;
using UnityEngine.UI;

public class CameraAnimator : MonoBehaviour {


    [SerializeField] RawImage fadeImage;

    LevelLoader levelLoader;

    void Awake() {
        levelLoader = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<LevelLoader>();
        levelLoader.LevelLoaded += FadeLevelIn;
        levelLoader.LevelExit += FadeLevelOut;
    }

    void FadeLevelIn() {
        fadeImage.color += new Color(0, 0, 0, 1);
        LeanTween.alpha(fadeImage.rectTransform, 0, levelLoader.animationLength);
    }

    void FadeLevelOut() {
        LeanTween.alpha(fadeImage.rectTransform, 1, levelLoader.animationLength);
    }
}
