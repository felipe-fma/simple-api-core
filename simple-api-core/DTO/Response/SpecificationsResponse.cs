using simple_api_core.Repositorys.Entity;

namespace simple_api_core.DTO.Response;

public class SpecificationsResponse
{
    public string? originally_published { get; set; }
    public string? author { get; set; }
    public int page_count { get; set; }
    public IEnumerable<string> illustrator { get; set; }
    public IEnumerable<string> genres { get; set; }

    public SpecificationsResponse(Specifications specifications)
    {
        originally_published = specifications.originally_published;
        author = specifications.author;
        page_count = specifications.page_count;
        illustrator = specifications.illustrator;
        genres = specifications.genres;
    }
}
