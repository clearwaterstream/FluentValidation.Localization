using FluentValidation;
using FluentValidation.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace clearwaterstream
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, IStringSource messageSource)
        {
            return rule.Configure(config =>
            {
                config.CurrentValidator.Options.ErrorMessageSource = messageSource;
            });
        }

        public static IRuleBuilderOptions<T, TProperty> WithMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, LocalizedString localizedString)
        {
            return WithMessage(rule, new LocalizedStringSource(localizedString));
        }
    }
}
