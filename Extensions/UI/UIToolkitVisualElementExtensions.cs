using Unity.VisualScripting;

using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

namespace VV.UIToolkitVisualScripting
{
    /// <summary>
    /// A collection of common actions performed on visual elements.
    /// Of possible the return type is VisualElement to support easy chaining of nodes.
    /// </summary>
    public static partial class UIToolkitVisualElementExtensions
    {
        // V I S U A L   E L E M E N T

        #region Enabled
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetEnabled(this VisualElement element) => element.enabledSelf;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SetEnabled(this VisualElement element, bool enable) => 
            element.enabledSelf = enable;
        
        #endregion Enabled

        #region Name
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetName(this VisualElement element) => element.name;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string SetName(this VisualElement element, string name) => element.name = name;
        
        #endregion Name

        #region PickingMode
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PickingMode GetPickingMode(this VisualElement element) => element.pickingMode;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PickingMode SetPickingMode(this VisualElement element, PickingMode pickingMode) => 
            element.pickingMode = pickingMode;
        
        #endregion PickingMode

        #region ViewDataKey

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetViewDataKey(this VisualElement element) => element.viewDataKey;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string SetViewDataKey(this VisualElement element, string viewDataKey) => 
            element.viewDataKey = viewDataKey;

        #endregion ViewDataKey

        #region UserData

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetUserData(this VisualElement element) => element.userData;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object SetUserData(this VisualElement element, object userData) => 
            element.userData = userData;

        #endregion UserData

        #region TabIndex

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetTabIndex(this VisualElement element) => element.tabIndex;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SetTabIndex(this VisualElement element, int tabIndex) => element.tabIndex = tabIndex;

        #endregion TabIndex

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rect GetLayout(this VisualElement element) => element.layout;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VisualElement GetContentContainer(this VisualElement element) => element.contentContainer;

        #region Tooltip

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetTooltip(this VisualElement element) => element.tooltip;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string SetTooltip(this VisualElement element, string tooltip) => element.tooltip = tooltip;

        #endregion

