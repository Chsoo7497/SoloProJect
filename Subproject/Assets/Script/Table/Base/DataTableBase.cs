using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class DataTableBase<T_Data>
{

    protected string JsonName;
    protected List<T_Data> DataTable_Data;

    public virtual void Init()
    {
        ReadDataTable();
    }
    public void ReadDataTable()
    {
        string Path = "JsonTable/" + JsonName; 
        TextAsset Json = Resources.Load<TextAsset>(Path);
        JArray JsonTable = JArray.Parse(Json.ToString());
        DataTable_Data = JsonTable.ToObject<List<T_Data>>();
    }
}