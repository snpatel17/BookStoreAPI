using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Data
{
    public class BooksModel
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Please Add title property")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string CoverImage { get; set; }

        public string Price { get; set; }

        
    }
}
