public record Patient(string FirstName, string LastName, int Age, bool IsSmoker, bool HasCancer, bool HasDiabetes, Address Address)
{
    public class Builder
    {
        public Builder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public Builder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public Builder WithAge(int age)
        {
            _age = age;
            return this;
        }

        public Builder IsSmoker(bool isSmoker)
        {
            _isSmoker = isSmoker;
            return this;
        }

        public Builder HasCancer(bool hasCancer)
        {
            _hasCancer = hasCancer;
            return this;
        }

        public Builder HasDiabetes(bool hasDiabetes)
        {
            _hasDiabetes = hasDiabetes;
            return this;
        }

        public Builder WithAddress(Address address)
        {
            _address = address;
            return this;
        }

        public Patient Build()
        {
            return new Patient(_firstName, _lastName, _age, _isSmoker, _hasCancer, _hasDiabetes, _address);
        }

        private string _firstName;
        private string _lastName;
        private int _age;
        private bool _isSmoker;
        private bool _hasCancer;
        private bool _hasDiabetes;
        private Address _address;
    }
}