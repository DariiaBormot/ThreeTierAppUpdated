
CREATE OR ALTER PROCEDURE [dbo].[spManufacturers_Delete]
	@id int
AS
BEGIN
	DELETE  FROM Manufacturers
	WHERE Id = @id
END
