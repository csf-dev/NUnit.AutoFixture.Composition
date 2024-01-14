using System.Reflection;
using AutoFixture.NUnit3;

namespace AutoFixture;

public class WithNameAttribute(string name) : CustomizeAttribute
{
    private readonly string name = name;

    public override ICustomization GetCustomization(ParameterInfo parameter)
        => GetParameterTranformer.Customization.ForType<SampleObject>(parameter, x => x.Name = name);
}
