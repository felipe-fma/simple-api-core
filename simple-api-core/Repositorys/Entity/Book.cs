namespace simple_api_core.Repositorys.Entity;

public class Book : TEntity
{
    public string? name { get; set; }
    public double price { get; set; }
    public Specifications specifications { get; set; }
}