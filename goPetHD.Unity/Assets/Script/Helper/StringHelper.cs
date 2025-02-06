namespace goPetHD.Helper
{
    public static class StringHelper
    {
        public const string UsernamePassRegex = @"^[a-zA-Z0-9]{5,20}$";

        public static bool IsUsernamePassValid(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, UsernamePassRegex);
        }
    }
}