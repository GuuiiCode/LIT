using LIT.Domain.Entities;
using Xunit;

namespace LIT.Tests.Domains
{
    public class ProductDomainTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NameIsNullOrEmpty_ReturnArgumentNullException(string name)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => CreateProductObject(name, "description", 10, "black", Guid.NewGuid()));
            Assert.Equal("Value cannot be null. (Parameter 'Name')", exception.Message);
        }

        [Theory]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam lorem est, pretium nec sapien ac, volutpat")]
        public void IfNameHasMoreThanOneHundredCharacter_ReturnArgumentOutOfRangeException(string name)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => CreateProductObject(name, "description", 10, "black", Guid.NewGuid()));
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'Name Maximum 100 characters')", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DescriptionIsNull_ReturnArgumentNullException(string description)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => CreateProductObject("nam", description, 10, "black", Guid.NewGuid()));
            Assert.Equal("Value cannot be null. (Parameter 'Description')", exception.Message);
        }

        [Theory]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam lorem est, pretium nec sapien ac, volutpat Aliquam lorem est, pretium nec sapien ac,  ")]
        public void IfDescribeHasMoreThanOneHundredFiftyCharacter_ReturnArgumentOutOfRangeException(string description)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => CreateProductObject("name", description, 10, "black", Guid.NewGuid()));
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'Description Maximum 150 characters')", exception.Message);
        }
        
        [Theory]
        [InlineData(null)]
        public void CategoryIsNull_ReturnArgumentNullException(Guid categoryId)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => CreateProductObject("name", "description", 10, "black", categoryId));
            Assert.Equal("Value cannot be null. (Parameter 'CategoryId')", exception.Message);
        }

        public Product CreateProductObject(string name, string description, decimal price, string? color, Guid categoryId) 
            => new(name, description, price, color, categoryId);
    }
}
