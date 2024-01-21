# NUnit.AutoFixture.Composition

This is a micro-framework for composable `CustomizeAttribute` in [AutoFixture.NUnit].
It originated with [this StackOverflow question/answer].

[AutoFixture.NUnit]: https://www.nuget.org/packages/AutoFixture.NUnit3/
[this StackOverflow question/answer]: https://stackoverflow.com/a/60560919/6221779

## What this library does

If you are using [NUnit 3], [AutoFixture] and [the integration between these frameworks] then it is likely that you are also writing your own [`CustomiseAttribute`] implementations to configure _how the AutoFixture specimen is created_.

What's slightly more difficult with just the libraries above is writing customize attributes _which may be composed_.
Customizations written using the following style of syntax _cannot be easily composed via separate classes_ this customization hook controls the initial creation of the specimen.
Only one such customisation can logically be used - you may only _create_ any given object once.

```csharp
fixture.Customize<MyType>(c => c.With(x => x.SomeProperty, "Some value"));
```

That lack of composition makes it difficult to write customize attributes which may be combined/composed, such that multiple attributes are all effective upon a single parameter.
This micro-framework provides a quick mechanism to solve this issue by providing a shorthand syntax for writing [AutoFixture behaviours]

[NUnit 3]: https://nunit.org/
[AutoFixture]: https://autofixture.github.io/
[the integration between these frameworks]: https://www.nuget.org/packages/AutoFixture.NUnit3
[`CustomiseAttribute`]: https://github.com/AutoFixture/AutoFixture/blob/master/Src/AutoFixture.NUnit3/CustomizeAttribute.cs
[AutoFixture behaviours]: https://stackoverflow.com/a/60560919/6221779

## Using this library

Write your [`CustomiseAttribute`] types like the following example; do not write an `ICustomization` class directly.

```csharp
public class WithMyPropertyAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter)
        => GetParameterTranformer.Customization.ForType<SampleObject>(parameter, x => x.MyProperty = "My value");
}
```

In this example, we customize an instance of `SampleObject` to have its `MyProperty` value set to the value "My value".
The example above is also configured to only apply this behaviour when the specimen is requested via a matching parameter, useful where the specimen is to be parameter-injection into a test.
As with any other attributes, this could be parameterized by accepting constructor parameters and/or property setters in the attribute definition.

The core of the library is the `GetParameterTranformer` static class.
It gets an `ICustomization` instance for post-processing a specimen.
For advanced uses, use the methods which gets only an `ISpecimenBuilderTransformation`, which may then be manipulated as desired.

Each method also contains overloads which provide access to an `ISpecimenContext` in the customization callback, providing access back to AutoFixture's infrastructure.
This could be useful to re-invoke AutoFixture functionality in order to create further specimens where needed.
