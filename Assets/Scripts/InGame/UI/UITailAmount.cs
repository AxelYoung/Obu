using UnityEngine;
using UnityEngine.UI;

public class UITailAmount : MonoBehaviour {

    [SerializeField] float textFadeBuffer = 2;
    Player player;
    Text tailAmountText;
    bool textFaded = true;
    float textFadeCurrentTime = 0;

    LevelLoader levelLoader;

    void Awake() {
        levelLoader = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<LevelLoader>();
        levelLoader.LevelLoaded += StartLevelAnimation;
        levelLoader.LevelExit += ExitLevelAnimation;
    }

    void StartLevelAnimation() {
        tailAmountText = GetComponent<Text>();
        player = levelLoader.player;
        tailAmountText.color = new Color(1, 1, 1, 0);
        FadeText();
    }

    void Update() {
        if (player != null) UpdateTailText();
    }

    void UpdateTailText() {
        int remainingTiles = player.tailGeneration.maxTailLength - player.tailGeneration.tailTiles.Count;
        if (remainingTiles <= 0) {
            if (!textFaded) {
                textFadeCurrentTime += Time.deltaTime;
                if (textFadeCurrentTime >= textFadeBuffer) {
                    FadeText();
                }
            }
        } else {
            if (textFaded) {
                FadeText();
            }
        }
        tailAmountText.text = remainingTiles.ToString();
    }

    void FadeText() {
        LeanTween.alphaText(tailAmountText.rectTransform, textFaded ? 1 : 0, levelLoader.animationLength);
        textFaded = !textFaded;
    }

    void ExitLevelAnimation() {
        if (!textFaded) {
            FadeText();
        }
    }
}
