/*
' Copyright (c) 2020 JTMaher2
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Linq;
using System.Web.Mvc;
using JTMaher2.Modules.JTM2_PasswordManager.Components;
using JTMaher2.Modules.JTM2_PasswordManager.Models;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using System.Security.Cryptography;
using System.IO;

namespace JTMaher2.Modules.JTM2_PasswordManager.Controllers
{
    [DnnHandleError]
    public class PasswordController : DnnController
    {
        private static readonly int NUM_PBKDF2_ITERS = 1000;
        private static readonly int KEY_LEN = 16;
        private static readonly int SALT_LEN = 8;

        public ActionResult Delete(int passwordId)
        {
            PasswordManager.Instance.DeletePassword(passwordId, ModuleContext.ModuleId);
            return RedirectToDefaultRoute();
        }

        public ActionResult Edit(int passwordId = -1)
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            var userlist = UserController.GetUsers(PortalSettings.PortalId);
            var users = from user in userlist.Cast<UserInfo>().ToList()
                        select new SelectListItem { Text = user.DisplayName, Value = user.UserID.ToString() };

            ViewBag.Users = users;

            var password = (passwordId == -1)
                 ? new Password { ModuleId = ModuleContext.ModuleId }
                 : PasswordManager.Instance.GetPassword(passwordId, ModuleContext.ModuleId);

            return View(password);
        }

        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(Password password)
        {
            if (password.PasswordId == -1)
            {
                password.CreatedByUserId = User.UserID;
                password.CreatedOnDate = DateTime.UtcNow;
                password.LastModifiedByUserId = User.UserID;
                password.LastModifiedOnDate = DateTime.UtcNow;
                string pwd1 = ModuleContext.Configuration.ModuleSettings["JTM2_PasswordManager_MasterPassword"].ToString();
                // Create a byte array to hold the random value.
                byte[] salt1 = new byte[SALT_LEN];
                using (RNGCryptoServiceProvider rngCsp = new
    RNGCryptoServiceProvider())
                {
                    // Fill the array with a random value.
                    rngCsp.GetBytes(salt1);
                }

                //data1 can be a string or contents of a file.
                string data1 = password.PasswordPlainText;
                //The default iteration count is 1000 so the two methods use the same iteration count.
                try
                {
                    Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(pwd1, salt1,
    NUM_PBKDF2_ITERS);
                    // Encrypt the data.
                    Aes encAlg = Aes.Create();
                    password.PasswordIV = encAlg.IV;

                    encAlg.Key = k1.GetBytes(KEY_LEN);
                    MemoryStream encryptionStream = new MemoryStream();
                    CryptoStream encrypt = new CryptoStream(encryptionStream,
    encAlg.CreateEncryptor(), CryptoStreamMode.Write);
                    byte[] utfD1 = new System.Text.UTF8Encoding(false).GetBytes(
    data1);

                    encrypt.Write(utfD1, 0, utfD1.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();
                    byte[] edata1 = encryptionStream.ToArray();
                    password.PasswordText = edata1;
                    k1.Reset();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: ", e);
                }

                password.PasswordSalt = salt1;

                password.PasswordPlainText = "";

                PasswordManager.Instance.CreatePassword(password);
            }
            else
            {
                var existingPassword = PasswordManager.Instance.GetPassword(password.PasswordId, password.ModuleId);
                existingPassword.LastModifiedByUserId = User.UserID;
                existingPassword.LastModifiedOnDate = DateTime.UtcNow;
                existingPassword.PasswordSite = password.PasswordSite;
                existingPassword.PasswordText = password.PasswordText;
                existingPassword.PasswordUrl = password.PasswordUrl;
                existingPassword.PasswordNotes = password.PasswordNotes;
                existingPassword.PasswordPlainText = "";
                existingPassword.PasswordIV = password.PasswordIV;
                existingPassword.PasswordSalt = password.PasswordSalt;

                existingPassword.AssignedUserId = password.AssignedUserId;

                PasswordManager.Instance.UpdatePassword(existingPassword);
            }

            return RedirectToDefaultRoute();
        }

        [ModuleAction(ControlKey = "Edit", TitleKey = "AddPassword")]
        public ActionResult Index()
        {
            var passwords = PasswordManager.Instance.GetPasswords(ModuleContext.ModuleId, ModuleContext.Configuration.ModuleSettings["JTM2_PasswordManager_MasterPassword"]?.ToString());
            return View(passwords);
        }
    }
}
