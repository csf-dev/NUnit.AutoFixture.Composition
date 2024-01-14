using AutoFixture.Kernel;

namespace AutoFixture;

public class SingleBehaviourCustomization(ISpecimenBuilderTransformation transformation) : ICustomization
{
    private readonly ISpecimenBuilderTransformation transformation = transformation ?? throw new ArgumentNullException(nameof(transformation));

    public void Customize(IFixture fixture)
    {
        fixture.Behaviors.Add(transformation);
    }
}