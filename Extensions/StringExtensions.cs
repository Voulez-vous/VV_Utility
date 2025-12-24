using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace VV.Utility
{
    public enum VVStringComparison
    {
        CurrentCulture,
        CurrentCultureIgnoreCase,
        CurrentCultureIgnoreCaseNoSpecialChars,
        InvariantCulture,
        InvariantCultureIgnoreCase,
        InvariantCultureIgnoreCaseNoSpecialChars,
        Ordinal,
        OrdinalIgnoreCase,
        OrdinalIgnoreCaseNoSpecialChars,
    }
    
    public static class StringExtensions
    {
        private const float Epsilon = 0.0001f;

        /// <summary>
        ///  Equals with CurrentCulture
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsCC(this string text, string other) =>
            text.Equals(other, VVStringComparison.CurrentCulture);
        
        /// <summary>
        ///  Equals with CurrentCultureIgnoreCase
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsCC_IC(this string text, string other) =>
            text.Equals(other, VVStringComparison.CurrentCultureIgnoreCase);
        
        /// <summary>
        ///  Equals with CurrentCultureIgnoreCaseNoSpecialChars
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsCC_IC_NS(this string text, string other) =>
            text.Equals(other, VVStringComparison.CurrentCultureIgnoreCaseNoSpecialChars);
        
        /// <summary>
        ///  Equals with InvariantCulture
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsIC(this string text, string other) =>
            text.Equals(other, VVStringComparison.InvariantCulture);
        
        /// <summary>
        ///  Equals with InvariantCultureIgnoreCase
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsIC_IC(this string text, string other) =>
            text.Equals(other, VVStringComparison.InvariantCultureIgnoreCase);
        
        /// <summary>
        ///  Equals with InvariantCultureIgnoreCaseNoSpecialChars
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsIC_IC_NS(this string text, string other) =>
            text.Equals(other, VVStringComparison.InvariantCultureIgnoreCaseNoSpecialChars);
        
        /// <summary>
        ///  Equals with Ordinal
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsO(this string text, string other) =>
            text.Equals(other, VVStringComparison.Ordinal);
        
        /// <summary>
        ///  Equals with OrdinalIgnoreCase
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsO_IC(this string text, string other) =>
            text.Equals(other, VVStringComparison.OrdinalIgnoreCase);
        
        /// <summary>
        ///  Equals with OrdinalIgnoreCaseNoSpecialChars
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsO_IC_NS(this string text, string other) =>
            text.Equals(other, VVStringComparison.OrdinalIgnoreCaseNoSpecialChars);
            
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(this string text, string other, VVStringComparison comparison)
        {
            const string noSpecialCharsExt = "NoSpecialChars";
            string comp = comparison.ToString();
            bool noSpecialChars = comp.Contains(noSpecialCharsExt);
            string comparisonString = comp[..comp.IndexOf(noSpecialCharsExt)];
            string _text = noSpecialChars ? text.NoSpecialChars() : text;
            string _other = noSpecialChars ? other.NoSpecialChars() : other;
            return _text.Equals(_other, Enum.Parse<StringComparison>(comparisonString));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NoSpecialChars(this string text) => GetWithoutDiacritics(text);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetWithoutDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}