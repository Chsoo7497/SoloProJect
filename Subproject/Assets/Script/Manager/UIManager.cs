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

	, NameWidget        // (WorldScreen) �̸� (�����±�)
	, Interaction       // (WorldScreen) ���ͷ���
	, WorldCommon       // (WorldScreen) World�� �ߴ� �Ϲ������� ����.
	, SpeechBubble      // (WorldScreen) Npc, User ��ǳ��

	, End
	};
	public enum UIScreenLayer
	{
		None

	, Controller        // (UIScreen) �׼� �е� �� ���̽�ƽ.
	, HUD               // (UIScreen) ���.
	, Chatting          // (UIScreen) ä��
	, NPCDialog         // (UIScreen) NPC��ȭ ����.
	, Common            // (UIScreen) UI�� �ߴ� �Ϲ������� ����.
	, Slide             // (UIScreen) 
	, Popup             // (UIScreen) 
	, Completion        // (UIScreen) ����, ����Ʈ�Ϸ� ���..
	, Cinematic         // (UIScreen) �ó׸�ƽ ���
	, Loading           // (UIScreen) ���� �̵� ��, �̵� ������ ȭ���� ������ �뵵�� ���̾�.
	, Waiting           // (UIScreen) ������ ����/�Ǹ�, ������ ���� �� �ܺο� �񵿱��� ó���� �ʿ��� ���
	, Notice            // (UIScreen) ��� ���� ����, ���� ���� ����, ���� �̵� ���� ���

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
			// UI prefab �ε�
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