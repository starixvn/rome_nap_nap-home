// --------------------------------------------------------------------------
// <copyright file="StringExtensions.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.28">Create</change>
// </history>

namespace Fanex.NAP.Home.Core.Helpers
{
    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if the string is longer than expected length then truncates it with '...' at the end of string.
        /// Otherwise, keeps it as it be.
        /// </summary>
        /// <param name="value">The value that needs to be truncated.</param>
        /// <param name="length">The expected length.</param>
        /// <returns>Truncated string.</returns>
        public static string Truncate(this string value, int length)
        {
#pragma warning disable S1226 // Method parameters and caught exceptions should not be reassigned
            const string ect = "...";
            length -= ect.Length;
            value = value ?? string.Empty;
            value = value.Trim();
            value = value.Length > length ? value.Substring(0, length) + "..." : value;
#pragma warning restore S1226 // Method parameters and caught exceptions should not be reassigned

            return value;
        }

        /// <summary>
        /// Trims a string if it is not null or returns an empty string.
        /// </summary>
        /// <param name="value">The value that needs to be trimmed.</param>
        /// <returns>The trimmed string.</returns>
        public static string TrimOrEmpty(string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
        }
    }
}
