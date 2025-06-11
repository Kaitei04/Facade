using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    private string namaSceneGame = "Introduction";
    [SerializeField] Button play;
    [SerializeField] Button exit;

    public void Start()
    {
        play.onClick.AddListener(TombolPlay);
        exit.onClick.AddListener(TombolExit);
    }


    public void TombolPlay()
    {
        SceneManager.LoadScene(namaSceneGame);
    }

    
    public void TombolExit()
    {
        Application.Quit();
        Debug.Log("Keluar dari game (hanya akan bekerja di build)");
    }
}
