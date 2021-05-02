using $ext_safeprojectname$.Infrastructure.DataStore.EventStore;
using Microsoft.EntityFrameworkCore;
using $ext_safeprojectname$.Query.Dto.Users;
using System;

namespace $ext_safeprojectname$.Infrastructure.DataStore
{
    public class Context : DbContext
    {
        public DbSet<EventData> EventData { get; set; }
        public DbSet<UserProfileReadModel> UserProfileReadModels { get; set; }

        private readonly string _dbConnectionString;

        public Context(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(_dbConnectionString))
                throw new Exception(@"please add a connectionstring the appsetting ""ConnectionStrings.DefaultDatabase""; sample tables will be created automatically");

            optionsBuilder.UseSqlServer(_dbConnectionString);
        }
    }
}
