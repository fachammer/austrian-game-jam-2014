using UnityEngine;
using System.Collections;

public class SceneHandler : MonoBehaviour {

    public void changeScene(int sceneIndex) {
        Application.LoadLevel(sceneIndex);
    }

    public void changeScene(string sceneName) {
        Application.LoadLevel(sceneName);
    }

    public void reloadScene() {
        Application.LoadLevel(Application.loadedLevel);
    }

}
