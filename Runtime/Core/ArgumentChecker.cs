using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    internal static partial class ConsoleCore
    {
        private const string COLOR_PREFIX = "Color(";

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

            public static bool IsVector4Valid(string token, out object value)
            {
                value = null;

                if (!IsVectorValid(token, out Vector4 vector))
                    return false;

                value = vector;

                return true;
            }

            public static bool IsVector3Valid(string token, out object value)
            {
                value = null;

                if (!IsVectorValid(token, out Vector4 vector))
                    return false;

                value = new Vector3(
                    vector.x,
                    vector.y,
                    vector.z
                );

                return true;
            }

            public static bool IsVector2Valid(string token, out object value)
            {
                value = null;

                if (!IsVectorValid(token, out Vector4 vector))
                    return false;

                value = new Vector2(
                    vector.x,
                    vector.y
                );

                return true;
            }

            /// <summary>
            /// Usage : Color(Vector)
            /// </summary>
            public static bool IsColorValid(string token, out object value)
            {
                value = null;

                if (!IsPrefixValid(token, COLOR_PREFIX, out string convertedToken))
                    return false;

                string[] args = token.Split(',');
                Vector4 colorVec;

                if (args.Length == 3)
                {
                    if (!IsVector3Valid(convertedToken, out value))
                        return false;

                    colorVec = (Vector3)value;
                    value = new Color(colorVec.x, colorVec.y, colorVec.z);
                    return true;
                }

                if (!IsVector4Valid(convertedToken, out value))
                    return false;

                colorVec = (Vector4)value;
                value = new Color(colorVec.x, colorVec.y, colorVec.z, colorVec.w);

                return true;
            }

            private static bool IsVectorValid(string token, out Vector4 vector)
            {
                string[] argsTokens = token.Split(',');
                vector = new Vector4();

                for (int i = 0; i < argsTokens.Length; i++)
                {
                    if (!float.TryParse(argsTokens[i], out float dimValue))
                        return false;

                    vector[i] = dimValue;
                }

                return true;
            }

            private static bool IsPrefixValid(string token, string prefix, out string convertedToken)
            {
                convertedToken = null;
                string tokenPrefix = token.Substring(0, prefix.Length);

                if (prefix != COLOR_PREFIX)
                    return false;

                convertedToken = token.Substring(prefix.Length);
                convertedToken = convertedToken.Split(')')[0];

                return true;
            }
        }
    }
}
