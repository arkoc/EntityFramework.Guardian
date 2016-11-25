using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EntityFramework.Guardian.UnitTests
{
    public class GuardianKernelTests
    {
        [Fact]
        public void TryValidate_WithNullQueryGuard_ShouldTrowExceptions()
        {
            var kernel = new GuardianKernel();
            kernel.QueryGuard = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                kernel.TryValidate();
            });
        }

        [Fact]
        public void TryValidate_WithNullSubmitGuard_ShouldTrowExceptions()
        {
            var kernel = new GuardianKernel();
            kernel.SubmitGuard = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                kernel.TryValidate();
            });
        }

        [Fact]
        public void TryValidate_ShouldSuccess()
        {
            var kernel = new GuardianKernel();
            kernel.SubmitGuard = null;
            kernel.TryValidate();
        }

        [Fact]
        public void EnableGuards_ShouldBeEnabled()
        {
            var kernel = new GuardianKernel();
            Assert.Equal(true, kernel.EnableGuards);
        }

        [Fact]
        public void QueryPolicyCollection_ShouldBeInitialized()
        {
            var kernel = new GuardianKernel();
            Assert.NotEqual(null, kernel.QueryPolicies);
        }

        [Fact]
        public void SubmitPolicyCollection_ShouldBeInitialized()
        {
            var kernel = new GuardianKernel();
            Assert.NotEqual(null, kernel.SubmitPolicies);
        }
    }
}
