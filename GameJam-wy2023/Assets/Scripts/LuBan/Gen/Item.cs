//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;



namespace Dye
{

public sealed partial class Item :  Bright.Config.BeanBase 
{
    public Item(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Type = (ItemType)_buf.ReadInt();
        Name = _buf.ReadString();
        Desc = _buf.ReadString();
        Usable = _buf.ReadBool();
        UseOnBackpack = _buf.ReadBool();
        PostInit();
    }

    public static Item DeserializeItem(ByteBuf _buf)
    {
        return new Item(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 物品类型
    /// </summary>
    public ItemType Type { get; private set; }
    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Desc { get; private set; }
    /// <summary>
    /// 是否可使用
    /// </summary>
    public bool Usable { get; private set; }
    /// <summary>
    /// 是否在背包中使用
    /// </summary>
    public bool UseOnBackpack { get; private set; }

    public const int __ID__ = 2289459;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Type:" + Type + ","
        + "Name:" + Name + ","
        + "Desc:" + Desc + ","
        + "Usable:" + Usable + ","
        + "UseOnBackpack:" + UseOnBackpack + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
