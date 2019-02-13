﻿using clearwaterstream;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Xunit;

namespace FluentValidation.Localization.Test
{
    public class LocalizedStringSourceTest
    {
        class Student
        {
            public string Name { get; set; }
            public double GPA { get; set; }
        }

        class StudentValidator : AbstractValidator<Student>
        {
            public StudentValidator()
            {
                RuleFor(x => x.Name).MinimumLength(3).WithMessage(
                    "Name must be at least 3 characters long".Localize()
                    .InFrench("Le name must be at least 3 characters long")
                    .InCanadianFrench("Le name must be at least 3 characters long, éh")
                    .In("de-DE", "Das name must be at least 3 characters long"));

                RuleFor(x => x.GPA).Must(x => x >= 0 && x <= 4.0).WithMessage(
                    "Must be from 0 to 4".Localize()
                    .InFrench("Must be from 0 to 4, comprendre?")
                    .InCanadianFrench("Must be from 0 to 4, s'il vous plaît")
                    .In("de-DE", "Must be from 0 to 4, verstehen?"));
            }
        }

        [Fact]
        public void TestValidator()
        {
            var s = new Student()
            {
                Name = "JT",
                GPA = 4.1
            };

            var v = new StudentValidator();

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(string.Empty); // invariant

            var r = v.Validate(s);

            Assert.Equal(2, r.Errors.Count);

            Assert.Equal("Name must be at least 3 characters long", r.Errors[0].ErrorMessage);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

            r = v.Validate(s);

            Assert.Equal(2, r.Errors.Count);

            Assert.Equal("Das name must be at least 3 characters long", r.Errors[0].ErrorMessage);
        }
    }
}
