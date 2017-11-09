
using System;

namespace GoA.App.v3.Domain
{
    public static class NobaseExtension
    {
        public static BaseNode When<T>(this BaseNode root, Action action) where T : BaseNode
        {
            if (root.GetType() == typeof(T))
            {
                action();
            }
            return root;
        }
    }
}
