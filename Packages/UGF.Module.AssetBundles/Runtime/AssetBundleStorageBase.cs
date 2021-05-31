using System;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.AssetBundles.Runtime
{
    public abstract class AssetBundleStorageBase : IAssetBundleStorage
    {
        public string GetAddress(IAssetInfo info, string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnGetAddress(info, id, type, parameters, context);
        }

        protected abstract string OnGetAddress(IAssetInfo info, string id, Type type, IAssetLoadParameters parameters, IContext context);
    }
}
