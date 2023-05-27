using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    internal static partial class ConsoleCore
    {
        private static class TokenChecker
        {
            public static bool IsIntValid(string token, out object value)
            {
                bool valid = int.TryParse(token, out int output);
                value = output;

                return valid;
            }

            public static bool IsFloatValid(string token, out object value)
            {
                bool valid = float.TryParse(token, out float output);
                value = output;

                return valid;
            }

            public static bool IsBoolValid(string token, out object value)
            {
                token = token.ToLower();
                value = token == "true";

                return token == "false" || token == "true";
            }

            public static bool IsVector3Valid(string token, out object value)
            {
                string[] argsTokens = token.Split(',');
                value = new Vector3();

                if (argsTokens.Length != 3)
                    return false;

                if (float.TryParse(argsTokens[0], out float x) && float.TryParse(argsTokens[1], out float y) && float.TryParse(argsTokens[2], out float z))
                {
                    value = new Vector3(x, y, z);
                    return true;
                }

                return false;
            }
        }
    }
}
