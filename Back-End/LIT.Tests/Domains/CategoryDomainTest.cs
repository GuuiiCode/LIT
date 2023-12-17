using LIT.Domain.Entities;
using Xunit;

namespace LIT.Tests.Domains
{
    public class CategoryDomainTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NameIsNullOrEmpty_ReturnArgumentNullException(string? name)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => CreateCategoryObject(name, "description"));
            Assert.Equal("Value cannot be null. (Parameter 'Name')", exception.Message);
        }

        [Theory]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam lorem est, pretium nec sapien ac, volutpat")]
        public void IfNameHasMoreThanOneHundredCharacter_ReturnArgumentOutOfRangeException(string? name)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => CreateCategoryObject(name, "description"));
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'Name Maximum 100 characters')", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DescriptionIsNull_ReturnArgumentNullException(string? description)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => CreateCategoryObject("nam", description));
            Assert.Equal("Value cannot be null. (Parameter 'Description')", exception.Message);
        }

        [Theory]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam lorem est, pretium nec sapien ac, volutpat Aliquam lorem est, pretium nec sapien ac,  ")]
        public void IfDescribeHasMoreThanOneHundredFiftyCharacter_ReturnArgumentOutOfRangeException(string? description)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => CreateCategoryObject("name", description));
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'Description Maximum 150 characters')", exception.Message);
        }

        public Category CreateCategoryObject(string? name, string? description)
        {
            return new Category(name, description);
        }

    }
}
