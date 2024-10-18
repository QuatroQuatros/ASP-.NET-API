namespace GestaoDeResiduos.ViewModels.Responses;

public class PaginatedResponse<T>
{
    public List<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}
    
public class Pagination
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;

    public Pagination()
    {
        
    }
    public bool isValid()
    {
        return (Page > 0 && Size >= 5);

    }
    
    public bool isInvalid()
    {
        return !isValid();

    }
    
}
