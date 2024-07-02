using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PopupData : MonoBehaviour
{
    public enum EOnClickState
    {
        Yes,
        No,
        Close
    }

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
    public UnityEvent<EOnClickState> OnClick;


    public bool SetPopup(string Title, string Yes, string No, GameObject ContentPrefab, UnityAction<EOnClickState> OnClickEvent)
    {
        if (Title == null)
        {
            TextTitle.gameObject.SetActive(false);
        }
        if (Yes == null)
        {
            ButtonYes.gameObject.SetActive(false);
        }
        if (No == null)
        {
            ButtonYes.gameObject.SetActive(false);
        }
        TextTitle.text = Title;
        TextYes.text = Yes;
        TextNo.text = No;
        OnClick.AddListener(OnClickEvent);

        ContentUI = MonoBehaviour.Instantiate(ContentPrefab, ContentAria.transform) as GameObject;
        if (ContentUI != null)
        {
            ContentUI.SetActive(true);
        }
        else
        {
            Debug.Log("Popup Content prefab Clon error");
            return false;
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ButtonClose.onClick.AddListener(OnClickedClose);
        ButtonYes.onClick.AddListener(OnClickedYes);
        ButtonNo.onClick.AddListener(OnClickedNo);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnClickedClose()
    {
        Debug.Log("Popup Close OnClick");
        OnClick.Invoke(EOnClickState.Close);
    }
    public void OnClickedYes()
    {
        Debug.Log("Popup Yes OnClick");
        OnClick.Invoke(EOnClickState.Yes);
    }
    public void OnClickedNo()
    {
        Debug.Log("Popup No OnClick");
        OnClick.Invoke(EOnClickState.No);
    }
}
