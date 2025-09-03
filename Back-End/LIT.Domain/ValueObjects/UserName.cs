namespace LIT.Domain.ValueObjects
{
    public class UserName
    {
        public string Value { get; private set; }

        public UserName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("UserName nao pode ser nula ou vazia.");

            if (value.Length < 3 || value.Length > 50)
                throw new ArgumentException("UserName deve ter entre 3 a 50 caracteres.");

            Value  = value;
        }
    }
}
