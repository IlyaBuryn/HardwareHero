namespace Users.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseMigration<TContext>(this IApplicationBuilder app, string migrationAssembly) where TContext : DbContext
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                //serviceScope.ServiceProvider.GetService<TContext>()!
                //    .Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();

                // Set the migration assembly
                var contextOptions = context.Database.GetDbConnection().ConnectionString;
                var optionsBuilder = new DbContextOptionsBuilder<TContext>();
                optionsBuilder.UseSqlServer(contextOptions, sqlOptions => sqlOptions.MigrationsAssembly(migrationAssembly));

                // Create new context with migration assembly set
                var newContext = (TContext)Activator.CreateInstance(typeof(TContext), optionsBuilder.Options)!;
                newContext.Database.Migrate();
            }
        }
    }
}
