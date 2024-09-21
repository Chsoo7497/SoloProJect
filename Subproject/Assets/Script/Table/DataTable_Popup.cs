using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataTable_PopupData
{
    public int ID;
    public string Title;
    public string Yes;
    public string No;
    public string EPopupContent;
    public string Path;
}

public class DataTable_Popup : DataTableBase<DataTable_PopupData>
{
    protected static DataTable_Popup Instance;
    public static DataTable_Popup GetInstance()
    {
        if (Instance == null)
        {
            Instance = new DataTable_Popup();
            Instance.Init();
        }
        return Instance;
    }
    public override void Init()
    {
        JsonName = "Popup";
        base.Init();
    }
    public DataTable_PopupData FindPopup_Data(int _ID)
    {
        foreach (DataTable_PopupData Data in DataTable_Data)
        {
            if (Data.ID == _ID)
            {
                return Data;
            }
        }
        return null;
    }
    public GameObject GetContentPrefab(int _ID)
    {
        DataTable_PopupData Data = FindPopup_Data(_ID);

        if (Data != null)
        {
            return Resources.Load(Data.Path) as GameObject;
        }

        return null;
    }
}
