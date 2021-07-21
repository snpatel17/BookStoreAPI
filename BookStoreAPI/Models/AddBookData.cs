using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStoreAPI.Data
{
    public class AddBookData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreContext(
                serviceProvider.GetRequiredService<DbContextOptions<BookStoreContext>>()))
            {
                // Look for any Books.
                if (context.Books.Any())
                {
                    return;   // Data was already seeded
                }
                context.Books.AddRange(
                new BooksModel
                {
                    Id = "BeaRe123",
                    Title = "Beach Read",
                    Description = "Original, sparkling bright, and layered with feeling.",
                    Author = "Henry, Emily",
                    CoverImage = "emilyhenryBR.jpeg",
                    Price = "$11.99"
                },
                new BooksModel
                {
                    Id = "BomMa456",
                    Title = "Bomber Mafia",
                    Description = "The stories of a Dutch genius and his homemade computer, a band of brothers in central Alabama,.",
                    Author = "Gladwell, Malcolm",
                    CoverImage = "malcomgladwellBM.jpeg",
                    Price = "$34.00"
                });

                context.SaveChanges();
            }
        }
    }
}
