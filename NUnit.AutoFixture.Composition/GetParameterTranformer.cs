using System;
using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture
{
    public static class GetParameterTranformer
    {
        public static ISpecimenBuilderTransformation ForType<T>(ParameterInfo parameter, Action<T> builderCustomizer) where T : class
            => ForType<T>(parameter, (x, _) => builderCustomizer(x));

        public static ISpecimenBuilderTransformation ForType<T>(ParameterInfo parameter, Action<T, ISpecimenContext> builderCustomizer) where T : class
            => new ParameterPostprocessingTransformation<T>(builderCustomizer, parameter);

        public static class Customization
        {
            public static ICustomization ForType<T>(ParameterInfo parameter, Action<T> builderCustomizer) where T : class
                => ForType<T>(parameter, (x, _) => builderCustomizer(x));

            public static ICustomization ForType<T>(ParameterInfo parameter, Action<T, ISpecimenContext> builderCustomizer) where T : class
            {
                var behaviour = GetParameterTranformer.ForType(parameter, builderCustomizer);
                return new SingleBehaviourCustomization(behaviour);
            }

        }
    }
}