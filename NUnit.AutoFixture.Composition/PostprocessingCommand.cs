using System;
using AutoFixture.Kernel;

namespace AutoFixture
{
    public class PostprocessingCommand<T> : ISpecimenCommand where T : class
    {
        private readonly Action<T, ISpecimenContext> builderCustomizer;

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