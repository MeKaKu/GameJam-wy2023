//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;


namespace Dye
{
   
public partial class Configs
{
    public TbItem TbItem {get; }

    public Configs(System.Func<string, ByteBuf> loader)
    {
        var tables = new System.Collections.Generic.Dictionary<string, object>();
        TbItem = new TbItem(loader("tbitem")); 
        tables.Add("TbItem", TbItem);

        PostInit();
        TbItem.Resolve(tables); 
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        TbItem.TranslateText(translator); 
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}