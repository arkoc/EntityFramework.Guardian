using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Policies
{
    public interface IRetrieveProtectionPolicy
    {
        bool Check(RetrievePolicyContext context, GuardianKernel kernel);
    }
}
