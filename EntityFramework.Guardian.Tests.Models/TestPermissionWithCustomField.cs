// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Tests.Models
{
    public class TestPermissionWithCustomField : TestPermission
    {
        public string CustomField { get; set; }
    }
}
