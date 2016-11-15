// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EntityFramework.Guardian.Policies;

namespace EntityFramework.Guardian.UnitTests.Dummy
{
    public class DummyRetrievePolicy : IRetrievePolicy
    {
        public RetrievePolicyResult Check(RetrievePolicyContext context)
        {
            return new RetrievePolicyResult(isSuccess: false);
        }
    }
}
