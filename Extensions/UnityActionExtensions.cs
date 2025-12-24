using System.Runtime.CompilerServices;
using UnityEngine.Events;
namespace VV.Utility
{
    public static class UnityActionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddListenerOnce(this UnityAction thisAction, UnityAction action)
        {
            thisAction += RegisterOnceCallback;
            return;

            void RegisterOnceCallback()
            {
                action?.Invoke();
                thisAction -= RegisterOnceCallback;
            }
        }
    }
}