using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager manager;

    public GameObject winCanvas;

    void Start () {
        GameManager.manager = this;
    }

    public void Win () {
        winCanvas.SetActive(true);
    }

    public void ReturnToMenu () {
        SceneManager.LoadScene("menu");
    }

    public void Restart () {
        SceneManager.LoadScene("game");
    }
}
