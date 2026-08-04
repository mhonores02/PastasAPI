using PastasAPI.Application.Models;
using PastasAPI.Application.Models.Requests;

namespace PastasAPI.Application.Interfaces;

public interface ICartService
{
    CartDto GetByClientId(int clientId);
    CartDto AddProduct(int clientId, AddProductToCartRequest request);
    CartDto RemoveProduct(int clientId, int productId);
    CartDto Checkout(int clientId);
}