using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Guardian.Core.Policies
{
    public interface IModifyProtectionPolicy
    {
        bool Check(ModifyPolicyContext context, GuardianKernel kernel);
    }
}
