SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_get_reservations_by_filter]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[sp_get_reservations_by_filter]
GO

CREATE PROCEDURE [dbo].[sp_get_reservations_by_filter]						
		@sortOption int,
		@pageNumber int		
AS
BEGIN
	Select r.*, c.Name ContactName, c.BirthDate ContactBirthDate, c.PhoneNumber ContactPhoneNumber 
	from Reservation r
	inner join Contact c on c.ContactId = r.ContactId
END
GO

exec sp_get_reservations_by_filter 1, 2