using System.Reflection;
using AutoFixture.NUnit3;

namespace AutoFixture;

public class WithCityAttribute(string city) : CustomizeAttribute
{
    private readonly string city = city;

    public override ICustomization GetCustomization(ParameterInfo parameter)
        => GetParameterTranformer.Customization.ForType<SampleObject>(parameter, x => x.City = city);
}