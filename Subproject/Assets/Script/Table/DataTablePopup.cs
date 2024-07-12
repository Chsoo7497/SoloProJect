using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataTablePopup_Data
{
    public int ID;
    public string Title;
    public string Yes;
    public string No;
    public string EPopupContent;
    public string Path;
}

public class DataTablePopup : DataTableBase<DataTablePopup_Data>
{
    protected static DataTablePopup Instance;
    public static DataTablePopup GetInstance()
    {
        if (Instance == null)
        {
            Instance = new DataTablePopup();
            Instance.Init();
        }
        return Instance;
    }
    public override void Init()
    {
        JsonName = "Popup";
        base.Init();
    }
    public DataTablePopup_Data FindPopup_Data(int _ID)
    {
        foreach (DataTablePopup_Data Data in DataTable_Data)
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
        DataTablePopup_Data Data = FindPopup_Data(_ID);

        if (Data != null)
        {
            return Resources.Load(Data.Path) as GameObject;
        }

        return null;
    }
}
