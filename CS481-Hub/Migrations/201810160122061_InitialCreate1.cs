namespace CS481_Hub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Available_API",
                c => new
                    {
                        API_ID = c.Int(nullable: false, identity: true),
                        API_Name = c.String(),
                        void_ind = c.String(),
                    })
                .PrimaryKey(t => t.API_ID);
            
            CreateTable(
                "dbo.USER_API_XREF",
                c => new
                    {
                        API_ID = c.Int(nullable: false, identity: true),
                        USER_ID = c.String(),
                        void_ind = c.String(),
                        Available_API_API_ID = c.Int(),
                    })
                .PrimaryKey(t => t.API_ID)
                .ForeignKey("dbo.Available_API", t => t.Available_API_API_ID)
                .Index(t => t.Available_API_API_ID);
            
            CreateTable(
                "dbo.BLOGs",
                c => new
                    {
                        BLOG_ID = c.Int(nullable: false, identity: true),
                        USER_ID = c.String(),
                        BLOG_TEXT = c.String(),
                        CREATE_DT = c.DateTime(nullable: false),
                        UPDATE_DT = c.DateTime(nullable: false),
                        VOID_IND = c.String(),
                    })
                .PrimaryKey(t => t.BLOG_ID);
            
            CreateTable(
                "dbo.USER_EXT",
                c => new
                    {
                        USER_ID = c.String(nullable: false, maxLength: 128),
                        FIRST_NM = c.String(),
                        LAST_NM = c.String(),
                        ZIPCODE = c.String(),
                        void_ind = c.String(),
                    })
                .PrimaryKey(t => t.USER_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.USER_API_XREF", "Available_API_API_ID", "dbo.Available_API");
            DropIndex("dbo.USER_API_XREF", new[] { "Available_API_API_ID" });
            DropTable("dbo.USER_EXT");
            DropTable("dbo.BLOGs");
            DropTable("dbo.USER_API_XREF");
            DropTable("dbo.Available_API");
        }
    }
}
