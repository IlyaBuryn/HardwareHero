namespace Identity.Api.Data
{
    public static class DefaultDataConfig
    {
        public record IdentitySeedUser(
            string Id,
            string UserName,
            string FirstName,
            string LastName,
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
                new IdentitySeedRole(Roles.User, Roles.User),
                new IdentitySeedRole(Roles.Admin, Roles.Admin),
                new IdentitySeedRole(Roles.Manager, Roles.Manager),
                new IdentitySeedRole(Roles.Contributor, Roles.Contributor),
            };

        public static IEnumerable<IdentitySeedUser> DefaultIdentityUsers =>
            new[]
            {
                // Admin
                new IdentitySeedUser(Id: "9fe09964-898b-4ece-a0da-58dd32cb90d0", UserName: "admin", FirstName: "Admin", LastName: "", Email: "admin@m.com", null, "123456", Roles: new[] { "Admin", "Manager", "Contributor", "User" }),

                // Manager
                new IdentitySeedUser(Id: "f2a66e51-5af7-40b2-a38d-4157d3d1abb1", UserName: "isaac", FirstName: "Isaac", LastName: "Bishop", Email: "isaac@m.com", null, "123456", Roles: new[] { "Manager", "Contributor", "User" }),

                // Just users
                new IdentitySeedUser(Id: "6b3ba2d5-9489-48dc-a294-40ea688235fb", UserName: "ivan", FirstName: "Ivan", LastName: "Ivanovich", Email: "ivan@m.com", null, "123456", Roles: new[] { "User" }),
                new IdentitySeedUser(Id: "a02401fa-5172-43bd-9d38-2dbf220474b5", UserName: "jacob9090", FirstName: "Jacob", LastName: "Rodriges", Email: "jr.forever@example.org", null, "123456", Roles: new[] { "User" }),
                new IdentitySeedUser(Id: "f5fa4ef9-978a-40a4-a8a7-9baab1fb835e", UserName: "furyricky", FirstName: "Ricky", LastName: "Fury", Email: "rf.rf.rf@example.com", null, "123456", Roles: new[] { "User" }),

                // Contributors [by]
                new IdentitySeedUser(Id: "a9caa7b2-109b-4c21-bc24-749ff87b9b18", UserName: "johndoe", FirstName: "John", LastName: "Doe", Email: "johndoe@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "274f801d-2117-48ff-96f7-ecb9b193bc7f", UserName: "alicesmith", FirstName: "Alice", LastName: "Smith", Email: "alicesmith@example.com" , null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "46f5064b-a6fe-4b58-b303-9ed344700195", UserName: "mikejohnson", FirstName: "Mike", LastName: "Johnson", Email: "mikejohnson@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "7c5086ea-4faf-4db2-91a4-c1217a2f3029", UserName: "laurawilliams", FirstName: "Laura", LastName: "Williams", Email: "laurawilliams@example.com" , null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "b0a3bda2-525d-42c0-b7ff-8b0f68b4ca84", UserName: "davidbrown", FirstName: "David", LastName: "Brown", Email: "davidbrown@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),

                // Contributors [ru]
                new IdentitySeedUser(Id: "17f87d98-17f0-4708-a7ff-0cb4ec09b58a", UserName: "sarahwilson", FirstName: "Sarah", LastName: "Wilson", Email: "sarahwilson@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "8bc0e747-443a-4f62-a05a-6e7d8cb1516f", UserName: "peterjackson", FirstName: "Peter", LastName: "Jackson", Email: "peterjackson@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "02e91bcd-c2f5-4025-8f2f-5bac70b6924c", UserName: "emilythompson", FirstName: "Emily", LastName: "Thompson", Email: "emilythompson@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "ffcd6b86-9327-4b7a-b2ad-ec13cf531d3f", UserName: "robertrodriguez", FirstName: "Robert", LastName: "Rodriguez", Email: "robertrodriguez@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),

                // Contributors [pl]
                new IdentitySeedUser(Id: "67bfe5a9-28e2-4c55-8549-888556d2a670", UserName: "jessicamiller", FirstName: "Jessica", LastName: "Miller", Email: "jessicamiller@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "34302079-5037-499e-8703-9920be62adf7", UserName: "ryanjackson", FirstName: "Ryan", LastName: "Jackson", Email: "ryanjackson@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),
                new IdentitySeedUser(Id: "0a3a8a9f-9bb3-4e06-a050-20c953855795", UserName: "oliviamartin", FirstName: "Olivia", LastName: "Martin", Email: "oliviamartin@example.com", null, "123456", Roles: new[] { "Contributor", "User" }),            };
    }
}
