
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UserDataManager;

public class PopupContentLogIn : MonoBehaviour
{
    [Header("InputField")]
    [SerializeField] 
    public InputField IdField;
    [SerializeField]
    public InputField PassWardField;
    [SerializeField]
    public InputField NickNameField;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void CreateID()
    {
        UserDataManager.UserDataManager Instance = UserDataManager.UserDataManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UserDataManager GetInstance error");
            return;
        }
        Instance.CreateUser(IdField.text.Trim(), PassWardField.text.Trim(), NickNameField.text.Trim());
    }
    public void LogIn()
    {
        UserDataManager.UserDataManager Instance = UserDataManager.UserDataManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UserDataManager GetInstance error");
            return;
        }
        Instance.LogIn(IdField.text.Trim(), PassWardField.text.Trim());
    }
    public void LogOut()
    {
        UserDataManager.UserDataManager Instance = UserDataManager.UserDataManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UserDataManager GetInstance error");
            return;
        }
        Instance.LogOut();
    }
}
