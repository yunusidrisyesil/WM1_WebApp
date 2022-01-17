using ItServiceApp.Models.Payment;

namespace ItServiceApp.Services
{
    public interface IPaymentService
    {
        public PaymentResponseModel Pay(PaymentModel model);
        public InstallmentModel CheckInstallments(string binNumber,decimal price);
    }
}
