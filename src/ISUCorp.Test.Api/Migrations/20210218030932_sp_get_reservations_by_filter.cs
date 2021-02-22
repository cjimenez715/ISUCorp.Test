using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISUCorp.Test.Api.Migrations
{
    public partial class sp_get_reservations_by_filter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @" SET QUOTED_IDENTIFIER OFF 
						GO
						SET ANSI_NULLS OFF 
						GO

						if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_get_reservations_pager]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
							drop procedure [dbo].[sp_get_reservations_pager]
						GO

						CREATE PROCEDURE [dbo].[sp_get_reservations_pager]						
								@sortOption int,
								@pageNumber int		
						AS
						BEGIN
							Declare @startRowIndex int,
									@numRows int
	
							select @numRows = 10;
							select @startRowIndex = (@numRows * @pageNumber);

							SELECT *
							FROM
							(Select ROW_NUMBER() OVER (ORDER BY r.ReservationId )as RowNum,
									r.*, c.Name ContactName, c.BirthDate ContactBirthDate, c.PhoneNumber ContactPhoneNumber 
							from Reservation r
							inner join Contact c on c.ContactId = r.ContactId) as RowResult
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
