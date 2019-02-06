using System;
using System.Collections.Generic;
using System.Text;

namespace clearwaterstream
{
    public static class LocalizedStringExtensions
    {
        public static T And<T>(this T localizedString, string cultureName, string localizedValue) where T : LocalizedString
        {
            if (localizedString == null)
                return localizedString;

            localizedString[cultureName] = localizedValue;

            return localizedString;
        }

        public static T Invariant<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return And(localizedString, string.Empty, localizedValue);
        }

        public static T InEnglish<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return And(localizedString, "en-US", localizedValue);
        }

        public static T InQueensEnglish<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return And(localizedString, "en-GB", localizedValue);
        }

        public static T InCanadianEnglish<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return And(localizedString, "en-CA", localizedValue);
        }

        public static T InCanadianFrench<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return And(localizedString, "fr-CA", localizedValue);
        }

        public static T InFrench<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return And(localizedString, "fr-FR", localizedValue);
        }
    }
}
