using System;
using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture
{
    /// <summary>
    /// An AutoFixture behaviour (a <see cref="ISpecimenBuilderTransformation"/>) which applies a post-processing
    /// action to a specimen which was created to satisfy a specified parameter.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class is the heart of the NUnit/AutoFixture composition micro-library.
    /// It 'connects' an instance of <see cref="PostprocessingCommand{T}"/> with a an instance of
    /// <see cref="ParameterSpecification"/> in order to create and return an instance of
    /// <see cref="Postprocessor"/>. That returned post-processor will do whatever the current
    /// <see cref="ISpecimenBuilder"/> does, but additionally apply the builder customization
    /// (post-processing) action to the specimen identified by the parameter.
    /// </para>
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class ParameterPostprocessingTransformation<T> : ISpecimenBuilderTransformation where T : class
    {
        private readonly Action<T, ISpecimenContext> builderCustomizer;
        private readonly ParameterInfo parameter;

        /// <inheritdoc/>
        public ISpecimenBuilderNode Transform(ISpecimenBuilder builder)
        {
            var command = new PostprocessingCommand<T>(builderCustomizer);
            var spec = new ParameterSpecification(parameter.ParameterType, parameter.Name);
            return new Postprocessor(builder, command, spec);
        }

        /// <summary>
        /// Creates a new instance of <see cref="ParameterPostprocessingTransformation{T}"/>.
        /// </summary>
        /// <param name="builderCustomizer">The customization action which will post-process the specimen.</param>
        /// <param name="parameter">The parameter indicating the specimen which should be post-processed.</param>
        /// <exception cref="ArgumentNullException">If either parameter is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">If the <see cref="ParameterInfo.ParameterType"/> is not compatible with the generic type <typeparamref name="T"/>.</exception>
        public ParameterPostprocessingTransformation(Action<T, ISpecimenContext> builderCustomizer, ParameterInfo parameter)
        {
            this.builderCustomizer = builderCustomizer ?? throw new ArgumentNullException(nameof(builderCustomizer));
            this.parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));

            if (!typeof(T).GetTypeInfo().IsAssignableFrom(parameter.ParameterType))
                throw new ArgumentException("The parameter type must be assignable to the generic type for " +
                                            $"which {nameof(ParameterPostprocessingTransformation<object>)} was created.\n" +
                                            $"Parameter type : {parameter.ParameterType}\n" +
                                            $"Generic type   : {typeof(T)}",
                                            nameof(parameter));
        }
    }
}
