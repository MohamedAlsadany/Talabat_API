using AutoMapper;
using Talabat.Core.Entity;
using Talabat.Core.Entity.Identity;
using Talabat.Core.Entity.Order_Aggregate;
using Talabat_APIs.DTOs;
using OrderAddress = Talabat.Core.Entity.Order_Aggregate.Address;
using IdentityAddress = Talabat.Core.Entity.Identity.Address;

namespace Talabat_APIs.Helpers
{
	public class MappingProfiles :Profile
	{
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                     .ForMember(P=>P.ProductBrand ,O=>O.MapFrom(S=>S.ProductBrand.Name))
                     .ForMember(P=>P.ProductType ,O=>O.MapFrom(S=>S.ProductType.Name))
                     .ForMember(P=>P.PictureUrl ,O=>O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<IdentityAddress, AddressDto>().ReverseMap();
            CreateMap<AddressDto , OrderAddress>().ReverseMap();
			CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d =>d.DeliveryMethod ,O=>O.MapFrom(S=>S.DeliveryMethod.ShortName))
				.ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()

                .ForMember(d=>d.ProductId ,O=>O.MapFrom(S=>S.product.ProductId))
				.ForMember(d => d.ProductName, O => O.MapFrom(S => S.product.ProductName))
				.ForMember(d => d.PictureUrl, O => O.MapFrom(S => S.product.PictureUrl))
				.ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());

		}
    }
}
