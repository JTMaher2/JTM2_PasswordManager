﻿using JTMaher.Modules.JTMPasswordsStencilJS.Data.Entities;
using JTMaher.Modules.JTMPasswordsStencilJS.Data.Repositories;
using JTMaher.Modules.JTMPasswordsStencilJS.DTO;
using JTMaher.Modules.JTMPasswordsStencilJS.Services;
using JTMaher.Modules.JTMPasswordsStencilJS.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services
{
    public class ItemServiceTests
    {
        private Mock<IRepository<Item>> itemRepository;
        private IItemService itemService;

        public ItemServiceTests()
        {
            this.itemRepository = new Mock<IRepository<Item>>();
            this.itemService = new ItemService(this.itemRepository.Object);
        }

        [Fact]
        public async Task CreateItem_NoItemThrows()
        {
            Task createItem() => this.itemService.CreateItemAsync(null, 123);

            await Assert.ThrowsAsync<ArgumentNullException>(createItem);
        }

        [Fact]
        public async Task CreateItem_NoNameThrows()
        {
            Task createItem() => this.itemService.CreateItemAsync(new CreateItemDTO(), 123);

            await Assert.ThrowsAsync<ArgumentNullException>(createItem);
        }

        [Fact]
        public async Task CreateItem_Creates()
        {
            var item = new CreateItemDTO() { Name = "Name", Description = "Description", M_StrMasterPassword = "Master Password" };
            itemRepository.Setup(r => r.CreateAsync(It.IsAny<Item>(), It.IsAny<int>()))
                .Callback<Item, int>((i, u) =>
                {
                    i.Id = 1;
                    i.Name = item.Name;
                    i.Description = item.Description;
                });

            var createdItem = await this.itemService.CreateItemAsync(item, 123);

            this.itemRepository.Verify(r =>
                r.CreateAsync(It.Is<Item>(i =>
                i.Name == item.Name &&
                i.Description == item.Description), 123));
            Assert.Equal(1, createdItem.Id);
            Assert.Equal(item.Name, createdItem.Name);
            Assert.NotNull(createdItem.Description);
            Assert.Null(createdItem.M_StrMasterPassword);
        }

        [Theory]
        [InlineData(false)]
        public async Task GetItemsPage_GetsPages(bool descending)
        {
            var returnedItems = new List<Item>();
            for (int i = 0; i < 30; i++)
            {
                returnedItems.Add(new Item
                {
                    Id = i,
                    Name = $"Name {i}",
                    Description = "{\"Password\":\"50,133,111,126,174,71,79,249,159,144,157,187,37,197,147,32\",\"Salt\":\"172,57,212,115,94,44,246,25\",\"Initialization Vector\":\"150,34,187,154,147,165,80,192,5,151,172,228,205,24,23,185\"}",
                });
            }
            var returnedPage = new PagedList<Item>(
                returnedItems, 1, 10, 30, 3);
            this.itemRepository.Setup(i => i.GetPageAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<Func<IQueryable<Item>, IOrderedQueryable<Item>>>()))
                .Returns(Task.FromResult(returnedPage));

            var finalReturn = await this.itemService.GetItemsPageAsync("test", "test", 1, 10, descending);

            Assert.IsType<ItemsPageViewModel>(finalReturn);
            Assert.Equal(30, finalReturn.ResultCount);
            Assert.Equal(3, finalReturn.PageCount);
            Assert.Equal(1, finalReturn.Page);
        }

        [Fact]
        public async Task DeleteItem_Deletes()
        {
            var itemId = 123;

            await this.itemService.DeleteItemAsync(itemId);

            this.itemRepository.Verify(i => i.DeleteAsync(123), Times.Once);
        }

        [Fact]
        public async Task UpdateItem_Updates()
        {
            var originalItem = new Item
            {
                Id = 1,
                Name = "Original Name",
                Description = "test",
            };

            var item = new UpdateItemDTO
            {
                Id = 1,
                Name = "New Item Name",
                Description = "test",
                M_StrMasterPassword = "test",
            };
            this.itemRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(originalItem));

            await this.itemService.UpdateItemAsync(item, 2);

            itemRepository.Verify(r => r.UpdateAsync(It.Is<Item>(i =>
                i.Id == item.Id &&
                i.Name == item.Name //&&
                /*i.Description == item.Description*/), 2), Times.Once);
        }

        [Fact]
        public async Task UpdateItem_NullDto_Throws()
        {
            Task nullDto() => this.itemService.UpdateItemAsync(null, 2);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(nullDto);
            Assert.Equal("dto", ex.ParamName);
            itemRepository.Verify(r =>
                r.UpdateAsync(
                    It.IsAny<Item>(),
                    It.IsAny<int>()
                ), Times.Never);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public async Task UpdateItem_NoName_Throws(string name)
        {
            var item = new UpdateItemDTO
            {
                Id = 123,
                Name = name,
                Description = "",
                M_StrMasterPassword = "",
            };

            Task noName() => this.itemService.UpdateItemAsync(item, 2);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(noName);
            Assert.Equal("Name", ex.ParamName);
        }
    }
}
