// --------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
            length -= 3;
            value = value ?? string.Empty;
            value = value.Trim();
            value = value.Length > length ? value.Substring(0, length) + "..." : value;

            return value;
        }
    }
}
