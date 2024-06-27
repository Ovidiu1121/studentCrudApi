using FluentMigrator;

namespace StudentCrudApi.Data.Migrations
{
    [Migration(19032024)]
    public class CreateSchema : Migration
    {
        public override void Down()
        {
          
        }

        public override void Up()
        {
            Create.Table("student")
                 .WithColumn("id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("name").AsString(128).NotNullable()
                 .WithColumn("age").AsInt32().NotNullable()
                 .WithColumn("specialization").AsString(128).NotNullable();
        }
    }
}
