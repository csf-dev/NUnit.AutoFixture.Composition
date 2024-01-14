using System;
using AutoFixture.Kernel;

namespace AutoFixture
{
    /// <summary>
    /// An AutoFixture specimen command which applies a post-processing action to a specimen of the
    /// appropriate generic type.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class does the work of applying the actual builder customizer (the post-procesing action) to the specimen.
    /// </para>
    /// <para>
    /// Instances of this class are expected to be created &amp; consumed automatically by
    /// <see cref="ParameterPostprocessingTransformation{T}"/>
    /// </para>
    /// </remarks>
    /// <typeparam name="T">The expected/required type of the specimen</typeparam>
    public class PostprocessingCommand<T> : ISpecimenCommand where T : class
    {
        private readonly Action<T, ISpecimenContext> builderCustomizer;

        /// <inheritdoc/>
        public void Execute(object specimen, ISpecimenContext context)
        {
            var builder = (T)specimen;
            builderCustomizer(builder, context);
        }

        public PostprocessingCommand(Action<T, ISpecimenContext> builderCustomizer)
        {
            this.builderCustomizer = builderCustomizer ?? throw new ArgumentNullException(nameof(builderCustomizer));
        }
    }
}