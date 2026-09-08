using System.Net;
using apitest.DTO.BookStoreDTO;
using apitest.Interfaces.IBookStore;
using AutoTestsForApplication.Helpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace apitest;

public class BookStoreTests
{
    private IBookStore API;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services
            .AddRefitClient<IBookStore>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://demoqa.com");
            });

        var provider = services.BuildServiceProvider();
        API = provider.GetRequiredService<IBookStore>();
    }

    [Test]
    public async Task CreateUserAsync()
    {
        var user = new UserDTO{UserName = "Inga5", Password = "StrongPass123!"};
        var response = await API.CreateUserAsync(user);
    }
    
    [Test]
    public async Task GetToken()
    {
        try
        {
            await API.CreateUserAsync(new UserDTO
            {
                UserName = "Inga5", Password = "StrongPass321!"
            });
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
        {
            // Пользователь уже создан при прошлом запуске теста - это ожидаемо, продолжаем.
        }

        var tokenResponse = await API.GenerateTokenAsync(new LoginRequestDTO
        {
            UserName = "Inga5",
            Password = "StrongPass321!"
        });

        tokenResponse.Token.Should().NotBeNullOrEmpty();
        tokenResponse.Status.Should().Be("Success");
        tokenResponse.Result.Should().Contain("authorized");
    }
    
    [Test]
    public async Task LoginUserAsync()
    {
        var user = new UserDTO { UserName = "Inga5", Password = "StrongPass321!" };
        var response = await API.LoginUserAsync(user);

        response.UserId.Should().NotBeNullOrEmpty();
        response.Username.Should().Contain(user.UserName);
    }

    [Test]
    public async Task AddBookAsync()
    {
        var token = await GetTokenAsync();

        var userId = await GetUserIdAsync();
        // isbn 9781449325862
        var newBook = new AddBookRequestDTO
        {
            UserId = userId, CollectionOfIsbns = new List<BookDTO> { new BookDTO { Isbn = "9781449325862" } }
        };

        var addResponse = await API.AddBookAsync(newBook, token);
        addResponse.Books.Should().HaveCount(1);
    }

    [Test]
    public async Task GetAllBooksAsync()
    {
        var response = await API.GetAllBookAsync();

        response.Should().NotBeNull();
        response.Books.Should().HaveCount(8);
    }

    [Test]
    public async Task GetBookByIsbnAsync()
    {
        var getAllBooks = await API.GetAllBookAsync();
        var rndBook = RandomHelper.GetRandomItem(getAllBooks.Books);
        var bookByIsbn = rndBook.Isbn;
        var book = await API.GetBookByIsbnAsync(bookByIsbn);

        book.SubTitle.Should().Be(rndBook.SubTitle);
    }

    [Test]
    public async Task AddMultipleBookAsync()
    {
        var token = await GetTokenAsync();

        var userId = await GetUserIdAsync();
        // isbn 9781449325862
        // isbn 9781449331818
        var newBook = new AddBookRequestDTO
        {
            UserId = userId,
            CollectionOfIsbns = new List<BookDTO>
                { new BookDTO { Isbn = "9781449325862" }, new BookDTO { Isbn = "9781449331818" } }
        };

        var addResponse = await API.AddBookAsync(newBook, token);

        addResponse.Books.Should().HaveCount(2);
    }

    [Test]
    public async Task AddBookWoTokenAsync() //негативный проверка запроса без токена, проверка на 401
    {
        var userId = await GetUserIdAsync();
        var newBook = new AddBookRequestDTO
        {
            UserId = userId,
            CollectionOfIsbns = new List<BookDTO>
                { new BookDTO { Isbn = "9781449325862" }, new BookDTO { Isbn = "9781449331818" } }
        };

        // var addResponse = await api.AddBookAsync(newBook, token: null);
        //
        // addResponse.Books.Should().HaveCount(2);

        Func<Task> action = async () =>
            await API.AddBookAsync(newBook, token: null); //делегат
        await action.Should().ThrowAsync<ApiException>()
            .Where(e => e.StatusCode == System.Net.HttpStatusCode.Unauthorized);
    }

    // [Test]
    // public async Task AddMultipleBookWithWrongIsbnAsync() //негативный с неверным isbn и статус 400
    // {
    //     var token = await GetTokenAsync();
    //
    //     var userId = await GetUserIdAsync();
    //     // isbn 9781449325862
    //     // isbn 9781449331818
    //     var newBook = new AddBookRequestDTO
    //     {
    //         UserId = userId,
    //         CollectionOfIsbns = new List<BookDTO>
    //             { new BookDTO { Isbn = "" }, new BookDTO { Isbn = "" } }
    //     };
    //
    //     //var addResponse = await api.AddBookAsync(newBook, token);
    //
    //     //addResponse.Books.Should().HaveCount(2);
    //
    //
    //     // Тест красный так как сервер возвращает 200 ок и эксепшен не ловится
    //     Func<Task> action = async () =>
    //         await api.AddBookAsync(newBook, token); //делегат
    //
    //     await action.Should().ThrowAsync<ApiException>()
    //         .Where(e => e.StatusCode == System.Net.HttpStatusCode.BadRequest);
    // }

    [Test]
    public async Task DeleteBookByIsbnAndByUserIdAsync()
    {
        var token = await GetTokenAsync();
        var userId = await GetUserIdAsync();
        var userInfo = await API.GetUserAsync(userId, token);

        foreach (var book in userInfo.Books)
        {
            var b = new DeleteBookRequestDTO { Isbn = book.Isbn, UserId = userId };
            await API.DeleteBookByIsbnAndByUserIdAsync(b, token);
        }

        var userInfoAfterDeleteBooks = await API.GetUserAsync(userId, token);
        userInfoAfterDeleteBooks.Books.Should().HaveCount(0);
    }


    private async Task<string> GetTokenAsync()
    {
        var user = new LoginRequestDTO() { UserName = "Inga5", Password = "StrongPass321!" };
        var getToken = await API.GenerateTokenAsync(user);
        var token = $"Bearer {getToken.Token}";
        return token;
    }

    private async Task<string> GetUserIdAsync()
    {
        var user = new UserDTO { UserName = "Inga5", Password = "StrongPass321!" };
        var response = await API.LoginUserAsync(user);
        var userId = response.UserId;
        return userId;
    }
}