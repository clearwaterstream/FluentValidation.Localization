# FluentValidation.Localization
A helper library for [FluentValidation](https://github.com/JeremySkinner/FluentValidation) that allows for easy declaration of error messages in various languages without the use of resource files. When ValidationResult is produced, the value of `Thread.CurrentThread.CurrentCulture` will be used to resolve the translations for the `ErrorMessage` fields.

Useful for short strings. If strings are getting longer -- consider switching to time-tested technique of using resource files.

The library uses LocalizedString, get more info [here](https://github.com/clearwaterstream/LocalizedString)

## Example:

```csharp
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

var student = new Student()
{
    Name = "TJ",
    GPA = 4.1
};

var validator = new StudentValidator();

Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(string.Empty); // invariant

var validationResult = validator.Validate(student);

Console.WriteLine(validationResult.Errors[0].ErrorMessage); // Name must be at least 3 characters long

Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

validationResult = validator.Validate(student);

Console.WriteLine(validationResult.Errors[0].ErrorMessage); // Das name must be at least 3 characters long
```

### - Enjoy Responsibly -
