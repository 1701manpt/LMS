using LMS.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Item> Items { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Dvd> Dvds { get; set; }
    public DbSet<Magazine> Magazines { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<BorrowedHistory> BorrowedHistories { get; set; }
    public DbSet<BorrowedItem> BorrowedItems { get; set; }
    public DbSet<BorrowedItemTemp> BorrowedItemTemps { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply Discriminator for Item hierarchy
        modelBuilder.Entity<Item>()
            .HasDiscriminator(i => i.Type)
            .HasValue<Book>(ItemType.Book)
            .HasValue<Dvd>(ItemType.Dvd)
            .HasValue<Magazine>(ItemType.Magazine);

        modelBuilder.Entity<BorrowedHistory>()
            .HasOne(bh => bh.Borrower) // Một BorrowedHistory có một Borrower
            .WithMany(b => b.BorrowedHistories) // Một Borrower có nhiều BorrowedHistory
            .HasForeignKey(bh => bh.BorrowerId); // Khóa ngoại BorrowerId

        modelBuilder.Entity<BorrowedItem>()
            .HasOne(bi => bi.Item) // Một BorrowedItem có một Item
            .WithMany(i => i.BorrowedItems) // Một Item có nhiều BorrowedItem
            .HasForeignKey(bi => bi.ItemId); // Khóa ngoại ItemId

        modelBuilder.Entity<BorrowedItem>()
            .HasOne(bi => bi.BorrowedHistory) // Một BorrowedItem có một BorrowedHistory
            .WithMany(bh => bh.BorrowedItems) // Một BorrowedHistory có nhiều BorrowedItem
            .HasForeignKey(bi => bi.BorrowedHistoryId); // Khóa ngoại BorrowedHistoryId

        modelBuilder.Entity<BorrowedItemTemp>()
            .HasOne(bit => bit.Item) // Một BorrowedItemTemp có một Item
            .WithMany(i => i.BorrowedItemTemps) // Một Item có nhiều BorrowedItemTemp
            .HasForeignKey(bi => bi.ItemId); // Khóa ngoại ItemId
    }
}
