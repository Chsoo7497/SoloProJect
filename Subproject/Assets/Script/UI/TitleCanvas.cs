using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleCanvas : MonoBehaviour
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
    public void OnClickStart()
    {
        UserDataManager.UserDataManager Instance = UserDataManager.UserDataManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UserDataManager GetInstance error");
            return;
        }
        if(Instance.UserData != null)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
