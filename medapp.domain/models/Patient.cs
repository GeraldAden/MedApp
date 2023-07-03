public class Patient
{
    public Patient()
    {
        Addresses = new List<Address>();
    }

    public string FirstName {get; set;}
    public string LastName {get; set;}
    public DateTime DateOfBirth {get; set;}
    public bool IsSmoker {get; set;}
    public bool HasCancer {get; set;}
    public bool HasDiabetes {get; set;}
    public ICollection<Address> Addresses {get; set;}
}