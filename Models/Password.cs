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
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace JTMaher2.Modules.JTM2_PasswordManager.Models
{
    [TableName("JTM2_PasswordManager_Passwords")]
    //setup the primary key for table
    [PrimaryKey("PasswordId", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("Passwords", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class Password
    {
        ///<summary>
        /// The ID of your object with the name of the PasswordName
        ///</summary>
        public int PasswordId { get; set; } = -1;
        ///<summary>
        /// A string with the name of the website that the password is for
        ///</summary>
        public string PasswordSite { get; set; }

        ///<summary>
        /// A byte array with the encrypted text of the password
        ///</summary>
        public byte[] PasswordText { get; set; }

        ///<summary>
        /// A string with the text of the password
        ///</summary>
        public string PasswordPlainText { get; set; }

        ///<summary>
        /// A byte array with the IV of the password
        ///</summary>
        public byte[] PasswordIV { get; set; }

        ///<summary>
        /// A byte array with the salt of the password
        ///</summary>
        public byte[] PasswordSalt { get; set; }

        ///<summary>
        /// A string with the URL of the website
        /// </summary>
        public string PasswordUrl { get; set; }

        ///<summary>
        /// A string with any notes the user has about the password
        /// </summary>
        public string PasswordNotes { get; set; }

        ///<summary>
        /// An integer with the user id of the assigned user for the object
        ///</summary>
        public int AssignedUserId { get; set; }

        ///<summary>
        /// The ModuleId of where the object was created and gets displayed
        ///</summary>
        public int ModuleId { get; set; }

        ///<summary>
        /// An integer for the user id of the user who created the object
        ///</summary>
        public int CreatedByUserId { get; set; } = -1;

        ///<summary>
        /// An integer for the user id of the user who last updated the object
        ///</summary>
        public int LastModifiedByUserId { get; set; } = -1;

        ///<summary>
        /// The date the object was created
        ///</summary>
        public DateTime CreatedOnDate { get; set; } = DateTime.UtcNow;

        ///<summary>
        /// The date the object was updated
        ///</summary>
        public DateTime LastModifiedOnDate { get; set; } = DateTime.UtcNow;
    }
}
