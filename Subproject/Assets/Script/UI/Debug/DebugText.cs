using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    public Text TextDebug;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UserDataManager.UserDataManager UserDataInstance = UserDataManager.UserDataManager.GetInstance();
        if (UserDataInstance == null)
        {
            Debug.Log("UserDataManager GetInstance error");
            return;
        }
        TextDebug.text = UserDataInstance.GetUserId() + "\n";
        UIManager.UIManager UIInstance = UIManager.UIManager.GetInstance();
        if (UIInstance == null)
        {
            Debug.Log("UIManager GetInstance error");
            return;
        }
        TextDebug.text += UIInstance.GetDebugString() + "\n";
    }
}
