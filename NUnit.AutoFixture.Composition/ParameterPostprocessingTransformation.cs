using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture;

public class ParameterPostprocessingTransformation<T> : ISpecimenBuilderTransformation where T : class
{
    private readonly Action<T, ISpecimenContext> builderCustomizer;
    private readonly ParameterInfo parameter;

    public ParameterPostprocessingTransformation(Action<T, ISpecimenContext> builderCustomizer, ParameterInfo parameter)
    {
        this.builderCustomizer = builderCustomizer ?? throw new ArgumentNullException(nameof(builderCustomizer));
        this.parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        
        if (!parameter.ParameterType.IsAssignableTo(typeof(T)))
            throw new ArgumentException("The parameter type must be assignable to the generic type for " +
                                        $"which {nameof(ParameterPostprocessingTransformation<object>)} was created.\n" +
                                        $"Parameter type : {parameter.ParameterType}\n" +
                                        $"Generic type   : {typeof(T)}",
                                        nameof(parameter));
    }

    public ISpecimenBuilderNode Transform(ISpecimenBuilder builder)
    {
        var command = new PostprocessingCommand<T>(builderCustomizer);
        var spec = new ParameterSpecification(parameter.ParameterType, parameter.Name);
        return new Postprocessor(builder, command, spec);
    }
}
