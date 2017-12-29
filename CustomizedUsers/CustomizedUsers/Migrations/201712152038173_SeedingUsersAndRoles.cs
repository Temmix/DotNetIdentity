namespace CustomizedUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingUsersAndRoles : DbMigration
    {
        public override void Up()
        { 
            // This is the perfect way to seed your users and roles on any database. with password of !1Seikos
            Sql(@"
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Firstname], [Lastname], [Telephone], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0de953cd-4c1a-4721-b625-2d3db8e5effe', N'Ife', N'Makinde', N'07511123111', N'Ife.Makinde@GMail.com', 0, N'AJMr0I6v1ds1qwoaqYq+ZeKYKgnqAV45vWzZ2ScqMja8z8dCMYquj7h1Lk46AdO6zw==', N'dadc9ab4-37d7-45ec-9c70-cede5084f927', NULL, 0, 0, NULL, 1, 0, N'Ife.Makinde@GMail.com')
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Firstname], [Lastname], [Telephone], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'bafec5de-4119-45ed-81da-664ad5337a2e', N'Temi', N'Makinde', N'07525637490', N'Temi.Makinde@GMail.com', 0, N'APVArhhWVBeX0+LSfJQXh4uoFuhZ5L2S3FXiBl7m/pVmGIxMQcxxSkKct7ysuJGUNQ==', N'6a64977e-459c-49ed-8d17-517ccb3aeeb6', NULL, 0, 0, NULL, 1, 0, N'Temi.Makinde@GMail.com')
            
                    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a4726b39-d3a9-4280-9eaa-362d99963ca8', N'CanManageUsers')
                    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bafec5de-4119-45ed-81da-664ad5337a2e', N'a4726b39-d3a9-4280-9eaa-362d99963ca8')
               "); 
        }
        
        public override void Down()
        {
        }
    }
}
