using System;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.AssetBundles.Runtime
{
    public abstract class AssetBundleStorage<TInfo> : AssetBundleStorageBase where TInfo : IAssetInfo
    {
        protected override string OnGetAddress(IAssetInfo info, GlobalId id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            return OnGetAddress((TInfo)info, id, type, parameters, context);
        }

        protected abstract string OnGetAddress(TInfo info, GlobalId id, Type type, IAssetLoadParameters parameters, IContext context);
    }
}
