using Newtonsoft.Json;

namespace simple_api_core.Repositorys.Entity;

[JsonObject]
public class Specifications
{
    public string? originally_published { get; set; }
    public string? author { get; set; }
    public int page_count { get; set; }
    public IEnumerable<string> illustrator { get; set; }
    public IEnumerable<string> genres { get; set; }
}

