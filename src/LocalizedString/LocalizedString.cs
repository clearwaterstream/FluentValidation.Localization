/* Igor Krupin
 * https://github.com/clearwaterstream/LocalizedString
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace clearwaterstream
{
    public class LocalizedString : IEquatable<LocalizedString>, IEquatable<string>, IComparable, IComparable<LocalizedString>, IComparable<string>, ICloneable
    {
        static readonly string _defaultCultureName = string.Empty; // invariant
        readonly Dictionary<string, string> valueByCulture = new Dictionary<string, string>();

        public LocalizedString(string localizedValue)
        {
            this[_defaultCultureName] = localizedValue;
        }

        public LocalizedString(string cultureName, string localizedValue)
        {
            if (string.IsNullOrEmpty(cultureName))
                cultureName = _defaultCultureName;

            this[cultureName] = localizedValue;
        }

        private LocalizedString() { }

        public string this[string cultureName]
        {
            get
            {
                if (cultureName == null)
                    cultureName = _defaultCultureName;

                if (valueByCulture.TryGetValue(cultureName, out string localizedValue))
                    return localizedValue;
                // if we cannot find the value based on the requested culture, try to fallback on the default culture ...
                else if (valueByCulture.TryGetValue(_defaultCultureName, out string fallback))
                    return fallback;
                else
                    return null;
            }
            set
            {
                if (cultureName == null)
                    cultureName = _defaultCultureName;

                valueByCulture[cultureName] = value;
            }
        }

        public override string ToString()
        {
            return ToString(Thread.CurrentThread.CurrentCulture);
        }

        public string ToString(CultureInfo culture)
        {
            var cultureName = culture?.Name ?? null;

            return this[cultureName];
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj == null)
                return Equals((string)null);

            if (obj is string)
                return Equals((string)obj);

            if (obj is LocalizedString)
                return Equals(obj as LocalizedString);

            return false;
        }

        public bool Equals(string other)
        {
            return string.Compare(ToString(), other) == 0;
        }

        public bool Equals(string other, StringComparison comparisonType)
        {
            return string.Compare(ToString(), other, comparisonType) == 0;
        }

        public bool Equals(LocalizedString other)
        {
            return Equals(other?.ToString());
        }

        public bool Equals(LocalizedString other, StringComparison comparisonType)
        {
            return Equals(other?.ToString(), comparisonType);
        }

        public int CompareTo(string other)
        {
            return string.Compare(ToString(), other);
        }

        public int CompareTo(LocalizedString other)
        {
            return CompareTo(other?.ToString());
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return string.Compare(ToString(), null);

            if (obj is string)
                return CompareTo((string)obj);

            if(obj is LocalizedString)
                return CompareTo(obj as LocalizedString);

            return 1;
        }

        public override int GetHashCode()
        {
            var str = ToString();

            if (str == null)
                return string.Empty.GetHashCode();

            return str.GetHashCode();
        }

        public static explicit operator string(LocalizedString localizedString)
        {
            return localizedString.ToString();
        }

        public object Clone()
        {
            var newInst = new LocalizedString();

            foreach(var kvp in valueByCulture)
            {
                newInst.valueByCulture.Add(kvp.Key, kvp.Value);
            }

            return newInst;
        }
    }
}
