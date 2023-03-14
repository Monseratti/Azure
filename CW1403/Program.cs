using CW1403.Class;
using Microsoft.EntityFrameworkCore;

using (var db = new MyDBContext())
{
    //await db.Database.EnsureDeletedAsync();
    //await db.Database.EnsureCreatedAsync();

    //Author author = new Author() { Id = 1, Name = "Test", Surname = "Tester" };
    //db.Add(author);
    //Book book = new Book() { Id = 2, Title = "SomeBook2", AuthorId = 1 };
    //db.Add(book);
    //await db.SaveChangesAsync();

    //var authors = await db.Authors.ToListAsync();
    //var books = await db.Books.ToListAsync();

    //var data = books.Join(authors, o => o.AuthorId, a => a.Id, (o, a) => new {id=o.Id, title=o.Title, author=$"{a.Name} {a.Surname}"}).ToList();

    //foreach (var item in data)
    //{
    //    Console.WriteLine($"ID: {item.id}, Title: {item.title}, Author: {item.author}");
    //}

    //var book = await db.Books.Where(o=>o.Id==2).FirstAsync();
    //Console.WriteLine(book.ToString());
    //book.Title = "ChangeTitle";
    //db.Update(book);
    //await db.SaveChangesAsync();

    db.Books.Remove(db.Books.Where(o=>o.Id==2).First());
    await db.SaveChangesAsync();

    //Console.WriteLine( (await db.Books.Where(o => o.Id == 2).FirstAsync()).ToString()); 
    Console.WriteLine("It`s ok");
}