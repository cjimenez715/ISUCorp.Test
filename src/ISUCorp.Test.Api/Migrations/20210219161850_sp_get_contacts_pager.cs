using Microsoft.EntityFrameworkCore.Migrations;

namespace ISUCorp.Test.Api.Migrations
{
    public partial class sp_get_contacts_pager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var sp = @" SET QUOTED_IDENTIFIER OFF 
						GO
						SET ANSI_NULLS OFF 
						GO

						if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_get_contacts_pager]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
							drop procedure [dbo].[sp_get_contacts_pager]
						GO

						CREATE PROCEDURE [dbo].[sp_get_contacts_pager]						
								@pageNumber int		
						AS
						BEGIN
							Declare @startRowIndex int,
									@numRows int
	
							select @numRows = 10;
							select @startRowIndex = (@numRows * @pageNumber);

							SELECT *
							FROM
							(Select ROW_NUMBER() OVER (ORDER BY c.ContactId ) as RowNum,
									c.ContactId, c.Name, c.PhoneNumber, c.BirthDate, ct.Name ContactTypeName
							from Contact c
								 inner join ContactType ct on c.ContactTypeId = ct.ContactTypeId
							) as RowResult
							where RowNum  between @startRowIndex + 1 and @startRowIndex + @numRows
							Order by RowNum
						END
						GO";

			migrationBuilder.Sql(sp);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
