using System.Diagnostics.CodeAnalysis;

namespace DealFinder.Core.Communication
{
    [ExcludeFromCodeCoverage]
    public class ErrorCodes
    {
        public static int DatabaseError = 1;
        public static int UnknownAuthenticator = 2;
        public static int AuthenticatorError = 3;
        public static int UserAlreadyExists = 4;
        public static int InvalidGuidProvided = 5;
        public static int DealCreationCooldownActive = 6;
        public static int ValidationError = 7;
        public static int DealNotFound = 8;
    }
}