using FluentValidation;
using FluentValidation.Resources;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace clearwaterstream
{
    /// <summary>
    /// Enables the use of  <see cref="LocalizedString"/> in FluentValidation
    /// </summary>
    public class LocalizedStringSource : IStringSource
    {
        readonly LocalizedString _localizedString = null;

        public LocalizedStringSource(LocalizedString localizedString)
        {
            _localizedString = localizedString;
        }

        public string GetString(IValidationContext context)
        {
            return _localizedString?.ToString();
        }

        public string ResourceName => null;
        public Type ResourceType => null;
    }
}
