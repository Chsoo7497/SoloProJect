using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PopupManager
{
    public enum EPopupContent
    {
        LogIn,
        CreateId,
        Notice
    }
    public enum EOnClickState
    {
        Close,
        Yes,
        No
    }
    public class PopupData
    {
        public GameObject Popup;
        public GameObject Content;
        public UnityEvent<EOnClickState> OnClick;
    }


    public class PopupManager
    {
        private static PopupManager Instance = null;

        public static PopupManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new PopupManager();
                Instance.Init();
            }
            return Instance;
        }

        private GameObject Popups;
        public void Init()
        {
        }

        //null 시 팝업 구현 실패
        //PopupData.Content?.GetComponent<EPopupContent의 스크립트>를 받아와 설정해줌 외부에서 팝업 설정 가능
        //PopupData.OnClick?을 통해 버튼 클릭에 따른 동작 설정 가능
        public PopupData CreatePopup(string Title, string Yes, string No, Transform ParentTransform, EPopupContent Content)
        {
            PopupData Data;
            // Resources/Prefabs/Popup/PopupUI.prefab 로드
            GameObject PopupPrefab = Resources.Load("Prefabs/Popup/PopupUI", typeof(GameObject)) as GameObject;
            if (PopupPrefab == null)
            {
                Debug.Log("Popup prefab Load error");
                return null;
            }
            GameObject Popup = MonoBehaviour.Instantiate(PopupPrefab, ParentTransform) as GameObject;
            if (Popup == null)
            {
                Debug.Log("Popup prefab Instantiate error");
                return null;
            }

            // 콘텐츠 prefab 로드
            GameObject ContentPrefab = Resources.Load(GetContentPath(Content), typeof(GameObject)) as GameObject;
            if (ContentPrefab == null)
            {
                Debug.Log("Content prefab Load error");
                return null;
            }

            PopupDefault popupDefault = Popup.GetComponent<PopupDefault>();
            if (popupDefault == null)
            {
                Debug.Log("GetComponent popupDefault error");
                return null;
            }
            Data = popupDefault.SetPopup(Title, Yes, No, ContentPrefab);
            if (Data == null)
            {
                Debug.Log("popupDefault SetPopup error");
                MonoBehaviour.Destroy(Popup);
            }
            Data.Popup = Popup;
            Data.OnClick.AddListener((ClickState) =>
            {
                switch (ClickState)
                {
                    case EOnClickState.Close:
                        MonoBehaviour.Destroy(Popup);
                        break;
                    case EOnClickState.Yes:
                        MonoBehaviour.Destroy(Popup);
                        break;
                    case EOnClickState.No:
                        MonoBehaviour.Destroy(Popup);
                        break;
                }
            });
            Popup.SetActive(true);
            return Data;
        }

        public string GetContentPath(EPopupContent Content)
        {
            switch (Content)
            {
                case EPopupContent.LogIn:
                    return "Prefabs/Popup/Content/LogIn";
                case EPopupContent.CreateId:
                    return "Prefabs/Popup/Content/CreateId";
                case EPopupContent.Notice:
                    return "Prefabs/Popup/Content/Notice";
                default:
                    return null;
            }
        }
        
    }
}