        #region Hierarchy

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetChildCount(this VisualElement element) => element.childCount;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VisualElement GetChildAt(this VisualElement element, int index) => element.ElementAt(index);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VisualElement ChildAt(this VisualElement element, int index) => element.ElementAt(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetIndexOf(this VisualElement element, VisualElement child) => element.IndexOf(child);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetIndex(this VisualElement element) => 
            element.parent?.GetIndexOf(element) ?? 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VisualElement GetParent(this VisualElement element) => element.parent;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<VisualElement> GetChildren(this VisualElement element) => element.Children();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IPanel GetPanel(this VisualElement element) => element.panel;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddChild(this VisualElement element, VisualElement child) => element.Add(child);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsChild(this VisualElement element, VisualElement child) => element.Contains(child);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear(this VisualElement element) => element.Clear();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InsertAt(this VisualElement element, int index, VisualElement child) => 
            element.Insert(index, child);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveChildAt(this VisualElement element, int index) => element.RemoveAt(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveChild(this VisualElement element, VisualElement child) => element.Remove(child);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SortChildren(this VisualElement element, Comparison<VisualElement> comp) => 
            element.Sort(comp);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MakeFirst(this VisualElement element) => element.SendToBack();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MoveToBack(this VisualElement element) => element.SendToBack();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MakeLast(this VisualElement element) => element.BringToFront();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MoveToFront(this VisualElement element) => element.BringToFront();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MoveBehind(this VisualElement element, VisualElement sibling) => 
            element.PlaceBehind(sibling);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MoveInFront(this VisualElement element, VisualElement sibling) => 
            element.PlaceInFront(sibling);
        
        public static VisualElement MoveUp(this VisualElement element)
        {
            if (element.parent == null)
                return element;

            int index = element.parent.IndexOf(element);
            if (index <= 0 || index >= element.parent.childCount) return element;
            
            var sibling = element.parent.ElementAt(index - 1);
            element.PlaceBehind(sibling);

            return element;
        }

        public static VisualElement MoveDown(this VisualElement element)
        {
            if (element.parent == null)
                return element;

            int index = element.parent.IndexOf(element);
            if (index < 0 || index >= element.parent.childCount - 1) return element;
            
            var sibling = element.parent.ElementAt(index + 1);
            element.PlaceInFront(sibling);

            return element;
        }

        public static VisualElement GetSibling(this VisualElement element, int indexDelta)
        {
            if (element.parent == null)
                return null;

            int index = element.parent.IndexOf(element) + indexDelta;
            if (index < 0 || index >= element.parent.childCount) return null;
            
            var sibling = element.parent.ElementAt(index);
            return sibling;

        }
        #endregion

        #region Classes
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetClassEnabled(this VisualElement element, string className, bool enable) =>
            element.EnableInClassList(className, enable);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToggleClass(this VisualElement element, string className) =>
            element.ToggleInClassList(className);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddClass(this VisualElement element, string className) =>
            element.AddToClassList(className);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveClass(this VisualElement element, string className) => 
            element.RemoveFromClassList(className);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasClass(this VisualElement element, string className) => 
            element.ClassListContains(className);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearClasses(this VisualElement element) => element.ClearClassList();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllClasses(this VisualElement element) => element.GetClasses();


        static List<string> _tmpClassNamesList = new List<string>();
        public static List<string> GetAllClassesAsTemporaryList(this VisualElement element, List<string> resultList = null)
        {
            var results = resultList ?? _tmpClassNamesList;

            results.Clear();
            results.AddRange(element.GetClasses());

            return results;
        }
        
        #endregion Classes

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsLocalPoint(this VisualElement element, Vector2 localPoint) => 
            element.ContainsPoint(localPoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DoesOverlap(this VisualElement element, Rect rectangle) => element.Overlaps(rectangle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFocus(this VisualElement element) => element.Focus();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Repaint(this VisualElement element) => element.MarkDirtyRepaint();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TriggerEvent(this VisualElement element, EventBase e) => element.SendEvent(e);

        // S T Y L E S

        #region HasValue

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue<T>(this StyleEnum<T> style) where T : struct, IConvertible => 
            style == StyleKeyword.Null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue(this StyleLength style) => style == StyleKeyword.Null;

        public static bool HasValue(this StyleFloat style) => style == StyleKeyword.Null;

        #endregion HasValue

        #region Visibility
        
        public static bool GetVisible(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.visibility == Visibility.Visible
                : element.style.visibility.value == Visibility.Visible;

        public static StyleEnum<Visibility> SetVisible(this VisualElement element, bool visible) =>
            element.style.visibility = visible ? Visibility.Visible : Visibility.Hidden;

        public static StyleEnum<Visibility> ResetVisibility(this VisualElement element) =>
            element.style.visibility = StyleKeyword.Null;
        
        #region VisibilityDisplay

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisplayStyle GetDisplay(this VisualElement element, bool resolved = false) =>
            resolved ? element.resolvedStyle.display : element.style.display.value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VisualElement ToggleDisplay(this VisualElement element)
        {
            element.style.display = element.style.display.value switch
            {
                DisplayStyle.Flex => DisplayStyle.None,
                DisplayStyle.None => DisplayStyle.Flex,
                _ => DisplayStyle.Flex
            };
            return element;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StyleEnum<DisplayStyle> SetDisplay(this VisualElement element, DisplayStyle display) => 
            element.style.display = display;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StyleEnum<DisplayStyle> ResetDisplay(this VisualElement element) =>
            element.style.display = StyleKeyword.Null;
        
        #endregion VisibilityDisplay

        #region VisibilityOpacity
        public static float GetOpacity(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.opacity : element.style.opacity.value;

        /// <summary>
        /// Sets the opactiy of the object.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="opacity">Range is from 0.0f to 1.0f</param>
        /// <returns></returns>
        public static VisualElement SetOpacity(this VisualElement element, float opacity)
        {
            element.style.opacity = opacity;
            return element;
        }

        public static VisualElement ResetOpacity(this VisualElement element)
        {
            element.style.opacity = StyleKeyword.Null;
            return element;
        }
        
        #endregion VisibilityOpacity
        
        #endregion Visibility

        #region Overflow
        
        public static Overflow GetOverflow(this VisualElement element) =>
            // Seems like element.resolvedStyle.overflow; is not a thing. TODO: Investigate.
            element.style.overflow.value;

        public static StyleEnum<Overflow> SetOverflow(this VisualElement element, Overflow overflow) =>
            element.style.overflow = overflow;

        public static StyleEnum<Overflow> ResetOverflow(this VisualElement element) =>
            element.style.overflow = StyleKeyword.Null;
        
        #endregion Overflow

        #region Direction
        
        public static FlexDirection GetFlexDirection(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.flexDirection : element.style.flexDirection.value;

        public static StyleEnum<FlexDirection> SetFlexDirection(this VisualElement element, FlexDirection flexDirection) =>
            element.style.flexDirection = flexDirection;

        public static StyleEnum<FlexDirection> ResetFlexDirection(this VisualElement element) =>
            element.style.flexDirection = StyleKeyword.Null;
        
        #endregion Direction

        #region GrowOptions
        
        public static float GetFlexGrow(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.flexGrow : element.style.flexGrow.value;

        public static StyleFloat SetFlexGrow(this VisualElement element, float flexGrow) =>
            element.style.flexGrow = flexGrow;

        public static StyleFloat ResetFlexGrow(this VisualElement element) =>
            element.style.flexGrow = StyleKeyword.Null;

        public static float GetFlexShrink(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.flexShrink : element.style.flexShrink.value;

        public static StyleFloat SetFlexShrink(this VisualElement element, float flexShrink) =>
            element.style.flexShrink = flexShrink;

        public static StyleFloat ResetFlexShrink(this VisualElement element) =>
            element.style.flexShrink = StyleKeyword.Null;
        
        #endregion GrowOptions

        #region Wrap
        public static Wrap GetFlexWrap(this VisualElement element, bool resolved = false)
        {
            if (resolved)
            {
                return element.resolvedStyle.flexWrap;
            }
            else
            {
                return element.style.flexWrap.value;
            }
        }

        public static VisualElement SetFlexWrap(this VisualElement element, Wrap flexWrap)
        {
            element.style.flexWrap = flexWrap;
            return element;
        }

        public static VisualElement ResetFlexWrap(this VisualElement element)
        {
            element.style.flexWrap = StyleKeyword.Null;
            return element;
        }
        
        #endregion Wrap
        
        #region Alignment

        public static Align GetAlignContent(this VisualElement element, bool resolved = false)
        {
            if (resolved)
            {
                return element.resolvedStyle.alignContent;
            }
            else
            {
                return element.style.alignContent.value;
            }
        }

        public static VisualElement SetAlignContent(this VisualElement element, Align alignContent)
        {
            element.style.alignContent = alignContent;
            return element;
        }

        public static VisualElement ResetAlignContent(this VisualElement element)
        {
            element.style.alignContent = StyleKeyword.Null;
            return element;
        }

        public static Align GetAlignItems(this VisualElement element, bool resolved = false)
        {
            if (resolved)
            {
                return element.resolvedStyle.alignItems;
            }
            else
            {
                return element.style.alignItems.value;
            }
        }

        public static VisualElement SetAlignItems(this VisualElement element, Align alignItems)
        {
            element.style.alignItems = alignItems;
            return element;
        }

        public static VisualElement ResetAlignItems(this VisualElement element)
        {
            element.style.alignItems = StyleKeyword.Null;
            return element;
        }

        public static Align GetAlignSelf(this VisualElement element, bool resolved = false)
        {
            if (resolved)
            {
                return element.resolvedStyle.alignSelf;
            }
            else
            {
                return element.style.alignSelf.value;
            }
        }

        public static VisualElement SetAlignSelf(this VisualElement element, Align alignSelf)
        {
            element.style.alignSelf = alignSelf;
            return element;
        }

        public static VisualElement ResetAlignSelf(this VisualElement element)
        {
            element.style.alignSelf = StyleKeyword.Null;
            return element;
        }
        
        #endregion Alignment
        
        #region Text

        #region TextAlignment
        public static TextAnchor GetAlignText(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.unityTextAlign : element.style.unityTextAlign.value;

        public static TextAnchor SetAlignText(this VisualElement element, TextAnchor unityTextAlign) =>
            (element.style.unityTextAlign = unityTextAlign).value;

        public static StyleEnum<TextAnchor> ResetAlignText(this VisualElement element) =>
            element.style.unityTextAlign = StyleKeyword.Null;
        
        #endregion TextAlignment

        #region TextOverflow
        
        public static TextOverflow GetTextOverflow(this VisualElement element, bool resolved = false)
        {
            if (resolved)
            {
                return element.resolvedStyle.textOverflow;
            }
            else
            {
                return element.style.textOverflow.value;
            }
        }

        public static VisualElement SetTextOverflow(this VisualElement element, TextOverflow overflow)
        {
            element.style.textOverflow = overflow;
            return element;
        }

        public static VisualElement ResetTextOverflow(this VisualElement element)
        {
            element.style.textOverflow = StyleKeyword.Null;
            return element;
        }
        
        #endregion TextOverflow
        
        #region TextFontSize

        public static float GetFontSize(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.fontSize : element.style.fontSize.value.value;

        public static VisualElement SetFontSize(this VisualElement element, float size)
        {
            element.style.fontSize = size;
            return element;
        }

        public static VisualElement ResetFontSize(this VisualElement element)
        {
            element.style.fontSize = StyleKeyword.Null;
            return element;
        }
        
        #endregion TextFontSize
        
        #region TextOutline

        public static float GetTextOutlineWidth(this VisualElement element, bool resolved = false)
        {
            if (resolved)
            {
                return element.resolvedStyle.unityTextOutlineWidth;
            }
            else
            {
                return element.style.unityTextOutlineWidth.value;
            }
        }

        public static VisualElement SetTextOutlineWidth(this VisualElement element, float width)
        {
            element.style.unityTextOutlineWidth = width;
            return element;
        }

        public static VisualElement ResetTextOutlineWidth(this VisualElement element)
        {
            element.style.unityTextOutlineWidth = StyleKeyword.Null;
            return element;
        }

        public static Color GetTextOutlineColor(this VisualElement element, bool resolved = false)
        {
            if (resolved)
            {
                return element.resolvedStyle.unityTextOutlineColor;
            }
            else
            {
                return element.style.unityTextOutlineColor.value;
            }
        }

        public static VisualElement SetTextOutlineColor(this VisualElement element, Color color)
        {
            element.style.unityTextOutlineColor = color;
            return element;
        }

        public static VisualElement ResetTextOutlineColor(this VisualElement element)
        {
            element.style.unityTextOutlineColor = StyleKeyword.Null;
            return element;
        }
        
        #endregion TextOutline
        
        #region TextFontStyle

        public static FontStyle GetFontStyle(this VisualElement element, bool resolved = false)
        {
            if (resolved)
            {
                return element.resolvedStyle.unityFontStyleAndWeight;
            }
            else
            {
                return element.style.unityFontStyleAndWeight.value;
            }
        }

        public static VisualElement SetFontStyle(this VisualElement element, FontStyle style)
        {
            element.style.unityFontStyleAndWeight = style;
            return element;
        }

        public static VisualElement ResetFontStyle(this VisualElement element)
        {
            element.style.unityFontStyleAndWeight = StyleKeyword.Null;
            return element;
        }
        
        #endregion TextFontStyle

        #region TextFont
        
        public static StyleFont GetFont(this VisualElement element, bool resolved = false)
        {
            if (resolved)
            {
                return element.resolvedStyle.unityFont;
            }
            else
            {
                return element.style.unityFont.value;
            }
        }

        public static VisualElement SetFont(this VisualElement element, StyleFont font)
        {
            element.style.unityFont = font;
            return element;
        }

        public static VisualElement ResetFont(this VisualElement element)
        {
            element.style.unityFont = StyleKeyword.Null;
            return element;
        }

        public static FontDefinition GetFontDefinition(this VisualElement element, bool resolved = false)
        {
            if (resolved)
            {
                return element.resolvedStyle.unityFontDefinition;
            }
            else
            {
                return element.style.unityFontDefinition.value;
            }
        }

        public static VisualElement SetFontDefinition(this VisualElement element, FontDefinition definition)
        {
            element.style.unityFontDefinition = definition;
            return element;
        }

        public static VisualElement ResetFontDefinition(this VisualElement element)
        {
            element.style.unityFontDefinition = StyleKeyword.Null;
            return element;
        }
        
        #endregion TextFont
        
        #endregion Text

        #region Color

        public static Color GetColor(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.color : element.style.color.value;

        public static StyleColor SetColor(this VisualElement element, Color color) =>
            element.style.color = color;

        public static StyleColor ResetColor(this VisualElement element) =>
            element.style.color = StyleKeyword.Null;
        
        #endregion Color
        
        #region Background

        #region BackgroundColor
        public static Color GetBackgroundColor(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.backgroundColor : element.style.backgroundColor.value;

        public static StyleColor SetBackgroundColor(this VisualElement element, Color color) =>
            element.style.backgroundColor = color;

        public static StyleColor ResetBackgroundColor(this VisualElement element) =>
            element.style.backgroundColor = StyleKeyword.Null;
        
        #endregion BackgroundColor

        #region BackgroundImage
        public static Background GetBackgroundImage(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.backgroundImage : element.style.backgroundImage.value;

        public static StyleBackground SetBackgroundImage(this VisualElement element, Background image) =>
            element.style.backgroundImage = image;

        public static StyleBackground SetBackgroundImage(this VisualElement element, Sprite sprite) =>
            element.style.backgroundImage = new Background() {sprite = sprite};

        public static StyleBackground SetBackgroundImage(this VisualElement element, VectorImage vectorImage) =>
            element.style.backgroundImage = new Background() {vectorImage = vectorImage};

        public static StyleBackground SetBackgroundImage(this VisualElement element, Texture2D texture) =>
            element.style.backgroundImage = new Background() {texture = texture};

        public static StyleBackground SetBackgroundImage(this VisualElement element, RenderTexture renderTexture)=>
            element.style.backgroundImage = new Background() {renderTexture = renderTexture};

        public static StyleBackground ResetBackgroundImage(this VisualElement element) =>
            element.style.backgroundImage = StyleKeyword.Null;

        public static Color GetBackgroundImageTint(this VisualElement element, bool resolved = false) => 
            resolved ? 
                element.resolvedStyle.unityBackgroundImageTintColor : element.style.unityBackgroundImageTintColor.value;

        public static StyleColor SetBackgroundImageTint(this VisualElement element, Color tintColor) =>
            element.style.unityBackgroundImageTintColor = tintColor;

        public static StyleColor ResetBackgroundImageTint(this VisualElement element) =>
            element.style.unityBackgroundImageTintColor = StyleKeyword.Null;
        
        #endregion BackgroundImage
        
        #endregion Background

        #region Border
        
        #region BorderColor

        #region BorderLeftColor

        public static StyleColor SetBorderLeftColor(this VisualElement element, Color color) =>
            element.style.borderLeftColor = color;

        public static StyleColor ResetBorderLeftColor(this VisualElement element) =>
            element.style.borderLeftColor = StyleKeyword.Null;
        
        public static Color GetBorderLeftColor(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderLeftColor : element.style.borderLeftColor.value;
        
        #endregion BorderLeftColor

        #region BorderRightColor

        public static StyleColor SetBorderRightColor(this VisualElement element, Color color) =>
            element.style.borderRightColor = color;

        public static StyleColor ResetBorderRightColor(this VisualElement element) =>
            element.style.borderRightColor = StyleKeyword.Null;
        
        public static Color GetBorderRightColor(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderRightColor : element.style.borderRightColor.value;

        #endregion BorderRightColor

        #region BorderTopColor

        public static StyleColor SetBorderTopColor(this VisualElement element, Color color) =>
            element.style.borderTopColor = color;

        public static StyleColor ResetBorderTopColor(this VisualElement element) =>
            element.style.borderTopColor = StyleKeyword.Null;
        
        public static Color GetBorderTopColor(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderTopColor : element.style.borderTopColor.value;

        #endregion BorderTopColor

        #region BorderBottomColor

        public static StyleColor SetBorderBottomColor(this VisualElement element, Color color) =>
            element.style.borderBottomColor = color;

        public static StyleColor ResetBorderBottomColor(this VisualElement element) =>
            element.style.borderBottomColor = StyleKeyword.Null;
        
        public static Color GetBorderBottomColor(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderBottomColor : element.style.borderBottomColor.value;

        #endregion BorderBottomColor

        public static (Color, Color, Color, Color) GetBorderColor(this VisualElement element) =>
            (element.style.borderLeftColor.value,
                element.style.borderTopColor.value,
                    element.style.borderRightColor.value,
                        element.style.borderBottomColor.value);

        public static Color SetBorderColor(this VisualElement element, Color color) =>
                (element.style.borderLeftColor = 
                    element.style.borderTopColor = 
                        element.style.borderRightColor = 
                            element.style.borderBottomColor = color).value;

        public static StyleColor ResetBorderColor(this VisualElement element) =>
            element.style.borderLeftColor = 
                element.style.borderTopColor = 
                    element.style.borderRightColor = 
                        element.style.borderBottomColor = StyleKeyword.Null;

        public static (StyleColor, StyleColor, StyleColor, StyleColor) SetBorderColor(this VisualElement element, 
            Color? left = null, Color? top = null, Color? right = null, Color? bottom = null) =>
            (left.HasValue ? element.style.borderLeftColor = left.Value : GetBorderLeftColor(element),
            top.HasValue ? element.style.borderTopColor = top.Value : GetBorderTopColor(element),
            right.HasValue ? element.style.borderRightColor = right.Value : GetBorderRightColor(element),
            bottom.HasValue ? element.style.borderBottomColor = bottom.Value : GetBorderBottomColor(element));
        
        #endregion BorderColor

        #region BorderWidth
        public static float GetBorderLeftWidth(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderLeftWidth : element.style.borderLeftWidth.value;

        public static float GetBorderRightWidth(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderRightWidth : element.style.borderRightWidth.value;

        public static float GetBorderTopWidth(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderTopWidth : element.style.borderTopWidth.value;

        public static float GetBorderBottomWidth(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderBottomWidth : element.style.borderBottomWidth.value;

        public static float GetBorderWidth(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderLeftWidth : element.style.borderLeftWidth.value;

        public static Vector4 SetBorderWidth(this VisualElement element, float width, bool delta = false) =>
            new(SetBorderLeftWidth(element, width, delta).value,
            SetBorderRightWidth(element, width, delta).value,
            SetBorderTopWidth(element, width, delta).value,
            SetBorderBottomWidth(element, width, delta).value);

        public static StyleFloat ResetBorderWidth(this VisualElement element) =>
            element.style.borderLeftWidth = 
                element.style.borderTopWidth = 
                    element.style.borderRightWidth = 
                        element.style.borderBottomWidth = StyleKeyword.Null;

        public static StyleFloat ResetBorderLeftWidth(this VisualElement element) =>
            element.style.borderLeftWidth = StyleKeyword.Null;

        public static StyleFloat ResetBorderTopWidth(this VisualElement element) =>
            element.style.borderTopWidth = StyleKeyword.Null;

        public static StyleFloat ResetBorderRightWidth(this VisualElement element) =>
            element.style.borderRightWidth = StyleKeyword.Null;

        public static StyleFloat ResetBorderBottomWidth(this VisualElement element) =>
            element.style.borderBottomWidth = StyleKeyword.Null;

        public static Vector4 SetBorderWidths(this VisualElement element, float? left = null, float? top = null, 
            float? right = null, float? bottom = null, bool delta = false) =>
            new(
                left.HasValue ? SetBorderLeftWidth(element, left.Value, delta).value : GetBorderLeftWidth(element),
                right.HasValue ? SetBorderRightWidth(element, right.Value, delta).value : GetBorderRightWidth(element),
                top.HasValue ? SetBorderTopWidth(element, top.Value, delta).value : GetBorderTopWidth(element),
                bottom.HasValue ? SetBorderBottomWidth(element, bottom.Value, delta).value : GetBorderBottomWidth(element));

        public static StyleFloat SetBorderLeftWidth(this VisualElement element, float width, bool delta = false) =>
                element.style.borderLeftWidth = 
                    (delta ? 
                        (element.style.borderLeftWidth.HasValue() ? 
                            element.style.borderLeftWidth.value : 
                            element.resolvedStyle.borderLeftWidth) : 0f) + width;
        
        public static StyleFloat SetBorderTopWidth(this VisualElement element, float width, bool delta = false) =>
            element.style.borderTopWidth = 
                (delta ? 
                    (element.style.borderTopWidth.HasValue() ? 
                        element.style.borderTopWidth.value : 
                        element.resolvedStyle.borderTopWidth) : 0f) + width;
        
        public static StyleFloat SetBorderRightWidth(this VisualElement element, float width, bool delta = false) =>
            element.style.borderRightWidth = 
                (delta ? 
                    (element.style.borderRightWidth.HasValue() ? 
                        element.style.borderRightWidth.value : 
                        element.resolvedStyle.borderRightWidth) : 0f) + width;
        
        public static StyleFloat SetBorderBottomWidth(this VisualElement element, float width, bool delta = false) =>
            element.style.borderBottomWidth = 
                (delta ? 
                    (element.style.borderBottomWidth.HasValue() ? 
                        element.style.borderBottomWidth.value : 
                        element.resolvedStyle.borderBottomWidth) : 0f) + width;
        
        #endregion BorderWidth
        
        #region borderRadius

        #region BorderAllRadius

        public static Vector4 GetBorderRadius(this VisualElement element, bool resolved = false) =>
            new(
                resolved ? 
                    element.resolvedStyle.borderTopLeftRadius : element.style.borderTopLeftRadius.value.value,
                resolved ? 
                    element.resolvedStyle.borderTopRightRadius : element.style.borderTopRightRadius.value.value,
                resolved ? 
                    element.resolvedStyle.borderBottomLeftRadius : element.style.borderBottomLeftRadius.value.value,
                resolved ? 
                    element.resolvedStyle.borderBottomRightRadius : element.style.borderBottomRightRadius.value.value);

        public static Vector4 SetBorderRadius(this VisualElement element, float radius, LengthUnit unit, 
            bool delta = false) =>
            new(
                SetBorderTopLeftRadius(element, radius, unit, delta).value.value,
                SetBorderTopRightRadius(element, radius, unit, delta).value.value,
                SetBorderBottomLeftRadius(element, radius, unit, delta).value.value,
                SetBorderBottomRightRadius(element, radius, unit, delta).value.value);

        public static Vector4 SetBorderRadius(this VisualElement element, float radius, bool delta = false) =>
            new(
                SetBorderTopLeftRadius(element, radius, delta).value.value,
                SetBorderTopRightRadius(element, radius, delta).value.value,
                SetBorderBottomLeftRadius(element, radius, delta).value.value,
                SetBorderBottomRightRadius(element, radius, delta).value.value);

        public static StyleLength ResetBorderRadius(this VisualElement element) =>
            element.style.borderTopLeftRadius = element.style.borderTopRightRadius = 
                element.style.borderBottomLeftRadius = element.style.borderBottomRightRadius = StyleKeyword.Null;

        #endregion BorderAllRadius

        #region BorderTopLeftRadius

        public static StyleLength SetBorderTopLeftRadius(this VisualElement element, float radius, LengthUnit unit, bool delta = false) =>
            element.style.borderTopLeftRadius = 
                new Length((delta ? 
                    (element.style.borderTopLeftRadius.HasValue() ? 
                        element.style.borderTopLeftRadius.value.value : 
                        element.resolvedStyle.borderTopLeftRadius) : 0f) + radius, unit);
        
        public static StyleLength SetBorderTopLeftRadius(this VisualElement element, float radius, bool delta = false) =>
            element.style.borderTopLeftRadius = 
                new Length((delta ? 
                    (element.style.borderTopLeftRadius.HasValue() ? 
                        element.style.borderTopLeftRadius.value.value : 
                        element.resolvedStyle.borderTopLeftRadius) : 0f) + radius);
        
        public static StyleLength ResetBorderTopLeftRadius(this VisualElement element) =>
            element.style.borderTopLeftRadius = StyleKeyword.Null;
        
        public static float GetBorderTopLeftRadius(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderTopLeftRadius : element.style.borderTopLeftRadius.value.value;
        
        #endregion BorderTopLeftRadius

        #region BorderTopRightRadius

        public static StyleLength SetBorderTopRightRadius(this VisualElement element, float radius, LengthUnit unit, bool delta = false) =>
            element.style.borderTopRightRadius = 
                new Length((delta ? 
                    (element.style.borderTopRightRadius.HasValue() ? 
                        element.style.borderTopRightRadius.value.value : 
                        element.resolvedStyle.borderTopRightRadius) : 0f) + radius, unit);
        
        public static StyleLength SetBorderTopRightRadius(this VisualElement element, float radius, bool delta = false) =>
            element.style.borderTopRightRadius = 
                new Length((delta ? 
                    (element.style.borderTopRightRadius.HasValue() ? 
                        element.style.borderTopRightRadius.value.value : 
                        element.resolvedStyle.borderTopRightRadius) : 0f) + radius);

        public static StyleLength ResetBorderTopRightRadius(this VisualElement element) =>
            element.style.borderTopRightRadius = StyleKeyword.Null;
        
        public static float GetBorderTopRightRadius(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderTopRightRadius : element.style.borderTopRightRadius.value.value;

        #endregion BorderTopRightRadius
        
        #region BorderBottomLeftRadius

        public static StyleLength SetBorderBottomLeftRadius(this VisualElement element, float radius, LengthUnit unit, bool delta = false) =>
            element.style.borderBottomLeftRadius = 
                new Length((delta ? 
                    (element.style.borderBottomLeftRadius.HasValue() ? 
                        element.style.borderBottomLeftRadius.value.value : 
                        element.resolvedStyle.borderBottomLeftRadius) : 0f) + radius, unit);
        
        public static StyleLength SetBorderBottomLeftRadius(this VisualElement element, float radius, bool delta = false) =>
            element.style.borderBottomLeftRadius = 
                new Length((delta ? 
                    (element.style.borderBottomLeftRadius.HasValue() ? 
                        element.style.borderBottomLeftRadius.value.value : 
                        element.resolvedStyle.borderBottomLeftRadius) : 0f) + radius);
        
        public static StyleLength ResetBorderBottomLeftRadius(this VisualElement element) =>
            element.style.borderBottomLeftRadius = StyleKeyword.Null;
        
        public static float GetBorderBottomLeftRadius(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderBottomLeftRadius : element.style.borderBottomLeftRadius.value.value;
        
        #endregion BorderBottomLeftRadius
        
        #region BorderBottomRightRadius

        public static StyleLength SetBorderBottomRightRadius(this VisualElement element, float radius, LengthUnit unit, bool delta = false) =>
            element.style.borderBottomRightRadius = 
                new Length((delta ? 
                    (element.style.borderBottomRightRadius.HasValue() ? 
                        element.style.borderBottomRightRadius.value.value : 
                        element.resolvedStyle.borderBottomRightRadius) : 0f) + radius, unit);
        
        public static StyleLength SetBorderBottomRightRadius(this VisualElement element, float radius, bool delta = false) =>
            element.style.borderBottomRightRadius = 
                new Length((delta ? 
                    (element.style.borderBottomRightRadius.HasValue() ? 
                        element.style.borderBottomRightRadius.value.value : 
                        element.resolvedStyle.borderBottomRightRadius) : 0f) + radius);
        
        public static StyleLength ResetBorderBottomRightRadius(this VisualElement element) =>
            element.style.borderBottomRightRadius = StyleKeyword.Null;
        
        public static float GetBorderBottomRightRadius(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.borderBottomRightRadius : element.style.borderBottomRightRadius.value.value;
        
        #endregion BorderBottomRightRadius
        
        #endregion borderRadius
        
        #endregion Border
        
        #region Margin

        #region MarginLeft

        public static StyleLength SetMarginLeft(this VisualElement element, float margin, LengthUnit unit, bool delta = false) =>
            element.style.marginLeft = new Length(
                ((delta ? 
                    element.style.marginLeft.HasValue() ? 
                        element.style.marginLeft.value.value : 
                        element.resolvedStyle.marginLeft : 0f) + margin), unit);

        public static StyleLength SetMarginLeft(this VisualElement element, float margin, bool delta = false) =>
            element.style.marginLeft = 
                (delta ? 
                    element.style.marginLeft.HasValue() ? 
                        element.style.marginLeft.value.value : 
                        element.resolvedStyle.marginLeft : 0f) + margin;

        public static StyleLength ResetMarginLeft(this VisualElement element) =>
            element.style.marginLeft = StyleKeyword.Null;
        
        public static float GetMarginLeft(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.marginLeft : element.style.marginLeft.value.value;

        #endregion MarginLeft

        #region MarginTop

        public static StyleLength SetMarginTop(this VisualElement element, float margin, LengthUnit unit, bool delta = false) =>
            element.style.marginTop = new Length(
                ((delta ? 
                    element.style.marginTop.HasValue() ? 
                        element.style.marginTop.value.value : 
                        element.resolvedStyle.marginTop : 0f) + margin), unit);

        public static StyleLength SetMarginTop(this VisualElement element, float margin, bool delta = false) =>
            element.style.marginTop = 
                (delta ? 
                    element.style.marginTop.HasValue() ? 
                        element.style.marginTop.value.value : 
                        element.resolvedStyle.marginTop : 0f) + margin;

        public static StyleLength ResetMarginTop(this VisualElement element) =>
            element.style.marginTop = StyleKeyword.Null;
        
        public static float GetMarginTop(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.marginTop : element.style.marginTop.value.value;

        #endregion MarginTop

        #region MarginRight

        public static StyleLength SetMarginRight(this VisualElement element, float margin, LengthUnit unit, bool delta = false) =>
            element.style.marginRight = new Length(
                ((delta ? 
                    element.style.marginRight.HasValue() ? 
                        element.style.marginRight.value.value : 
                        element.resolvedStyle.marginRight : 0f) + margin), unit);

        public static StyleLength SetMarginRight(this VisualElement element, float margin, bool delta = false) =>
            element.style.marginRight = 
                (delta ? 
                    element.style.marginRight.HasValue() ? 
                        element.style.marginRight.value.value : 
                        element.resolvedStyle.marginRight : 0f) + margin;

        public static StyleLength ResetMarginRight(this VisualElement element) =>
            element.style.marginRight = StyleKeyword.Null;
        
        public static float GetMarginRight(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.marginRight : element.style.marginRight.value.value;

        #endregion MarginRight

        #region MarginBottom

        public static StyleLength SetMarginBottom(this VisualElement element, float margin, LengthUnit unit, bool delta = false) =>
            element.style.marginBottom = new Length(
                ((delta ? 
                    element.style.marginBottom.HasValue() ? 
                        element.style.marginBottom.value.value : 
                        element.resolvedStyle.marginBottom : 0f) + margin), unit);

        public static StyleLength SetMarginBottom(this VisualElement element, float margin, bool delta = false) =>
            element.style.marginBottom = 
                (delta ? 
                    element.style.marginBottom.HasValue() ? 
                        element.style.marginBottom.value.value : 
                        element.resolvedStyle.marginBottom : 0f) + margin;

        public static StyleLength ResetMarginBottom(this VisualElement element) =>
            element.style.marginBottom = StyleKeyword.Null;
        
        public static float GetMarginBottom(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.marginBottom : element.style.marginBottom.value.value;

        #endregion MarginBottom

        #region MarginAll
        
        public static Vector4 SetMargin(this VisualElement element, float margin, LengthUnit unit, bool delta = false) =>
            new(SetMarginLeft(element, margin, unit, delta).value.value,
            SetMarginTop(element, margin, unit, delta).value.value,
            SetMarginRight(element, margin, unit, delta).value.value,
            SetMarginBottom(element, margin, unit, delta).value.value);

        public static Vector4 SetMargin(this VisualElement element, float margin, bool delta = false) =>
            new(SetMarginLeft(element, margin, delta).value.value,
            SetMarginTop(element, margin, delta).value.value,
            SetMarginRight(element, margin, delta).value.value,
            SetMarginBottom(element, margin, delta).value.value);

        public static StyleLength ResetMargin(this VisualElement element) =>
            element.style.marginLeft = element.style.marginTop = element.style.marginRight = 
                element.style.marginBottom = StyleKeyword.Null;

        public static Vector4 GetMargin(this VisualElement element, bool resolved = false) => 
            new(GetMarginLeft(element, resolved), GetMarginTop(element, resolved), GetMarginRight(element, resolved),
                GetMarginBottom(element, resolved));
        
        #endregion MarginAll

        #endregion Margin

        #region Padding

        #region PaddingLeft

        public static StyleLength SetPaddingLeft(this VisualElement element, float padding, LengthUnit unit, bool delta = false) =>
            element.style.paddingLeft = new Length(
                ((delta ? 
                    element.style.paddingLeft.HasValue() ? 
                        element.style.paddingLeft.value.value : 
                        element.resolvedStyle.paddingLeft : 0f) + padding), unit);

        public static StyleLength SetPaddingLeft(this VisualElement element, float padding, bool delta = false) =>
            element.style.paddingLeft = 
                (delta ? 
                    element.style.paddingLeft.HasValue() ? 
                        element.style.paddingLeft.value.value : 
                        element.resolvedStyle.paddingLeft : 0f) + padding;

        public static StyleLength ResetPaddingLeft(this VisualElement element) =>
            element.style.paddingLeft = StyleKeyword.Null;
        
        public static float GetPaddingLeft(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.paddingLeft : element.style.paddingLeft.value.value;

        #endregion PaddingLeft

        #region PaddingTop

        public static StyleLength SetPaddingTop(this VisualElement element, float padding, LengthUnit unit, bool delta = false) =>
            element.style.paddingTop = new Length(
                ((delta ? 
                    element.style.paddingTop.HasValue() ? 
                        element.style.paddingTop.value.value : 
                        element.resolvedStyle.paddingTop : 0f) + padding), unit);

        public static StyleLength SetPaddingTop(this VisualElement element, float padding, bool delta = false) =>
            element.style.paddingTop = 
                (delta ? 
                    element.style.paddingTop.HasValue() ? 
                        element.style.paddingTop.value.value : 
                        element.resolvedStyle.paddingTop : 0f) + padding;

        public static StyleLength ResetPaddingTop(this VisualElement element) =>
            element.style.paddingTop = StyleKeyword.Null;
        
        public static float GetPaddingTop(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.paddingTop : element.style.paddingTop.value.value;

        #endregion PaddingTop

        #region PaddingRight

        public static StyleLength SetPaddingRight(this VisualElement element, float padding, LengthUnit unit, bool delta = false) =>
            element.style.paddingRight = new Length(
                ((delta ? 
                    element.style.paddingRight.HasValue() ? 
                        element.style.paddingRight.value.value : 
                        element.resolvedStyle.paddingRight : 0f) + padding), unit);

        public static StyleLength SetPaddingRight(this VisualElement element, float padding, bool delta = false) =>
            element.style.paddingRight = 
                (delta ? 
                    element.style.paddingRight.HasValue() ? 
                        element.style.paddingRight.value.value : 
                        element.resolvedStyle.paddingRight : 0f) + padding;

        public static StyleLength ResetPaddingRight(this VisualElement element) =>
            element.style.paddingRight = StyleKeyword.Null;
        
        public static float GetPaddingRight(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.paddingRight : element.style.paddingRight.value.value;

        #endregion PaddingRight

        #region PaddingBottom

        public static StyleLength SetPaddingBottom(this VisualElement element, float padding, LengthUnit unit, bool delta = false) =>
            element.style.paddingBottom = new Length(
                ((delta ? 
                    element.style.paddingBottom.HasValue() ? 
                        element.style.paddingBottom.value.value : 
                        element.resolvedStyle.paddingBottom : 0f) + padding), unit);

        public static StyleLength SetPaddingBottom(this VisualElement element, float padding, bool delta = false) =>
            element.style.paddingBottom = 
                (delta ? 
                    element.style.paddingBottom.HasValue() ? 
                        element.style.paddingBottom.value.value : 
                        element.resolvedStyle.paddingBottom : 0f) + padding;

        public static StyleLength ResetPaddingBottom(this VisualElement element) =>
            element.style.paddingBottom = StyleKeyword.Null;
        
        public static float GetPaddingBottom(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.paddingBottom : element.style.paddingBottom.value.value;

        #endregion PaddingBottom

        #region PaddingAll
        
        public static Vector4 SetPadding(this VisualElement element, float padding, LengthUnit unit, bool delta = false) =>
            new(SetPaddingLeft(element, padding, unit, delta).value.value,
            SetPaddingTop(element, padding, unit, delta).value.value,
            SetPaddingRight(element, padding, unit, delta).value.value,
            SetPaddingBottom(element, padding, unit, delta).value.value);

        public static Vector4 SetPadding(this VisualElement element, float padding, bool delta = false) =>
            new(SetPaddingLeft(element, padding, delta).value.value,
            SetPaddingTop(element, padding, delta).value.value,
            SetPaddingRight(element, padding, delta).value.value,
            SetPaddingBottom(element, padding, delta).value.value);

        public static StyleLength ResetPadding(this VisualElement element) =>
            element.style.paddingLeft = element.style.paddingTop = element.style.paddingRight = 
                element.style.paddingBottom = StyleKeyword.Null;

        public static Vector4 GetPadding(this VisualElement element, bool resolved = false) => 
            new(GetPaddingLeft(element, resolved), GetPaddingTop(element, resolved), GetPaddingRight(element, resolved),
                GetPaddingBottom(element, resolved));
        
        #endregion PaddingAll

        #endregion Padding

        #region Size

        public static Vector2 GetSize(this VisualElement element, bool resolved = false) => 
            resolved ? new Vector2(element.resolvedStyle.width, element.resolvedStyle.height) : 
                new Vector2(element.style.width.value.value, element.style.height.value.value);

        public static Vector2 SetSizeScalar(this VisualElement element, float width, float height,
            LengthUnit unit = default, bool delta = false) =>
            new(SetWidth(element, width, unit, delta).value.value,
                SetHeight(element, height, unit, delta).value.value);

        public static Vector2 SetSize(this VisualElement element, Vector2 size, LengthUnit unit = default,
            bool delta = false) =>
            new(SetWidth(element, size.x, unit, delta).value.value,
                SetHeight(element, size.y, unit, delta).value.value);

        #region Width
        public static float GetWidth(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.width : element.style.width.value.value;

        public static StyleLength SetWidth(this VisualElement element, float width, LengthUnit unit = default, 
            bool delta = false)
        {
            if (delta)
            {
                var length = element.style.width.value;
                // If the style is not yet defined then take the value from the resolved style.
                if (!element.style.width.HasValue())
                {
                    length = element.resolvedStyle.width;
                }
                length.value += width;
                element.style.width = length;
            }
            else
            {
                var length = new Length(width, unit);
                element.style.width = length;
            }

            return element.style.width;
        }

        public static StyleLength ResetWidth(this VisualElement element) => element.style.width = StyleKeyword.Null;

        #endregion Width
        
        #region Height
        public static float GetHeight(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.height : element.style.height.value.value;

        public static StyleLength SetHeight(this VisualElement element, float height, LengthUnit unit = default, 
            bool delta = false)
        {
            if (delta)
            {
                var length = element.style.height.value;
                // If the style is not yet defined then take the value from the resolved style.
                if (!element.style.height.HasValue())
                {
                    length = element.resolvedStyle.height;
                }
                length.value += height;
                element.style.height = length;
            }
            else
            {
                var length = new Length(height, unit);
                element.style.height = length;
            }

            return element.style.height;
        }

        public static StyleLength ResetHeight(this VisualElement element) => 
            element.style.height = StyleKeyword.Null;
        
        #endregion Height

        #region MinWidth
        public static float GetMinWidth(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.minWidth.value : element.style.minWidth.value.value;
        
        public static StyleLength SetMinWidth(this VisualElement element, float width, LengthUnit unit = default, 
            bool delta = false) =>
            element.style.minWidth = 
                new Length((delta ? 
                    (element.style.minWidth.HasValue() ? 
                        element.style.minWidth.value.value : 
                        element.resolvedStyle.minWidth.value) : 0f) + width, unit);

        public static StyleLength ResetMinWidth(this VisualElement element) =>
            element.style.minWidth = StyleKeyword.Null;
        
        #endregion MinWidth

        #region MaxWidth
        
        public static float GetMaxWidth(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.maxWidth.value : element.style.maxWidth.value.value;
        
        public static StyleLength SetMaxWidth(this VisualElement element, float width, LengthUnit unit = default, 
            bool delta = false) =>
            element.style.maxWidth = 
                new Length((delta ? 
                    (element.style.maxWidth.HasValue() ? 
                        element.style.maxWidth.value.value : 
                        element.resolvedStyle.maxWidth.value) : 0f) + width, unit);

        public static StyleLength ResetMaxWidth(this VisualElement element) =>
            element.style.maxWidth = StyleKeyword.Null;
        
        #endregion MaxWidth

        #region MinHeight

        public static float GetMinHeight(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.minHeight.value : element.style.minHeight.value.value;
        
        public static StyleLength SetMinHeight(this VisualElement element, float height, LengthUnit unit = default, 
            bool delta = false) =>
            element.style.minHeight = 
                new Length((delta ? 
                    (element.style.minHeight.HasValue() ? 
                        element.style.minHeight.value.value : 
                        element.resolvedStyle.minHeight.value) : 0f) + height, unit);

        public static StyleLength ResetMinHeight(this VisualElement element) =>
            element.style.minHeight = StyleKeyword.Null;
        
        #endregion MinHeight

        #region MaxHeight
        public static float GetMaxHeight(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.maxHeight.value : element.style.maxHeight.value.value;

        public static StyleLength SetMaxHeight(this VisualElement element, float height, LengthUnit unit = default, 
            bool delta = false) =>
                element.style.maxHeight = 
                    new Length((delta ? 
                        (element.style.maxHeight.HasValue() ? 
                            element.style.maxHeight.value.value : 
                            element.resolvedStyle.maxHeight.value) : 0f) + height, unit);

        public static StyleLength ResetMaxHeight(this VisualElement element) =>
            element.style.maxHeight = StyleKeyword.Null;
        
        #endregion MaxHeight

        #endregion Size
        
        #region Transform
        
        #region Position
        public static Position GetPosition(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.position : element.style.position.value;

        public static StyleEnum<Position> SetPosition(this VisualElement element, Position position) =>
            element.style.position = position;

        public static StyleEnum<Position> ResetPosition(this VisualElement element) =>
            element.style.position = StyleKeyword.Null;

        public static Vector2 GetPosTopLeft(this VisualElement element, bool resolved = false) => 
            resolved ? new Vector2(element.resolvedStyle.left, element.resolvedStyle.top) : 
                new Vector2(element.style.left.value.value, element.style.top.value.value);

        /// <summary>
        /// Just an alias for SetPosLeftTopScalar to make fuzzy finding easier.
        /// </summary>
        public static VisualElement SetPositionTopLeftScalar(this VisualElement element, float top, float left, 
            LengthUnit unit = default, bool delta = false) => 
            SetPositionLeftTopScalar(element, left, top, unit, delta);

        public static VisualElement SetPositionLeftTop(this VisualElement element, Vector2 leftTop, 
            LengthUnit unit = default, bool delta = false) => 
            SetPositionLeftTopScalar(element, leftTop.x, leftTop.y, unit, delta);

        public static VisualElement SetPositionLeftTopScalar(this VisualElement element, float left, float top, 
            LengthUnit unit = default, bool delta = false)
        {
            if (delta)
            {
                element.style.left = new Length(element.style.left.value.value + left, unit);
                element.style.top = new Length(element.style.top.value.value + top, unit);
            }
            else
            {
                element.style.left = new Length(left, unit);
                element.style.top = new Length(top, unit);
            }

            return element;
        }

        public static StyleLength ResetPositionLeftTop(this VisualElement element) => 
            element.style.left = element.style.top = StyleKeyword.Null;

        public static Vector2 GetPositionRightBottom(this VisualElement element, bool resolved = false) => 
            resolved ? new Vector2(element.resolvedStyle.right, element.resolvedStyle.bottom) : 
                new Vector2(element.style.right.value.value, element.style.bottom.value.value);

        /// <summary>
        /// Just an alias for SetPosRightBottomScalar to make fuzzy finding easier.
        /// </summary>
        public static Vector2 SetPositionBottomRightScalar(this VisualElement element, float bottom, float right, 
            LengthUnit unit = default, bool delta = false) => 
            SetPositionRightBottomScalar(element, right, bottom, unit, delta);

        public static Vector2 SetPositionRightBottom(this VisualElement element, Vector2 rightBottom, 
            LengthUnit unit = default, bool delta = false) => 
            SetPositionRightBottomScalar(element, rightBottom.x, rightBottom.y, unit, delta);

        public static Vector2 SetPositionRightBottomScalar(this VisualElement element, float right, float bottom, 
            LengthUnit unit = default, bool delta = false) =>
            new(SetPositionRight(element, right, unit, delta).value.value, 
                SetPositionBottom(element, bottom, unit, delta).value.value);

        public static StyleLength ResetPositionRightBottom(this VisualElement element) =>
            element.style.right = element.style.bottom = StyleKeyword.Null;

        public static float GetPositionLeft(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.left : element.style.left.value.value;

        public static StyleLength SetPositionLeft(this VisualElement element, float left, LengthUnit unit = default, 
            bool delta = false) =>
            element.style.left = delta ? 
                new Length(element.style.left.value.value + left, unit) : new Length(left, unit);

        public static StyleLength ResetPositionLeft(this VisualElement element) => 
            element.style.left = StyleKeyword.Null;

        public static float GetPositionTop(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.top : element.style.top.value.value;

        public static StyleLength SetPositionTop(this VisualElement element, float top, LengthUnit unit = default, 
            bool delta = false) =>
            element.style.top = delta ? new Length(element.style.top.value.value + top, unit) : new Length(top, unit);

        public static StyleLength ResetPositionTop(this VisualElement element) =>
            element.style.top = StyleKeyword.Null;

        public static float GetPositionRight(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.right : element.style.right.value.value;

        public static StyleLength SetPositionRight(this VisualElement element, float right, LengthUnit unit = default, 
            bool delta = false) =>
            element.style.right = delta ? 
                new Length(element.style.right.value.value + right, unit) : new Length(right, unit);

        public static StyleLength ResetPositionRight(this VisualElement element) => 
            element.style.right = StyleKeyword.Null;

        public static float GetPositionBottom(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.bottom : element.style.bottom.value.value;

        public static StyleLength SetPositionBottom(this VisualElement element, float bottom, LengthUnit unit = default, 
            bool delta = false) => 
            element.style.bottom = delta ? 
                new Length(element.style.bottom.value.value + bottom, unit) : new Length(bottom, unit);

        public static StyleLength ResetPositionBottom(this VisualElement element) =>
            element.style.bottom = StyleKeyword.Null;
        #endregion Position
        
        #region Rotation
        public static float GetRotation(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.rotate.angle.value : element.style.rotate.value.angle.value;

        public static StyleRotate SetRotation(this VisualElement element, float angle, 
            AngleUnit unit = AngleUnit.Degree, bool delta = false) =>
            element.style.rotate =
                new Rotate(new Angle((delta ? element.style.rotate.value.angle.value : 0) + angle, unit));

        public static StyleRotate ResetRotation(this VisualElement element) =>
            element.style.rotate = StyleKeyword.Null;
        #endregion Rotation

        #region Scale
        public static Vector3 GetScale(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.scale.value : element.style.scale.value.value;

        public static StyleScale SetScale(this VisualElement element, Vector3 scale, bool delta = false) =>
                element.style.scale = new Scale((delta ? GetScaleVector(element) : Vector3.zero) + scale);

        private static Vector3 GetScaleVector(VisualElement element) =>
            element.style.scale == StyleKeyword.Null ? Vector3.one : element.style.scale.value.value;

        public static StyleScale ResetScale(this VisualElement element) => element.style.scale = StyleKeyword.Null;

        public static float GetUniformScale(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.scale.value.x : element.style.scale.value.value.x;

        public static StyleScale SetUniformScale(this VisualElement element, float scale, bool delta = false) =>
            SetScale(element, new Vector3(
                (delta ? GetScaleVector(element).x : 0f) + scale,
                (delta ? GetScaleVector(element).y : 0f) + scale,
                (delta ? GetScaleVector(element).z : 0f) + scale));

        public static float GetScaleX(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.scale.value.x : element.style.scale.value.value.x;

        public static StyleScale SetScaleX(this VisualElement element, float scaleX, bool delta = false) =>
            element.style.scale = 
                new Scale(new Vector3(
                    (delta ? GetScaleVector(element).x :  0f) + scaleX, 
                    GetScaleVector(element).y, 
                    GetScaleVector(element).z));


        public static float GetScaleY(this VisualElement element, bool resolved = false) => 
            resolved ? element.resolvedStyle.scale.value.y : element.style.scale.value.value.y;

        public static StyleScale SetScaleY(this VisualElement element, float scaleY, bool delta = false)=>
            element.style.scale = 
                new Scale(new Vector3(
                    GetScaleVector(element).x,
                    (delta ? GetScaleVector(element).y :  0f) + scaleY, 
                    GetScaleVector(element).z));
        #endregion Scale
        
        #endregion Transform
    }
}