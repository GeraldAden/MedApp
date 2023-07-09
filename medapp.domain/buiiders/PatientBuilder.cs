namespace MedApp.Domain.Buiiders;

using MedApp.Infrastructure.Database.Entities;

public class PatientBuilder
{
    private string _firstName;
    private string _lastName;
    private DateTime _dateOfBirth;
    private string _email;
    private bool _isSmoker;
    private bool _hasCancer;
    private bool _hasDiabetes;
    private ICollection<Address> _addresses;

    public PatientBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public PatientBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public PatientBuilder WithDateOfBirth(DateTime dateOfBirth)
    {
        _dateOfBirth = dateOfBirth;
        return this;
    }

    public PatientBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public PatientBuilder IsSmoker(bool isSmoker)
    {
        _isSmoker = isSmoker;
        return this;
    }

    public PatientBuilder HasCancer(bool hasCancer)
    {
        _hasCancer = hasCancer;
        return this;
    }

    public PatientBuilder HasDiabetes(bool hasDiabetes)
    {
        _hasDiabetes = hasDiabetes;
        return this;
    }

    public PatientBuilder WithAddresses(ICollection<Address> addresses)
    {
        _addresses = addresses;
        return this;
    }

    public Patient Build()
    {
        return new Patient
        {
            FirstName = _firstName,
            LastName = _lastName,
            DateOfBirth = _dateOfBirth,
            Email = _email,
            IsSmoker = _isSmoker,
            HasCancer = _hasCancer,
            HasDiabetes = _hasDiabetes,
            Addresses = _addresses
        };
    }
}