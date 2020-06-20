using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour{
    public void Awake(){
        GameObject bgmObj = GameObject.Find("BGM");
        bgmObj.GetComponent<AudioSource>().volume = StaticValues.VOLUME_BGM;
    }

    public void StartGame(){
        SceneManager.LoadScene("Main");
    }

    public void MainMenu(){
        GameObject bgmObj = GameObject.Find("BGM");
        Destroy(bgmObj);
        SceneManager.LoadScene("Main Menu");
    }

    public void CaraBermain(){
        SceneManager.LoadScene("Cara Bermain");
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void SettingMenu(){
        SceneManager.LoadScene("Settings");
    }
}
