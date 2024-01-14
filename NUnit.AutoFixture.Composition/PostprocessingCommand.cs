using AutoFixture.Kernel;

namespace AutoFixture;

public class PostprocessingCommand<T>(Action<T, ISpecimenContext> builderCustomizer) : ISpecimenCommand where T : class
{
    private readonly Action<T, ISpecimenContext> builderCustomizer = builderCustomizer ?? throw new ArgumentNullException(nameof(builderCustomizer));

    public void Execute(object specimen, ISpecimenContext context)
    {
        var builder = (T)specimen;
        builderCustomizer(builder, context);
    }
}