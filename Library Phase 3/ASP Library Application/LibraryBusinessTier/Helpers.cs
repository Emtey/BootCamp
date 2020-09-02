using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication
{
    /// <summary>
    /// Helpers class contains some basic helpers that all
    /// classes and forms are going to need.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Checks a string for numerics.
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>True if numeric is found, False otherwise</returns>
        public static bool IsNumeric(string s)
        {
            bool ok = false;
            foreach (char c in s.Trim())
            {
                if (c == '1' || c == '2' || c == '3' || c == '4' || c == '5' ||
                    c == '6' || c == '7' || c == '8' || c == '9' || c == '0')
                    ok = true;
                else
                    ok = false;
            }
            return ok;
        }
        /// <summary>
        /// Takes a string and validates that it can be converted to a integer value
        /// </summary>
        /// <param name="input">string value</param>
        /// <returns>True if can be an Integer, false if it cant.</returns>
        public static bool StringToIntVal(string input)
        {
            try
            {
                int x = Convert.ToInt32(input);
                if (x == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// takes a string value and validates that it can be converted to a short value.
        /// </summary>
        /// <param name="input">string value</param>
        /// <returns>True if can be an short, false if it cant.</returns>
        public static bool StringToShortVal(string input)
        {
            try
            {
                int x = Convert.ToInt16(input);
                if (x == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
