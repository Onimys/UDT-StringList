using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

[Serializable]
[SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000, IsFixedLength = false)]
public class StringList : INullable, IBinarySerialize
{
    private bool is_Null;
    private List<string> _list;
    private char[] _separator;

    public StringList()
    {
        _list = new List<string>();
        _separator = new char[] { '|' };
    }

    /// <summary>
    /// Empty type
    /// </summary>
    public static StringList Null
    {
        get
        {
            StringList sl = new StringList();
            sl.is_Null = true;
            return sl;
        }
    }

    public List<string> List
    {
        get
        {
            return _list;
        }
        set
        {
            _list = value;
        }
    }

    /// <summary>
    /// On ms sql SET
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [SqlMethod(OnNullCall = false)]
    public static StringList Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        StringList sl = new StringList();
        string[] items = s.Value.Split(sl._separator);
        foreach (string item in items)
        {
            sl.List.Add(item);
        }
        return sl;
    }

    /// <summary>
    /// Add new item in list
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    [SqlMethod(InvokeIfReceiverIsNull = true)]
    public StringList AddItem(string item)
    {
        _list.Add(item ?? "");
        if (is_Null)
            is_Null = false;
        return this;
    }

    /// <summary>
    /// Add new items in list (with delimiter)
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    [SqlMethod(InvokeIfReceiverIsNull = true)]
    public StringList AddItems(string items)
    {
        items = items ?? "";
        string[] el = items.Split(_separator);
        foreach (string item in el)
        {
            _list.Add(item);
        }
        if (is_Null)
            is_Null = false;
        return this;
    }

    /// <summary>
    /// Remove item from list
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    [SqlMethod(OnNullCall = true, InvokeIfReceiverIsNull = true)]
    public StringList RemoveItem(SqlInt32 index)
    {
        if (IsNull || index > _list.Count - 1 || index < 0 || index.IsNull)
            throw new ArgumentOutOfRangeException("Invalid index");
        _list.RemoveAt(index.Value);
        if (_list.Count == 0)
            is_Null = true;
        return this;
    }

    /// <summary>
    /// Remove all items from list
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    [SqlMethod(InvokeIfReceiverIsNull = true)]
    public StringList RemoveAll()
    {
        _list.Clear();
        is_Null = true;
        return this;
    }

    /// <summary>
    /// Get item by index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [SqlMethod(OnNullCall = false)]
    public string GetItem(int index)
    {
        if (IsNull || index > _list.Count - 1 || index < 0)
            return "NULL";
        return _list[index];
    }

    /// <summary>
    /// Format message for list
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [SqlMethod(OnNullCall = false, InvokeIfReceiverIsNull = true)]
    public string Formated(string message)
    {
        return FormatMessage(message, this);
    }

    /// <summary>
    /// Format message for other list
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [SqlMethod(OnNullCall = false, InvokeIfReceiverIsNull = true)]
    public static string FormatMessage(string message, StringList StringList)
    {
        foreach (Match m in Regex.Matches(message, @"{\d{1,3}}"))
        {
             string replace = StringList.GetItem(Int32.Parse(Regex.Replace(m.Value, @"({|})", "")));

             message = message.Replace(m.Value, (replace != "NULL") ? replace : "");
        }

        return message;
    }


    /// <summary>
    /// Check for Null
    /// </summary>
    /// <returns></returns>
    [SqlMethod(OnNullCall = false, InvokeIfReceiverIsNull = true)]
    public bool isEmpty()
    {
        return IsNull;
    }

    [SqlMethod(InvokeIfReceiverIsNull = true)]
    public override string ToString()
    {
        return Concat();
    }

    /// <summary>
    /// ToString with delimiter
    /// </summary>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    [SqlMethod(OnNullCall = false)]
    public string Concat(string delimiter = "|")
    {
        if (IsNull)
            return "NULL";

        return String.Join(delimiter, _list.ToArray());
    }

    /// <summary>
    /// Items count in list
    /// </summary>
    /// <returns></returns>
    [SqlMethod(InvokeIfReceiverIsNull = true)]
    public Int32 Length()
    {
        return _list.Count;
    }


    #region IBinarySerialize
    public void Read(System.IO.BinaryReader r)
    {
        int itemCount = r.ReadInt32();
        _list = new List<string>(itemCount);

        for (int i = 0; i < itemCount; i++)
        {
            this._list.Add(r.ReadString());
        }
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(_list.Count);
        foreach (string s in _list)
        {
            w.Write(s);
        }
    }
    #endregion

    #region INullable
    public bool IsNull
    {
        get
        {
            return is_Null;
        }
    }
    #endregion

}
