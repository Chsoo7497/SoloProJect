using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace UIManager
{
	public enum WorldScreenLayer
	{
		None

	, NameWidget        // (WorldScreen) 이름 (네임태그)
	, Interaction       // (WorldScreen) 인터렉션
	, WorldCommon       // (WorldScreen) World에 뜨는 일반컨텐츠 위젯.
	, SpeechBubble      // (WorldScreen) Npc, User 말풍선

	, End
	};
	public enum UIScreenLayer
	{
		None

	, Controller        // (UIScreen) 액션 패드 및 조이스틱.
	, HUD               // (UIScreen) 허드.
	, Chatting          // (UIScreen) 채팅
	, NPCDialog         // (UIScreen) NPC대화 위젯.
	, Common            // (UIScreen) UI에 뜨는 일반컨텐츠 위젯.
	, Slide             // (UIScreen) 
	, Popup             // (UIScreen) 
	, Completion        // (UIScreen) 업적, 퀘스트완료 등등..
	, Cinematic         // (UIScreen) 시네마틱 모드
	, Loading           // (UIScreen) 레벨 이동 시, 이동 직전에 화면을 가리는 용도의 레이어.
	, Waiting           // (UIScreen) 아이템 구매/판매, 데이터 저장 등 외부와 비동기적 처리가 필요한 경우
	, Notice            // (UIScreen) 긴급 점검 공지, 공지 사항 노출, 레벨 이동 사유 등등

	, Debug             // (UIScreen) Debug

	, End
	};
	public class UIManager
	{
		private static UIManager Instance = null;

		public static UIManager GetInstance()
		{
			if (Instance == null)
			{
				Instance = new UIManager();
				Instance.Init();
			}
			return Instance;
		}

		public Dictionary<UIScreenLayer, List<GameObject>> UIPrefabs;
		public Action<UIScreenLayer, GameObject> AddUIByLayer;
		public void Init()
		{
			UIPrefabs = new Dictionary<UIScreenLayer, List<GameObject>>();
            foreach (UIScreenLayer e in Enum.GetValues(typeof(UIScreenLayer)))
            {
				UIPrefabs.Add(e, new List<GameObject>());
			}
		}

		public GameObject CreateUI(string UIPrefabPath, Transform ParentTransform)
		{
			// UI prefab 로드
			GameObject UIPrefab = Resources.Load(UIPrefabPath, typeof(GameObject)) as GameObject;
			if (UIPrefab == null)
			{
				Debug.Log("UI prefab Load error");
				return null;
			}
			GameObject UI = MonoBehaviour.Instantiate(UIPrefab, ParentTransform) as GameObject;
			if (UI == null)
			{
				Debug.Log("UI prefab Instantiate error");
				return null;
			}
			AddUIPrefabs(UI);

			return UI;
		}

		public void AddUIPrefabs(GameObject UIObject)
		{
			UIDesc Desc = UIObject.GetComponent<UIDesc>();
			if (Desc == null)
			{
				Debug.Log("GetComponent UIDesc error");
				return;
			}
			List<GameObject> LayerObjects = new List<GameObject>();
			if (UIPrefabs.TryGetValue(Desc.LayerType, out LayerObjects))
			{
				LayerObjects.Add(UIObject);
				AddUIByLayer?.Invoke(Desc.LayerType, UIObject);
			}
		}

		public void RemoveUIPrefabs(GameObject UIObject)
		{
			UIDesc RemoveUIDesc = UIObject.GetComponent<UIDesc>();
			if (RemoveUIDesc == null)
			{
				Debug.Log("GetComponent UIDesc error");
				return;
			}
			List<GameObject> objects = new List<GameObject>();
			if (UIPrefabs.TryGetValue(RemoveUIDesc.LayerType, out objects))
			{
				foreach(var e in objects)
				{
					UIDesc Desc = e.GetComponent<UIDesc>();
					if (Desc == RemoveUIDesc)
					{
						objects.Remove(e);
						break;
					}
				}
			}
		}
		
		public void ChangeActiveUIPrefabs(List<string> OffUIs, List<string> OnUIs)
		{
			Dictionary<UIScreenLayer, List<GameObject>> Prefabs = UIPrefabs;
			foreach (var LayerUIPrefabs in Prefabs.Reverse())
			{
				List<GameObject> objects = LayerUIPrefabs.Value;
				objects.Reverse();

				foreach (var UIPrefab in objects)
				{
					UIDesc Desc = UIPrefab.GetComponent<UIDesc>();
					foreach (var OffUI in OffUIs)
					{
						if (OffUI == Desc.name)
						{
							UIPrefab.SetActive(false);
						}
					}
					foreach (var OnUI in OnUIs)
					{
						if (OnUI == Desc.name)
						{
							UIPrefab.SetActive(true);
						}
					}
				}
			}
		}
		public string GetUIPrefabPath(string UIPrefabName)
		{
			return null;
		}
		public string GetDebugString()
		{
			string DebugString = "";
			Dictionary<UIScreenLayer, List<GameObject>> Prefabs = UIPrefabs;
			foreach (var LayerUIPrefabs in Prefabs.Reverse())
			{
				List<GameObject> ObjectList = LayerUIPrefabs.Value;
				if(ObjectList.Count == 0)
                {
					continue;
                }
				DebugString += LayerUIPrefabs.Key.ToString() + "\n";
				ObjectList.Reverse();

				foreach (var UIPrefab in ObjectList)
				{
					UIDesc Desc = UIPrefab.GetComponent<UIDesc>();
					bool Active = UIPrefab.activeSelf;
					DebugString += Desc.UIPrefabName +"   " + Active.ToString()+ "\n";

				}
			}
			return DebugString;
		}
	}
}