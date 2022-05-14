using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class LevelMenu : MonoBehaviour {

    public GameObject levelMenuObject;
    public Transform levelMenuParent;

    public Dictionary<string, GameObject> levelMenuItems = new Dictionary<string, GameObject>();

    public RawImage fadeImage;

    bool fading = false;

    void Start() {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        LeanTween.alpha(fadeImage.rectTransform, 0, 1);
        GetAllLevelJSONs();
    }

    void GetAllLevelJSONs() {
        DirectoryInfo dir = new DirectoryInfo(FileFunctions.GetPath(""));
        FileInfo[] info = dir.GetFiles("*.json").OrderBy(p => p.LastWriteTime).ToArray();
        Array.Reverse(info);
        foreach (FileInfo file in info) {
            string levelName = file.Name;
            LevelInMenu currentMenu = Instantiate(levelMenuObject, levelMenuParent).GetComponent<LevelInMenu>();
            currentMenu.startLevel.onClick.AddListener(delegate { LoadScene(levelName, "LevelLoader"); });
            currentMenu.editLevel.onClick.AddListener(delegate { LoadScene(levelName, "LevelEditor"); });
            currentMenu.deleteLevel.onClick.AddListener(delegate { DeleteLevel(levelName); });
            currentMenu.nameText.text = levelName.Substring(0, levelName.Length - 5);
            levelMenuItems.Add(levelName, currentMenu.gameObject);
            Destroy(currentMenu);
        }
    }

    void LoadScene(string levelName, string sceneName) {
        if (!fading) {
            LevelIDs.levelName = levelName;
            LeanTween.alpha(fadeImage.rectTransform, 1, 1).setOnComplete(() => SceneManager.LoadScene(sceneName));
            fading = true;
        }
    }

    public void LoadEmptyLevelEditor() {
        LevelIDs.levelName = "";
        SceneManager.LoadScene("LevelEditor");
    }

    public void DeleteLevel(string levelName) {
        File.Delete(FileFunctions.GetPath(levelName));
        Destroy(levelMenuItems[levelName]);
    }

}