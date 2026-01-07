using UnityEngine.UIElements;

namespace VV.UIToolkitVisualScripting
{
    public static class UIToolkitEventExtensions
    {
        public static VisualElement GetTarget(this EventBase evt)
        {
            return evt.target as VisualElement;
        }
    }
}