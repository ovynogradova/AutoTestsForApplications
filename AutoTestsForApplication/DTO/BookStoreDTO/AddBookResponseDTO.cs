namespace apitest.DTO.BookStoreDTO;

public class AddBookResponseDTO
{
    public string UserId { get; set; }
    public List<BookDTO> Books { get; set; }
}