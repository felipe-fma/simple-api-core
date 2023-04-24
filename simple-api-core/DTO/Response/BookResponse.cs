using simple_api_core.Repositorys.Entity;

namespace simple_api_core.DTO.Response;

public class BookResponse
{
    public int id { get; set; }
    public string? name { get; set; }
    public double price { get; set; }
    public SpecificationsResponse specifications { get; set; }

    public BookResponse(Book book)
    {
        id = book.Id;
        name = book.name;
        price = book.price;

        specifications = new SpecificationsResponse(book.specifications);
    }
}