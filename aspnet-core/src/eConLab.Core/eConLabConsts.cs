using eConLab.Debugging;

namespace eConLab
{
    public class eConLabConsts
    {
        public const string LocalizationSourceName = "eConLab";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "0aecb317e0d843a8abe3ab18e95569c9";
    }
}
