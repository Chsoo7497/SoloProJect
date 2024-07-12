using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIManager;
using System;

public class UIController : MonoBehaviour
{
    public Canvas MainCanvas;
    public GameObject[] DefultUI;
    Dictionary<UIScreenLayer, GameObject> UILayers;
    // Start is called before the first frame update
    void Start()
    {
        UILayers = new Dictionary<UIScreenLayer, GameObject>();
        foreach (UIScreenLayer Layer in Enum.GetValues(typeof(UIScreenLayer)))
        {
            if(Layer != UIScreenLayer.None && Layer != UIScreenLayer.End)
            {
                GameObject UILayer = new GameObject(Layer.ToString());
                UILayer.transform.SetParent(MainCanvas.transform);
                RectTransform LayerRect = UILayer.AddComponent<RectTransform>();
                LayerRect.anchorMin = new Vector2(0, 0);
                LayerRect.anchorMax = new Vector2(1, 1);
                LayerRect.pivot = new Vector2(0.5f, 0.5f);
                LayerRect.offsetMin = new Vector2(0, 0);
                LayerRect.offsetMax = new Vector2(0, 0);
                UILayers.Add(Layer, UILayer);
            }
        }

        UIManager.UIManager Instance = UIManager.UIManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UIManager GetInstance error");
            return;
        }
        Instance.ChangeActiveUI += ChangeActiveUI;
        StartDefultUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartDefultUI()
    {
        foreach(GameObject UIPrefab in DefultUI)
        {
            GameObject UI = MonoBehaviour.Instantiate(UIPrefab) as GameObject;
        }
    }

    void ChangeActiveUI()
    {

        UIManager.UIManager Instance = UIManager.UIManager.GetInstance();
        if (Instance == null)
        {
            Debug.Log("UIManager GetInstance error");
            return;
        }
        foreach(var UIPrefabLayer in Instance.UIPrefabs)
        {
            foreach(var UIPrefab in UIPrefabLayer.Value)
            {
                UIPrefab.transform.SetParent(UILayers[UIPrefabLayer.Key].transform, false);
            }
        }
    }
}
