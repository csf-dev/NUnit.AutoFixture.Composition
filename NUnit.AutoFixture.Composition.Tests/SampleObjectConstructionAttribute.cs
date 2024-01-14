using System.Reflection;
using AutoFixture.NUnit3;

namespace AutoFixture;

public class SampleObjectConstructionAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter)
        => new SampleObjectConstructionCustomization();
}