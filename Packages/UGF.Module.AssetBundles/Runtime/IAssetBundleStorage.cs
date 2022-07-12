using System;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.AssetBundles.Runtime
{
    public interface IAssetBundleStorage
    {
        string GetAddress(IAssetInfo info, GlobalId id, Type type, IAssetLoadParameters parameters, IContext context);
    }
}
