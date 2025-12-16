using System;

namespace PureMediator.Net.Abstractions.Pipeline
{
    /// <summary>
    /// Defines a validator for validating instances of a specified type.
    /// Implement this interface to provide custom validation logic for requests or other objects.
    /// </summary>
    /// <typeparam name="T">The type of object to validate.</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        void Validate(T instance);
    }
}
