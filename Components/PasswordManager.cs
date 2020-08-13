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
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using JTMaher2.Modules.JTM2_PasswordManager.Models;

namespace JTMaher2.Modules.JTM2_PasswordManager.Components
{
    interface IPasswordManager
    {
        void CreatePassword(Password t);
        void DeletePassword(int passwordId, int moduleId);
        void DeletePassword(Password t);
        IEnumerable<Password> GetPasswords(int moduleId, string strMasterPassword);
        Password GetPassword(int passwordId, int moduleId);
        void UpdatePassword(Password t);
    }

    class PasswordManager : ServiceLocator<IPasswordManager, PasswordManager>, IPasswordManager
    {
        private static readonly int NUM_PBKDF2_ITERS = 1000;

        private static readonly int SALT_LEN = 8;
        private static readonly int KEY_LEN = 16;

        public void CreatePassword(Password t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Password>();
                rep.Insert(t);
            }
        }

        public void DeletePassword(int passwordId, int moduleId)
        {
            var t = GetPassword(passwordId, moduleId);
            DeletePassword(t);
        }

        public void DeletePassword(Password t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Password>();
                rep.Delete(t);
            }
        }

        public IEnumerable<Password> GetPasswords(int moduleId, string strMasterPassword)
        {
            IEnumerable<Password> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Password>();
                t = rep.Get(moduleId);
            }
            IEnumerator<Password> passEnum = t.GetEnumerator();
            List<Password> retList = new List<Password>();
            while (passEnum.MoveNext())
            {
                // Try to decrypt, thus showing it can be round-tripped.
                byte[] salt1 = new byte[SALT_LEN];
                using (RNGCryptoServiceProvider rngCsp = new
    RNGCryptoServiceProvider())
                {
                    // Fill the array with a random value.
                    rngCsp.GetBytes(salt1);
                }
                Rfc2898DeriveBytes k2 = new Rfc2898DeriveBytes(strMasterPassword, passEnum.Current.PasswordSalt);
                Aes decAlg = Aes.Create();
                decAlg.Key = k2.GetBytes(KEY_LEN);
                decAlg.IV = passEnum.Current.PasswordIV;
                MemoryStream decryptionStreamBacking = new MemoryStream();
                CryptoStream decrypt = new CryptoStream(
decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);
                decrypt.Write(passEnum.Current.PasswordText, 0, passEnum.Current.PasswordText.Length);
                decrypt.Flush();
                decrypt.Close();
                k2.Reset();
                passEnum.Current.PasswordPlainText = new UTF8Encoding(false).GetString(
decryptionStreamBacking.ToArray());
                retList.Add(passEnum.Current);
            }
            return retList;
        }

        public Password GetPassword(int passwordId, int moduleId)
        {
            Password t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Password>();
                t = rep.GetById(passwordId, moduleId);
            }
            return t;
        }

        public void UpdatePassword(Password t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Password>();
                //The default iteration count is 1000 so the two methods use the same iteration count.
                byte[] salt1 = new byte[SALT_LEN];
                using (RNGCryptoServiceProvider rngCsp = new
    RNGCryptoServiceProvider())
                {
                    // Fill the array with a random value.
                    rngCsp.GetBytes(salt1);
                }
                try
                {
                    Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(t.PasswordPlainText, salt1,
    NUM_PBKDF2_ITERS);
                    // Encrypt the data.
                    Aes encAlg = Aes.Create();
                    t.PasswordIV = encAlg.IV;

                    encAlg.Key = k1.GetBytes(KEY_LEN);
                    MemoryStream encryptionStream = new MemoryStream();
                    CryptoStream encrypt = new CryptoStream(encryptionStream,
    encAlg.CreateEncryptor(), CryptoStreamMode.Write);
                    byte[] utfD1 = new UTF8Encoding(false).GetBytes(
    t.PasswordPlainText);

                    encrypt.Write(utfD1, 0, utfD1.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();
                    byte[] edata1 = encryptionStream.ToArray();
                    t.PasswordText = edata1;
                    t.PasswordSalt = salt1;
                    t.PasswordIV = encAlg.IV;
                    k1.Reset();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: ", e);
                }
                rep.Update(t);
            }
        }

        protected override System.Func<IPasswordManager> GetFactory()
        {
            return () => new PasswordManager();
        }
    }
}
