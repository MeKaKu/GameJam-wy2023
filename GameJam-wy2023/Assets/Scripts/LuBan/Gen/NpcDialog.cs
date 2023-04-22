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

/// <summary>
/// ncp对话
/// </summary>
public sealed partial class NpcDialog :  Bright.Config.BeanBase 
{
    public NpcDialog(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Type = _buf.ReadString();
        Next = _buf.ReadInt();
        Content = _buf.ReadString();
        Condition = _buf.ReadString();
        Effect = _buf.ReadString();
        PostInit();
    }

    public static NpcDialog DeserializeNpcDialog(ByteBuf _buf)
    {
        return new NpcDialog(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; private set; }
    /// <summary>
    /// 跳转
    /// </summary>
    public int Next { get; private set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; private set; }
    /// <summary>
    /// 条件
    /// </summary>
    public string Condition { get; private set; }
    /// <summary>
    /// 效果
    /// </summary>
    public string Effect { get; private set; }

    public const int __ID__ = -1905965207;
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
        + "Next:" + Next + ","
        + "Content:" + Content + ","
        + "Condition:" + Condition + ","
        + "Effect:" + Effect + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}