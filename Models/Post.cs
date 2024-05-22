namespace SnackisApp.Models
{
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public DateTime Date { get; set; }
    public string UserId { get; set; }
    public int? CategoryId { get; set; } // Nullable int
    public Category Category { get; set; }
    public int? SubCategoryId { get; set; } // Nullable int
    public SubCategory SubCategory { get; set; }
}
}