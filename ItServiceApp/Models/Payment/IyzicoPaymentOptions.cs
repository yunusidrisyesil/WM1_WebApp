using Iyzipay;

namespace ItServiceApp.Models.Payment
{
    public class IyzicoPaymentOptions : Options
    {
        public const string Key = "IyzicoOptions";

        public string ThreedCallbackUrl { get; set; }

    }
}
