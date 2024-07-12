using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleHUD: MonoBehaviour
{
    public Text TitleText;
    public Text StartText;
    public Text QuitText;

    // Start is called before the first frame update
    void Start()
    {
        TitleText.text = "Title";
        StartText.text = "Start";
        QuitText.text = "Quit";
    }

    // Update is called once per frame
    void Update()
    { 

    }
    private void OnDestroy()
    {
    }
    public void OnClickStart()
    {
        UserDataManager.UserDataManager Instance = UserDataManager.UserDataManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UserDataManager GetInstance error");
            return;
        }
        if (Instance.UserData != null)
        {
            SceneManager.LoadScene("MainScene");
            return;
        }
    }
    public void OnClickQuit()
    {
        UserDataManager.UserDataManager Instance = UserDataManager.UserDataManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UserDataManager GetInstance error");
            return;
        }
        Instance.LogOut();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
