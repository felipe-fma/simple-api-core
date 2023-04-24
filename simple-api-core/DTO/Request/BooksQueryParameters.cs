using simple_api_core.DTO.Response;

namespace simple_api_core.DTO.Request
{
    public class BooksQueryParameters
    {
        public string name { get; set; } = "";
        public double price { get; set; } = 0d;
        public string originally_published { get; set; } = "";
        public string author { get; set; } = "";
        public int page_count { get; set; } = 0;
        public string illustrator { get; set; } = "";
        public string genres { get; set; } = "";
        public bool orderAsc { get; set; } = true;
    }

    public static class BooksQueryParametersStatic
    {
        public static IEnumerable<BookResponse> Filtered(this BooksQueryParameters query, IEnumerable<BookResponse> books)
        {
            var newBooks = new List<BookResponse>();

            Func<BookResponse, bool> predicate = (x => true);

            if(!String.IsNullOrEmpty(query.name))
                predicate += (x=>x.name.Contains(query.name));

            if(query.price > 0)
                predicate += (x=>x.price == query.price);
                
            if(!String.IsNullOrEmpty(query.originally_published))
                predicate += (x=>x.specifications.originally_published.Contains(query.originally_published));

            if(!String.IsNullOrEmpty(query.author))
                predicate += (x=>x.specifications.author.Contains(query.author));

            if(query.page_count > 0)
                predicate += (x=>x.specifications.page_count == query.page_count);

            if(!String.IsNullOrEmpty(query.illustrator))
                predicate += (x=>x.specifications.illustrator.Contains(query.illustrator));

            if(!String.IsNullOrEmpty(query.genres))
                predicate += (x=>x.specifications.genres.Contains(query.genres));

            newBooks = books.Where(predicate).ToList();

            if(query.orderAsc)
                newBooks = newBooks.OrderBy(x=>x.price).ToList();
            else 
                newBooks = newBooks.OrderByDescending(x=>x.price).ToList();

           

            return newBooks;
        }
    }
}