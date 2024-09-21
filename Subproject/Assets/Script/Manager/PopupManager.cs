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
        public PopupData CreatePopup(int ID)
        {
            // Resources/Prefabs/Popup/PopupUI.prefab 로드
            GameObject PopupPrefab = Resources.Load<GameObject>("Prefabs/UI/Popup/PopupUI");
            if (PopupPrefab == null)
            {
                Debug.Log("Popup prefab Load error");
                return null;
            }
            GameObject Popup = MonoBehaviour.Instantiate(PopupPrefab) as GameObject;
            if (Popup == null)
            {
                Debug.Log("Popup prefab Instantiate error");
                return null;
            }
            DataTable_Popup PopupTable = DataTable_Popup.GetInstance();
            if (PopupTable == null)
            {
                Debug.Log("PopupTable GetInstance error");
                return null;
            }
            // 콘텐츠 prefab 로드
            GameObject ContentPrefab = PopupTable.GetContentPrefab(ID);
            if (ContentPrefab == null)
            {
                Debug.Log("Content prefab Load error");
                return null;
            }

            DataTable_PopupData PopupTableData = PopupTable.FindPopup_Data(ID);
            if (PopupTableData == null)
            {
                Debug.Log("popup_Data Find error");
                return null;
            }

            PopupDesc Desc = Popup.GetComponent<PopupDesc>();
            if (Desc == null)
            {
                Debug.Log("GetComponent PopupDesc error");
                return null;
            }
            PopupData Data = Desc.SetPopup(PopupTableData.Title, PopupTableData.Yes, PopupTableData.No, ContentPrefab);
            if (Data == null)
            {
                Debug.Log("popupDefault SetPopup error");
                MonoBehaviour.Destroy(Popup);
                return null;
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
    }
}