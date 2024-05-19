using System;
using AutoFixture.Kernel;

namespace AutoFixture
{
    /// <summary>
    /// An AutoFixture customization which applies a single behaviour to the fixture.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A behaviour is a specimen builder transformation, like a decorator object for the process of creating &amp; initialising
    /// a specimen.
    /// This customization very simply applies a single such behaviour to the fixture.
    /// </para>
    /// </remarks>
    public class SingleBehaviourCustomization : ICustomization
    {
        private readonly ISpecimenBuilderTransformation transformation;

        /// <inheritdoc/>
        public void Customize(IFixture fixture)
        {
            fixture.Behaviors.Add(transformation);
        }

        /// <summary>
        /// Creates a new instance of <see cref="SingleBehaviourCustomization"/>.
        /// </summary>
        /// <param name="transformation">The specimen builder transformation "behaviour" which will be applied by this customization.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="transformation"/> is <see langword="null" />.</exception>
        public SingleBehaviourCustomization(ISpecimenBuilderTransformation transformation)
        {
            this.transformation = transformation ?? throw new ArgumentNullException(nameof(transformation));
        }
    }
}