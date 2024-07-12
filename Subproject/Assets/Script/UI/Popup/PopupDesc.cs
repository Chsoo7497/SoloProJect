using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using PopupManager;

public class PopupDesc : MonoBehaviour
{

    [Header("Text")]
    public Text TextTitle;
    public Text TextYes;
    public Text TextNo;

    [Header("Button")]
    public Button ButtonClose;
    public Button ButtonYes;
    public Button ButtonNo;

    [Header("Content")]
    public GameObject ContentAria;
    private GameObject ContentUI;

    [Header("OnClick")]
    public UnityEvent<PopupManager.EOnClickState> OnClick;
    public PopupManager.PopupData SetPopup(string Title, string Yes, string No, GameObject ContentPrefab)
    {
        if (Title == "")
        {
            TextTitle.gameObject.SetActive(false);
        }
        if (Yes == "")
        {
            ButtonYes.gameObject.SetActive(false);
        }
        if (No == "")
        {
            ButtonNo.gameObject.SetActive(false);
        }
        TextTitle.text = Title;
        TextYes.text = Yes;
        TextNo.text = No;
        ContentUI = MonoBehaviour.Instantiate(ContentPrefab, ContentAria.transform) as GameObject;
        if (ContentUI != null)
        {
            ContentUI.SetActive(true);
        }
        else
        {
            Debug.Log("Popup Content prefab Instantiate error");
            return null;
        }
        PopupManager.PopupData Data = new PopupManager.PopupData();
        Data.Popup = gameObject;
        Data.Content = ContentUI;
        Data.OnClick = OnClick;
        return Data;
    }

    // Start is called before the first frame update
    void Start()
    {
        ButtonClose.onClick.AddListener(OnClickedClose);
        ButtonYes.onClick.AddListener(OnClickedYes);
        ButtonNo.onClick.AddListener(OnClickedNo);
        OnClick.AddListener((ClickState) =>
        {
            MonoBehaviour.Destroy(gameObject);
            switch (ClickState)
            {
                case EOnClickState.Close:
                    break;
                case EOnClickState.Yes:
                    break;
                case EOnClickState.No:
                    break;
                default:
                    break;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnClickedClose()
    {
        Debug.Log("Popup Close OnClick");
        OnClick?.Invoke(PopupManager.EOnClickState.Close);
    }
    public void OnClickedYes()
    {
        Debug.Log("Popup Yes OnClick");
        OnClick?.Invoke(PopupManager.EOnClickState.Yes);
    }
    public void OnClickedNo()
    {
        Debug.Log("Popup No OnClick");
        OnClick?.Invoke(PopupManager.EOnClickState.No);
    }
}
