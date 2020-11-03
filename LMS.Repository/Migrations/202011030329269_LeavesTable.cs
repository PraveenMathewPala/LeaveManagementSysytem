namespace Repositary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeavesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        Lid = c.Int(nullable: false, identity: true),
                        Eid = c.Int(nullable: false),
                        EmployeeName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Number = c.Int(nullable: false),
                        DepartmentName = c.String(),
                        Status = c.Boolean(nullable: false),
                        Projectid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Lid);
            
            AddColumn("dbo.AspNetUsers", "Projectid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Projectid");
            DropTable("dbo.Leaves");
        }
    }
}
