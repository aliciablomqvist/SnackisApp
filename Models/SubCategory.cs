namespace SnackisApp.Models
{
    
public class SubCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

      public List<Post> Post { get; set; }
}
}


