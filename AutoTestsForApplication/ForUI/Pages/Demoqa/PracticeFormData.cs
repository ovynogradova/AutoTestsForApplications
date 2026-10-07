namespace apitest.ForUI.Pages.Demoqa;

public enum Gender
{
    Male,
    Female,
    Other
}

public class PracticeFormData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public string Mobile { get; set; }
}