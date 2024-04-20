using FluentMigrator;

namespace StudentCrudApi.Data.Migrations
{
    [Migration(19032024612)]
    public class TestMigrate : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }
    }
}
