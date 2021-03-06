using System;

namespace PL.DataAccess;

public class ExecuteException : Exception
{
    /// <summary>
    /// Initializes a new instance of the System.Exception class.
    /// </summary>
    public ExecuteException() : base() { }

    /// <summary>
    /// Initializes a new instance of the System.Exception class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ExecuteException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the System.Exception class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
    public ExecuteException(string? message, Exception? innerException) : base(message, innerException) { }
}
