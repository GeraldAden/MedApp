public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public bool IsMailingAddress { get; set; }
    public int PatientId {get; set;}
    public Patient Patient {get; set;}
}