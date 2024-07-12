using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UserDataManager.UserDataManager Instance = UserDataManager.UserDataManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UserDataManager GetInstance error");
            return;
        }
        Instance.AsyncError += CreateNoticePopup;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        UserDataManager.UserDataManager Instance = UserDataManager.UserDataManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UserDataManager GetInstance error");
            return;
        }
        Instance.AsyncError -= CreateNoticePopup;
    }
    public void CreateLogInPopup()
    {
        UserDataManager.UserDataManager UserDataInstance = UserDataManager.UserDataManager.GetInstance();
        if (UserDataInstance == null)
        {
            Debug.Log("UserDataManager GetInstance error");
            return;
        }
        if (UserDataInstance.UserData != null)
        {
            return;
        }
        PopupManager.PopupManager Instance = PopupManager.PopupManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("PopupManager GetInstance error");
            return;
        }

        PopupManager.PopupData Data = Instance.CreatePopup(1);
        if (Data == null)
        {
            Debug.Log("PopupManager CreatePopup error");
            return;
        }
        Data.OnClick?.AddListener((ClickState) =>
        {
            PopupContentLogIn ContentLogIn = Data.Content?.GetComponent<PopupContentLogIn>();
            switch (ClickState)
            {
                case PopupManager.EOnClickState.Close:
                    break;
                case PopupManager.EOnClickState.Yes:
                    CreateCreateIdPopup();
                    break;
                case PopupManager.EOnClickState.No:
                    ContentLogIn?.LogIn();
                    break;
            }
        });
    }
    public void CreateCreateIdPopup()
    {
        PopupManager.PopupManager Instance = PopupManager.PopupManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("PopupManager GetInstance error");
            return;
        }
        PopupManager.PopupData Data = Instance.CreatePopup(2);
        if (Data == null)
        {
            Debug.Log("PopupManager CreatePopup error");
            return;
        }
        Data.OnClick?.AddListener((ClickState) =>
        {
            PopupContentLogIn ContentLogIn = Data.Content?.GetComponent<PopupContentLogIn>();
            switch (ClickState)
            {
                case PopupManager.EOnClickState.Close:
                    break;
                case PopupManager.EOnClickState.Yes:
                    break;
                case PopupManager.EOnClickState.No:
                    ContentLogIn?.CreateID();
                    break;
            }
        });
    }
    public void CreateNoticePopup(string Notice)
    {
        Debug.Log(Notice);
        PopupManager.PopupManager Instance = PopupManager.PopupManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("PopupManager GetInstance error");
            return;
        }
        PopupManager.PopupData Data = Instance.CreatePopup(3);
        if (Data == null)
        {
            Debug.Log("PopupManager CreatePopup error");
            return;
        }

        PopupContentNotice ContentNotice = Data.Content?.GetComponent<PopupContentNotice>();
        if (ContentNotice == null)
        {
            Debug.Log("ContentNotice error");
            return;
        }
        ContentNotice?.SetNotice(Notice);
    }
}
