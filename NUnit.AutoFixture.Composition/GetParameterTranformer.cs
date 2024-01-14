using System;
using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture
{
    /// <summary>
    /// A static helper class to get instances of <see cref="ICustomization"/> and/or <see cref="ParameterPostprocessingTransformation{T}"/>
    /// for post-processing a specimen which matches a specified parameter.
    /// </summary>
    public static class GetParameterTranformer
    {
        /// <summary>
        /// Gets a specimen builder transformation (an AutoFixture behaviour) for an instance of a specified generic type, for a specified parameter,
        /// which performs the specified post-processing action.
        /// </summary>
        /// <typeparam name="T">The type of the specimen to be post-processed.</typeparam>
        /// <param name="parameter">The parameter through which the specimen is to be initialised by AutoFixture</param>
        /// <param name="builderCustomizer">A post-processing action.</param>
        /// <returns>An AutoFixture behaviour instance.</returns>
        public static ISpecimenBuilderTransformation ForType<T>(ParameterInfo parameter, Action<T> builderCustomizer) where T : class
            => ForType<T>(parameter, (x, _) => builderCustomizer(x));

        /// <summary>
        /// Gets a specimen builder transformation (an AutoFixture behaviour) for an instance of a specified generic type, for a specified parameter,
        /// which performs the specified post-processing action.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This overload provides for a post-processing action which includes the AutoFixture specimen context.
        /// </para>
        /// </remarks>
        /// <typeparam name="T">The type of the specimen to be post-processed.</typeparam>
        /// <param name="parameter">The parameter through which the specimen is to be initialised by AutoFixture</param>
        /// <param name="builderCustomizer">A post-processing action.</param>
        /// <returns>An AutoFixture behaviour instance.</returns>
        public static ISpecimenBuilderTransformation ForType<T>(ParameterInfo parameter, Action<T, ISpecimenContext> builderCustomizer) where T : class
            => new ParameterPostprocessingTransformation<T>(builderCustomizer, parameter);

        /// <summary>
        /// Provides functionality to get an AutoFixture <see cref="ICustomization"/> for a specified parameter &amp; post-processing action.
        /// </summary>
        public static class Customization
        {
            /// <summary>
            /// Gets an AutoFixture <see cref="ICustomization"/> for an instance of a specified generic type, for a specified parameter,
            /// which performs the specified post-processing action.
            /// </summary>
            /// <typeparam name="T">The type of the specimen to be post-processed.</typeparam>
            /// <param name="parameter">The parameter through which the specimen is to be initialised by AutoFixture</param>
            /// <param name="builderCustomizer">A post-processing action.</param>
            /// <returns>An AutoFixture customization.</returns>
            public static ICustomization ForType<T>(ParameterInfo parameter, Action<T> builderCustomizer) where T : class
                => ForType<T>(parameter, (x, _) => builderCustomizer(x));

            /// <summary>
            /// Gets an AutoFixture <see cref="ICustomization"/> for an instance of a specified generic type, for a specified parameter,
            /// which performs the specified post-processing action.
            /// </summary>
            /// <remarks>
            /// <para>
            /// This overload provides for a post-processing action which includes the AutoFixture specimen context.
            /// </para>
            /// </remarks>
            /// <typeparam name="T">The type of the specimen to be post-processed.</typeparam>
            /// <param name="parameter">The parameter through which the specimen is to be initialised by AutoFixture</param>
            /// <param name="builderCustomizer">A post-processing action.</param>
            /// <returns>An AutoFixture customization.</returns>
            public static ICustomization ForType<T>(ParameterInfo parameter, Action<T, ISpecimenContext> builderCustomizer) where T : class
            {
                var behaviour = GetParameterTranformer.ForType(parameter, builderCustomizer);
                return new SingleBehaviourCustomization(behaviour);
            }

        }
    }
}