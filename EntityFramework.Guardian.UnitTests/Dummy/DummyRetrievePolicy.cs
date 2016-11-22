// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyRetrievePolicy : IRetrievalPolicy
    {
        public RetrievalPolicyResult Check(RetrievalPolicyContext context)
        {
            return new RetrievalPolicyResult(isSuccess: false);
        }
    }
}
