using JTMaher.Modules.JTMPasswordsStencilJS.Common;
using System;
using Xunit;

namespace UnitTests.Common
{
    public class GlobalsTests
    {
        [Fact]
        public void ModulePrefixIsValid()
        {
            var modulePrefix = Globals.ModulePrefix;
            Assert.Equal(expected: "JTM_JTM2_PasswordsStencilJS_", modulePrefix);
        }
    }
}
