namespace Messenger.Infrastructure
{
    public static class MessageConstrains
    {
        public const int SenderMaxStringLength = IdentityConstrains.MaxStringLength;
        public const int ReceiverMaxStringLength = IdentityConstrains.MaxStringLength;
        public const int NameMinStringLength = 1;
        public const int ThemeMaxStringLength = 255;
        public const int BodyMaxStringLength = 1000;
    }
}
