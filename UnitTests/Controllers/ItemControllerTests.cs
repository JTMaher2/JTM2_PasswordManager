﻿using DotNetNuke.Entities.Users;
using JTMaher.Modules.JTMPasswordsStencilJS.Controllers;
using JTMaher.Modules.JTMPasswordsStencilJS.DTO;
using JTMaher.Modules.JTMPasswordsStencilJS.Services;
using JTMaher.Modules.JTMPasswordsStencilJS.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace UnitTests.Controllers
{
    public class ItemControllerTests
    {
        private readonly Mock<IItemService> itemService;
        private readonly ItemController itemController;


        public ItemControllerTests()
        {
            this.itemService = new Mock<IItemService>();
            this.itemController = new FakeItemController(this.itemService.Object);
        }

        [Fact]
        public async Task CreateItem_Creates()
        {
            var name = "Name";
            var description = "Description";
            var strMasterPassword = "Master Password";
            var dto = new CreateItemDTO()
            {
                Name = name,
                Description = description,
                M_StrMasterPassword = strMasterPassword,
            };
            var viewModel = new ItemViewModel() { Id = 1, Name = name, Description = description, M_StrMasterPassword = strMasterPassword };
            this.itemService.Setup(i => i.CreateItemAsync(It.IsAny<CreateItemDTO>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new ItemViewModel() { Id = 1, Name = name, Description = description, M_StrMasterPassword = strMasterPassword }));

            var result = await this.itemController.CreateItem(dto);

            var response = Assert.IsType<OkNegotiatedContentResult<ItemViewModel>>(result);
            Assert.Equal(1, response.Content.Id);
            Assert.Equal(name, response.Content.Name);
            Assert.NotNull(response.Content.Description);
            Assert.NotNull(response.Content.M_StrMasterPassword);
        }

        [Fact]
        public async Task GetItemsPage_GetsProperPages()
        {
            var items = new List<ItemViewModel>();
            for (int i = 0; i < 100; i++)
            {
                var item = new ItemViewModel() { Id = i, Name = $"Name {i}", Description = "{\"Password\":\"50,133,111,126,174,71,79,249,159,144,157,187,37,197,147,32\",\"Salt\":\"172,57,212,115,94,44,246,25\",\"Initialization Vector\":\"150,34,187,154,147,165,80,192,5,151,172,228,205,24,23,185\"}", M_StrMasterPassword = "" };
                items.Add(item);
            }
            var itemsPageViewModel = new ItemsPageViewModel() { Items = items, Page = 1, PageCount = 10, ResultCount = 100 };
            this.itemService.Setup(i => i.GetItemsPageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(itemsPageViewModel));
            var dto = new GetItemsPageDTO
            {
                M_StrMasterPassword = "test",
                Query = "Name",
                Page = 1,
                PageSize = 10,
                Descending = true,
            };

            var result = await this.itemController.GetItemsPage(dto);

            var response = Assert.IsType<OkNegotiatedContentResult<ItemsPageViewModel>>(result);
            Assert.Equal(100, response.Content.Items.Count);
            Assert.Equal(1, response.Content.Page);
            Assert.Equal(10, response.Content.PageCount);
            Assert.Equal(100, response.Content.ResultCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public async Task DeleteItem_Deletes(int itemId)
        {
            var result = await this.itemController.DeleteItem(itemId);

            Assert.IsType<OkResult>(result);
            this.itemService.Verify(i => i.DeleteItemAsync(itemId), Times.Once);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UserCanEdit_ReturnsBool(bool canEdit)
        {
            this.itemController.CanEdit = canEdit;

            var result = this.itemController.UserCanEdit();

            var response = Assert.IsType<OkNegotiatedContentResult<bool>>(result);
            Assert.Equal(canEdit, response.Content);
        }

        [Fact]
        public async Task UpdateItem_Updates()
        {
            var item = new UpdateItemDTO
            {
                Id = 123,
                Name = "Edited Item",
                Description = "This item was edited",
                M_StrMasterPassword = "testpassword"
            };

            await this.itemController.UpdateItem(item);

            this.itemService.Verify(s => s.UpdateItemAsync(item, It.IsAny<int>()), Times.Once);
        }

        public class FakeItemController : ItemController
        {
            private bool canEdit = false;

            public readonly IItemService itemService;
            public FakeItemController(IItemService itemService)
                : base(itemService)
            {
                this.itemService = itemService;
            }
            public override UserInfo UserInfo => new UserInfo() { UserID = 123 };

            public override bool CanEdit
            {
                get { return this.canEdit; }
                set { this.canEdit = value; }
            }
        }

    }
}
