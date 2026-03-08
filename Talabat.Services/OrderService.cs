 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entity;
using Talabat.Core.Entity.Order_Aggregate;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Talabat.Core.Specifications.Order_Spec;

namespace Talabat.Services
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPaymentService _paymentService;

		public OrderService(IBasketRepository basketRepository
			    , IUnitOfWork unitOfWork 
			    , IPaymentService paymentService )
        {
			_basketRepository = basketRepository;
			_unitOfWork = unitOfWork;
			_paymentService = paymentService;
		}
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int DeliveryMethodId, Address ShippingAddress)
		{
			//Get Basket From Basket Repo
			var Basket =await _basketRepository.GetBasketAsync(basketId);
			//Get Selected Items at Basket From Product Repo
			var OrderItems = new List<OrderItem>();
			if (Basket?.Items.Count > 0)
			{
				foreach (var item in Basket.Items) {
					var Product =await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
					var ProductItemOrdered = new ProductItemOrdered(Product.Id ,Product.Name ,Product.PictureUrl);
					var OrderItem = new OrderItem(ProductItemOrdered, item.Quantity, Product.Price);
				    OrderItems.Add(OrderItem);
				}
			}
			// Calculate SubTotal
			var SubTotal = OrderItems.Sum(item => item.price * item.Quantity);
			// Get Delivery Method From DeliveryMethod Repo
			var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
			// Create Order
			var Spec = new OrderWithPaymentIntentSpec(Basket.PaymentIntendId);
			var ExOrder =await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(Spec);
			if (ExOrder is not null) {
				_unitOfWork.Repository<Order>().Delete(ExOrder);
			 await	_paymentService.CreateOrUpdatePaymentIntend(basketId);

			}


			var Order = new Order(buyerEmail , ShippingAddress, DeliveryMethod, OrderItems ,SubTotal, Basket.PaymentIntendId);
			// Add Order Locally
		    await _unitOfWork.Repository<Order>().AddAsync(Order);
			//Save Order To Database[ToDo]
		var Result =await  _unitOfWork.CompleteAsync();
			if (Result <= 0) return null;
			return Order;



		}

		public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
		{
			var DeliveryMethods =await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
			return DeliveryMethods;
		}

		public Task<Order> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
		{
			var Spec = new OrderSpecifications(buyerEmail ,orderId);
			var Order = _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(Spec);
			return Order;
		}

		public Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string buyerEmail)
		{
			var Spec = new OrderSpecifications(buyerEmail);
			var Orders = _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);
			return Orders;
		}
	}
}
