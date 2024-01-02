// MIT License
// Copyright JTMaher2

using JTMaher.Modules.JTMPasswordsStencilJS.Common.Extensions;
using JTMaher.Modules.JTMPasswordsStencilJS.Data.Entities;
using JTMaher.Modules.JTMPasswordsStencilJS.Data.Repositories;
using JTMaher.Modules.JTMPasswordsStencilJS.DTO;
using JTMaher.Modules.JTMPasswordsStencilJS.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JTMaher.Modules.JTMPasswordsStencilJS.Services
{
    /// <summary>
    /// Provides services to manage items.
    /// </summary>
    public class ItemService : IItemService
    {
        private static readonly int NUMPBKDF2ITERS = 1000;
        private static readonly int SALTLEN = 8;
        private static readonly int KEYLEN = 16;
        private IRepository<Item> itemRepository;
        private LoggingService cLoggingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemService"/> class.
        /// </summary>
        /// <param name="itemRepository">The items repository.</param>
        public ItemService(IRepository<Item> itemRepository)
        {
            this.itemRepository = itemRepository;
            this.cLoggingService = new LoggingService();
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"> is thrown if the item or one of its required properties are missing.</exception>
        public async Task<ItemViewModel> CreateItemAsync(CreateItemDTO item, int userId)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentNullException("The item name is required.", nameof(item.Name));
            }

            byte[][] arrArrBEncrypted = this.Encrypt(item.Description, item.M_StrMasterPassword);

            JObject cObj = new JObject();
            StringBuilder cStringBuilder = new StringBuilder();
            for (int i = 0; i < arrArrBEncrypted[0].Length; i++)
            {
                cStringBuilder.Append($"{arrArrBEncrypted[0][i]}");

                if (i < arrArrBEncrypted[0].Length - 1)
                {
                    cStringBuilder.Append(',');
                }
            }

            cObj["Password"] = cStringBuilder.ToString();
            cStringBuilder.Clear();
            for (int i = 0; i < arrArrBEncrypted[1].Length; i++)
            {
                cStringBuilder.Append($"{arrArrBEncrypted[1][i]}");

                if (i < arrArrBEncrypted[1].Length - 1)
                {
                    cStringBuilder.Append(',');
                }
            }

            cObj["Salt"] = cStringBuilder.ToString();
            cStringBuilder.Clear();
            for (int i = 0; i < arrArrBEncrypted[2].Length; i++)
            {
                cStringBuilder.Append($"{arrArrBEncrypted[2][i]}");

                if (i < arrArrBEncrypted[2].Length - 1)
                {
                    cStringBuilder.Append(',');
                }
            }

            cObj["Initialization Vector"] = cStringBuilder.ToString();

            var newItem = new Item()
            {
                Name = item.Name, Description = JsonConvert.SerializeObject(cObj),
            };

            await this.itemRepository.CreateAsync(newItem, userId);

            return new ItemViewModel(newItem);
        }

        /// <inheritdoc/>
        public async Task<ItemsPageViewModel> GetItemsPageAsync(string strMasterPassword, string query, int page = 1, int pageSize = 10, bool descending = false)
        {
            var items = await this.itemRepository.GetPageAsync(
                page,
                pageSize,
                entities => entities
                    .Where(item => string.IsNullOrEmpty(query) || item.Name.ToUpper().Contains(query.ToUpper()))
                    .Order(item => item.Name, descending));

            var itemsPageViewModel = new ItemsPageViewModel()
            {
                Items = items.Items.Select(item => new ItemViewModel
                {
                    Description = this.Decrypt(strMasterPassword, item.Description),
                    Id = item.Id,
                    Name = item.Name,
                    M_StrMasterPassword = strMasterPassword,
                }).ToList(),
                Page = items.Page,
                ResultCount = items.ResultCount,
                PageCount = items.PageCount,
            };

            return itemsPageViewModel;
        }

        /// <inheritdoc/>
        public async Task DeleteItemAsync(int itemId)
        {
            await this.itemRepository.DeleteAsync(itemId);
        }

        /// <inheritdoc/>
        public async Task UpdateItemAsync(UpdateItemDTO dto, int userId)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentNullException(nameof(dto.Name));
            }

            var item = await this.itemRepository.GetByIdAsync(dto.Id);
            item.Name = dto.Name;
            byte[][] arrArrBEncrypted = this.Encrypt(dto.Description, dto.M_StrMasterPassword);

            JObject cObj = new JObject();
            StringBuilder cStringBuilder = new StringBuilder();
            for (int i = 0; i < arrArrBEncrypted[0].Length; i++)
            {
                cStringBuilder.Append($"{arrArrBEncrypted[0][i]}");

                if (i < arrArrBEncrypted[0].Length - 1)
                {
                    cStringBuilder.Append(',');
                }
            }

            cObj["Password"] = cStringBuilder.ToString();
            cStringBuilder.Clear();
            for (int i = 0; i < arrArrBEncrypted[1].Length; i++)
            {
                cStringBuilder.Append($"{arrArrBEncrypted[1][i]}");

                if (i < arrArrBEncrypted[1].Length - 1)
                {
                    cStringBuilder.Append(',');
                }
            }

            cObj["Salt"] = cStringBuilder.ToString();
            cStringBuilder.Clear();
            for (int i = 0; i < arrArrBEncrypted[2].Length; i++)
            {
                cStringBuilder.Append($"{arrArrBEncrypted[2][i]}");

                if (i < arrArrBEncrypted[2].Length - 1)
                {
                    cStringBuilder.Append(',');
                }
            }

            cObj["Initialization Vector"] = cStringBuilder.ToString();

            item.Description = JsonConvert.SerializeObject(cObj);

            await this.itemRepository.UpdateAsync(item, userId);
        }

        private string Decrypt(string strMasterPassword, string strDescription)
        {
            // Try to decrypt
            string strPassword;
            try
            {
                JObject cEncryptedObj = JObject.Parse(strDescription);
                string strIV = cEncryptedObj["Initialization Vector"].ToString(),
                    strSalt = cEncryptedObj["Salt"].ToString();
                strPassword = cEncryptedObj["Password"].ToString();
                string[] strParts = strSalt.Split(',');
                byte[] arrBSalt = new byte[strParts.Length];
                for (int i = 0; i < strParts.Length; i++)
                {
                    arrBSalt[i] = byte.Parse(strParts[i]);
                }

                Rfc2898DeriveBytes k2 = new Rfc2898DeriveBytes(strMasterPassword, arrBSalt);
                Aes decAlg = Aes.Create();
                decAlg.Key = k2.GetBytes(KEYLEN);

                strParts = strIV.Split(',');
                byte[] arrBIV = new byte[strParts.Length];
                for (int i = 0; i < strParts.Length; i++)
                {
                    arrBIV[i] = byte.Parse(strParts[i]);
                }

                decAlg.IV = arrBIV;
                MemoryStream decryptionStreamBacking = new MemoryStream();
                CryptoStream decrypt = new CryptoStream(decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);

                strParts = strPassword.Split(',');
                byte[] arrBPassword = new byte[strParts.Length];
                for (int i = 0; i < strParts.Length; i++)
                {
                    arrBPassword[i] = byte.Parse(strParts[i]);
                }

                decrypt.Write(arrBPassword, 0, arrBPassword.Length);
                decrypt.Flush();
                decrypt.Close();
                k2.Reset();
                strPassword = new UTF8Encoding(false).GetString(decryptionStreamBacking.ToArray());
            }
            catch (Exception cEx)
            {
                this.cLoggingService.LogError(cEx.Message);
                strPassword = null;
            }

            return strPassword;
        }

        private byte[][] Encrypt(string strItemDescription, string strMasterPassword)
        {
            byte[][] arrBEncrypted;

            try
            {
                byte[] salt1 = new byte[SALTLEN];
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with a random value.
                    rngCsp.GetBytes(salt1);
                }

                arrBEncrypted = new byte[3][];
                Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(strMasterPassword, salt1, NUMPBKDF2ITERS);

                // Encrypt the data.
                Aes encAlg = Aes.Create(); // Declare the array of two elements.

                // Initialize the elements.
                encAlg.Key = k1.GetBytes(KEYLEN);
                MemoryStream encryptionStream = new MemoryStream();
                CryptoStream encrypt = new CryptoStream(encryptionStream, encAlg.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] utfD1 = new UTF8Encoding(false).GetBytes(strItemDescription);
                encrypt.Write(utfD1, 0, utfD1.Length);
                encrypt.FlushFinalBlock();
                encrypt.Close();
                arrBEncrypted[0] = encryptionStream.ToArray();
                arrBEncrypted[1] = salt1;
                arrBEncrypted[2] = encAlg.IV;
                k1.Reset();
            }
            catch (Exception)
            {
                arrBEncrypted = null;
            }

            return arrBEncrypted;
        }
    }
}
