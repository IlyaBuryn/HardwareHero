namespace Identity.Api.Data
{
    public static class DefaultDataConfig
    {
        public record IdentitySeedUser(
            string Id,
            string UserName,
            string FullName,
            string Email,
            DateTime? RegistrationDate,
            string? Password,
            string[] Roles,
            bool EmailConfirmed = true);

        public record IdentitySeedRole(
            string Name,
            string NormalizedName);

        public static IEnumerable<IdentitySeedRole> DefaultIdentityRoles =>
            new[]
            {
                new IdentitySeedRole("User", "User"),
                new IdentitySeedRole("Admin", "Admin"),
                new IdentitySeedRole("Manager", "Manager"),
                new IdentitySeedRole("Contributor", "Contributor"),
            };

        public static IEnumerable<IdentitySeedUser> DefaultIdentityUsers =>
            new[]
            {
                // Admin
                new IdentitySeedUser(Id: "9fe09964-898b-4ece-a0da-58dd32cb90d0", UserName: "admin", FullName: "Admin", Email: "admin@m.com", null, "123456", Roles: new[] { "Admin", "Manager", "Contributor", "User" }),

                // Manager
                new IdentitySeedUser(Id: "f2a66e51-5af7-40b2-a38d-4157d3d1abb1", UserName: "isaac", FullName: "Isaac Bishop", Email: "isaac@m.com", null, "123456", Roles: new[] { "Manager", "Contributor", "User" }),

                // Just users
                new IdentitySeedUser(Id: "6b3ba2d5-9489-48dc-a294-40ea688235fb", UserName: "ivan", FullName: "Ivan Ivanovich", Email: "ivan@m.com", null, "123456", Roles: new[] { "User" }),
                new IdentitySeedUser(Id: "a02401fa-5172-43bd-9d38-2dbf220474b5", UserName: "jacob9090", FullName: "Jacob Rodriges", Email: "jr.forever@example.org", null, "123456", Roles: new[] { "User" }),
                new IdentitySeedUser(Id: "f5fa4ef9-978a-40a4-a8a7-9baab1fb835e", UserName: "furyricky", FullName: "Ricky Fury", Email: "rf.rf.rf@example.com", null, "123456", Roles: new[] { "User" }),

                // Contributors [by]
                new IdentitySeedUser(Id: "a9caa7b2-109b-4c21-bc24-749ff87b9b18", UserName: "johndoe", FullName: "John Doe", Email: "johndoe@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "274f801d-2117-48ff-96f7-ecb9b193bc7f", UserName: "alicesmith", FullName: "Alice Smith", Email: "alicesmith@example.com" , null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "46f5064b-a6fe-4b58-b303-9ed344700195", UserName: "mikejohnson", FullName: "Mike Johnson", Email: "mikejohnson@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "7c5086ea-4faf-4db2-91a4-c1217a2f3029", UserName: "laurawilliams", FullName: "Laura Williams", Email: "laurawilliams@example.com" , null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "b0a3bda2-525d-42c0-b7ff-8b0f68b4ca84", UserName: "davidbrown", FullName: "David Brown", Email: "davidbrown@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),

                // Contributors [ru.msk]
                new IdentitySeedUser(Id: "17f87d98-17f0-4708-a7ff-0cb4ec09b58a", UserName: "sarahwilson", FullName: "Sarah Wilson", Email: "sarahwilson@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "8bc0e747-443a-4f62-a05a-6e7d8cb1516f", UserName: "peterjackson", FullName: "Peter Jackson", Email: "peterjackson@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "02e91bcd-c2f5-4025-8f2f-5bac70b6924c", UserName: "emilythompson", FullName: "Emily Thompson", Email: "emilythompson@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "ffcd6b86-9327-4b7a-b2ad-ec13cf531d3f", UserName: "robertrodriguez", FullName: "Robert Rodriguez", Email: "robertrodriguez@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),

                // Contributors [pl]
                new IdentitySeedUser(Id: "67bfe5a9-28e2-4c55-8549-888556d2a670", UserName: "jessicamiller", FullName: "Jessica Miller", Email: "jessicamiller@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "34302079-5037-499e-8703-9920be62adf7", UserName: "ryanjackson", FullName: "Ryan Jackson", Email: "ryanjackson@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "0a3a8a9f-9bb3-4e06-a050-20c953855795", UserName: "oliviamartin", FullName: "Olivia Martin", Email: "oliviamartin@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),

                // Contributors [ru.spb]
                new IdentitySeedUser(Id: "bad8a170-deb8-44e7-965a-2f660079d5ed", UserName: "sophiawalker", FullName: "Sophia Walker", Email: "sophiawalker@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "373ec651-0d88-45f4-90dd-4b2a98500ecb", UserName: "danielharris", FullName: "Daniel Harris", Email: "danielharris@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
            };
    }
}
