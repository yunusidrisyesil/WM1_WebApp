﻿using AutoMapper;
using ItServiceApp.Models.Payment;
using Microsoft.Extensions.Configuration;

namespace ItServiceApp.Services
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IyzicoPaymentOptions _options;
        private readonly IMapper _mapper;

        public IyzicoPaymentService(IConfiguration configuration,IyzicoPaymentOptions options,IMapper mapper)
        {
            _configuration = configuration;
            _options = options;
            _mapper = mapper;
            var section = _configuration.GetSection(IyzicoPaymentOptions.Key);
            _options = new IyzicoPaymentOptions()
            {
                ApiKey = section["ApiKey"],
                SecretKey = section["SecretKey"],
                BaseUrl = section["BaseUrl"],
                ThreedCallbackUrl = section["ThreedsCallbackUrl"]
            };
        }

        public InstallmentModel CheckInstallments(string binNumber, decimal price)
        {
            throw new System.NotImplementedException();
        }

        public PaymentResponseModel Pay(PaymentModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
