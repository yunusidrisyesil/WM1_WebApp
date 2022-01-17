﻿using AutoMapper;
using ItServiceApp.Models.Payment;
using Iyzipay.Model;

namespace ItServiceApp.MapperProfiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<InstallmentPriceModel,InstallmentPrice>().ReverseMap();
            CreateMap<InstallmentModel, InstallmentDetail>().ReverseMap();
            CreateMap<CardModel,PaymentCard>().ReverseMap();
            CreateMap<BasketModel,BasketItem>().ReverseMap();
            CreateMap<AddressModel,Address>().ReverseMap();
            CreateMap<CustomerModel,Buyer>().ReverseMap();
            CreateMap<PaymentResponseModel,Payment>().ReverseMap();
        }
    }
}
