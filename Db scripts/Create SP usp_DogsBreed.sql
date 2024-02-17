USE [Dogs]
GO

/****** Object:  StoredProcedure [dbo].[usp_DogsBreed]    Script Date: 16-02-2024 22:08:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--EXEC    [dbo].[usp_DogsBreed]
--		@breed_name = N'hound-afghan',
--		@image_path = N'https://images.dog.ceo/breeds/hound-afghan/n02088094_1003.jpg'

CREATE PROCEDURE [dbo].[usp_DogsBreed] 
  (
   @breed_name  varchar(255),
   @image_path varchar(255)
  )
AS
BEGIN

   IF Not EXISTS (SELECT breed_id  FROM [dbo].[Breeds] 
                   WHERE breed_name = @breed_name)
   BEGIN
	  INSERT INTO [dbo].[Breeds](breed_name, image_path)
       VALUES (@breed_name, @image_path)
    END
	 
	 SELECT [breed_id]
		  ,[breed_name]
		  ,[image_path]
	  FROM [dbo].[Breeds] where  breed_name = @breed_name
   
	
END
GO


