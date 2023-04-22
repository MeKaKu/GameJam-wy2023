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

public sealed partial class Npc :  Bright.Config.BeanBase 
{
    public Npc(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Name = _buf.ReadString();
        Des = _buf.ReadString();
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);Dialogs = new System.Collections.Generic.List<NpcDialog>(n);for(var i = 0 ; i < n ; i++) { NpcDialog _e;  _e = NpcDialog.DeserializeNpcDialog(_buf); Dialogs.Add(_e);}}
        PostInit();
    }

    public static Npc DeserializeNpc(ByteBuf _buf)
    {
        return new Npc(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// npc名字
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Des { get; private set; }
    /// <summary>
    /// 对话
    /// </summary>
    public System.Collections.Generic.List<NpcDialog> Dialogs { get; private set; }

    public const int __ID__ = 78529;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var _e in Dialogs) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var _e in Dialogs) { _e?.TranslateText(translator); }
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "Des:" + Des + ","
        + "Dialogs:" + Bright.Common.StringUtil.CollectionToString(Dialogs) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}