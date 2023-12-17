using JTMaher.Modules.JTMPasswordsStencilJS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Data.Entities
{
    public class ItemTests
    {
        [Fact]
        public void ItemConstructsProperly()
        {
            var item = new Item();
            Assert.NotNull(item);
            Assert.Equal(0, item.Id);
            Assert.True(string.IsNullOrWhiteSpace(item.Name));
            Assert.True(string.IsNullOrWhiteSpace(item.Description));
        }

        [Theory]
        [InlineData(1, "Name1", "{\"Password\":\"50,133,111,126,174,71,79,249,159,144,157,187,37,197,147,32\",\"Salt\":\"172,57,212,115,94,44,246,25\",\"Initialization Vector\":\"150,34,187,154,147,165,80,192,5,151,172,228,205,24,23,185\"}")]
        [InlineData(2, "Name2", "{\"Password\":\"50,133,111,126,174,71,79,249,159,144,157,187,37,197,147,32\",\"Salt\":\"172,57,212,115,94,44,246,25\",\"Initialization Vector\":\"150,34,187,154,147,165,80,192,5,151,172,228,205,24,23,185\"}")]
        public void ItemPropertiesPersist(int id, string name, string description)
        {
            var item = new Item()
            {
                Id = id,
                Name = name,
                Description = description,
            };
            Assert.Equal(id, item.Id);
            Assert.Equal(name, item.Name);
            Assert.Equal(description, item.Description);
        }
    }
}
