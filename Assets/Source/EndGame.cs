using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public bool dead;
    public Text text;

    void Update() {
        if (dead) {
            if (Input.GetButtonDown("Roll")) {
                Application.LoadLevel("menu");
            }
            else if (Input.GetButtonDown("Jump")) {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (text != null && text.text == "100") {
            Application.LoadLevel(3);
        }
        
    }
}
