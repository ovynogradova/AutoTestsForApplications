namespace apitest.ForUI.Pages.Demoqa;

public class PracticeForm
{
    private readonly PracticeFormData data = new();

    public PracticeForm WithFirstName(string firstName)
    {
        data.FirstName = firstName;
        return this;
    }

    public PracticeForm WithLastName(string lastName)
    {
        data.LastName = lastName;
        return this;
    }

    public PracticeForm WithGender(Gender gender)
    {
        data.Gender = gender;
        return this;
    }

    public PracticeForm WithMobile(string mobile)
    {
        data.Mobile = mobile;
        return this;
    }

    public PracticeFormData Build()
    {
        return data;
    }
}