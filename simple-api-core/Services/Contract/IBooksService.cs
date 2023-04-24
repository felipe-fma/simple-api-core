using simple_api_core.DTO.Request;
using simple_api_core.DTO.Response;

namespace simple_api_core.Services.Interface;
public interface IBooksService
{
    public IEnumerable<BookResponse> GetAll();

    public BookResponse GetById(int id);

    public IEnumerable<BookResponse> GetByParameters(BooksQueryParameters parameters);

    public ShippingResponse ShippingPrice(int id);

}