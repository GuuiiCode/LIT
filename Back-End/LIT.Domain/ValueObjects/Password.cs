namespace LIT.Domain.ValueObjects
{
    public class Password
    {
        public string? Hash { get; private  set; }

        public Password(string? plainText)
        {
            if(string.IsNullOrWhiteSpace(plainText)) 
                throw new ArgumentException("Password nao pode ser nula ou vazia.");

            if (plainText.Length < 6 || plainText.Length > 100)
                throw new ArgumentException("Password deve ter entre 6 a 100 caracteres.");

            Hash = BCrypt.Net.BCrypt.HashPassword(plainText);
        }

        public Password(string? hash, bool isHashed)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentNullException("Hash nao pode ser nulo ou vazio");

            Hash = hash;
        }

        public bool Verify(string? plainText)
        {
            if(string.IsNullOrWhiteSpace(plainText) || string.IsNullOrWhiteSpace(Hash))
                return false;

            return BCrypt.Net.BCrypt.Verify(plainText, Hash);
        }
    }
}
