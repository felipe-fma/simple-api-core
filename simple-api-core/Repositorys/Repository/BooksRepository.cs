using Newtonsoft.Json.Linq;
using simple_api_core.Repositorys.Entity;
using simple_api_core.Repositorys.Interface;

namespace simple_api_core.Repositorys.Repository;

//Foi utilizado o Newtonsoft.Json apenas para ajudar a criar o metodo de deserialização manual
public class BooksRepository : RepositoryBase<Book>, IBooksRepository
{
    /* 
    *  Nesse metodo poderia utilzar a função Deserialized, mas o campos Illustrator e Genres
    *  Nesse JSON tem 2 tipos de valores que pode ter sendo eles string e string[].
    *  Com isso montei uma função para deseralizar manualmente os campos e passar para a minha classe de entidade.
    */
    public override List<Book> GetAll()
    {
        var listBooks = new List<Book>();

        IList<JToken> list = JArray.Parse(json);
        for (int i = 0; i < list.Count; i++)
        {
            JObject jsonBook = (JObject)list[i];
            var book = new Book();

            book.Id = (int)jsonBook["id"];
            book.name = (string)jsonBook["name"];
            book.price = (double)jsonBook["price"];
            
            book.specifications = new Specifications();

            JObject? jsonSpecifications = (JObject?)jsonBook["specifications"];
            book.specifications.originally_published = (string)jsonSpecifications["Originally published"];
            book.specifications.author = (string)jsonSpecifications["Author"];
            book.specifications.page_count = (int)jsonSpecifications["Page count"];
            
            book.specifications.illustrator = JsonArrayToList(jsonSpecifications, "Illustrator");
            book.specifications.genres = JsonArrayToList(jsonSpecifications, "Genres");

            listBooks.Add(book);
        }
        
        return listBooks;
    }

    // Essa ea função para convertar o string para string[] que e aceito pela classe de entidade
    private List<string> JsonArrayToList(JObject jsonSpecifications, string Key )
    {
        var listString = new List<string>();

        var arrayString = jsonSpecifications[Key];
        
        if(arrayString.Count() > 1)
        {
            var lis = (IList<JToken>)arrayString;
            foreach (var x in lis)
                listString.Add(x.ToString());    
        }
        else 
            listString.Add(arrayString.ToString());

        return listString;
    }
}