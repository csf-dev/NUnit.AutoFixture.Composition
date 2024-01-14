using System;
using AutoFixture.Kernel;

namespace AutoFixture
{
    public class SingleBehaviourCustomization : ICustomization
    {
        private readonly ISpecimenBuilderTransformation transformation;

        public void Customize(IFixture fixture)
        {
            fixture.Behaviors.Add(transformation);
        }

        public SingleBehaviourCustomization(ISpecimenBuilderTransformation transformation)
        {
            this.transformation = transformation ?? throw new ArgumentNullException(nameof(transformation));
        }
    }
}