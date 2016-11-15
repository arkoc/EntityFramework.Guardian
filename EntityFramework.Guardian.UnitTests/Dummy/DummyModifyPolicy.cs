// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyModifyPolicy : IModifyPolicy
    {
        public ModifyPolicyResult Check(ModifyPolicyContext context)
        {
            return new ModifyPolicyResult(isSuccess: false);
        }
    }
}
