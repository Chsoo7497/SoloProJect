using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDesc : MonoBehaviour
{
    public UIManager.UIScreenLayer LayerType = UIManager.UIScreenLayer.None;
    public string UIPrefabPath;
    public string UIPrefabName;
    //public GameObject UIPrefabSelf;
    // Start is called before the first frame update
    void Start()
    {
        AddUIManager();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        RemoveUIManager();
    }

    private void AddUIManager()
    {
        UIManager.UIManager Instance = UIManager.UIManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UIManager GetInstance error");
            return;
        }
        Instance.AddUIPrefabs(gameObject);
    }
    private void RemoveUIManager()
    {
        UIManager.UIManager Instance = UIManager.UIManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UIManager GetInstance error");
            return;
        }
        Instance.RemoveUIPrefabs(gameObject);
    }
}
