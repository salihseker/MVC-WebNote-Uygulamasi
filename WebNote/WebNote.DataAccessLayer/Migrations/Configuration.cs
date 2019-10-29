namespace WebNote.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebNote.DataAccessLayer.DatabaseContext>
    {
        //Migration işlemleri Nuget Console Komutları.
        //enable-Migrations
        //update-database
        //update-database -verbose
        //update-database -Force
        //update-database -Script
        //update-database -TargetMigration:test

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WebNote.DataAccessLayer.DatabaseContext";
        }

        protected override void Seed(WebNote.DataAccessLayer.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
