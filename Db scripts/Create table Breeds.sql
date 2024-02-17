USE [Dogs]
GO

/****** Object:  Table [dbo].[Breeds]    Script Date: 16-02-2024 22:06:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Breeds](
	[breed_id] [int] IDENTITY(1,1) NOT NULL,
	[breed_name] [varchar](255) NOT NULL,
	[image_path] [varchar](255) NULL,
	[cached_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[breed_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Breeds] ADD  CONSTRAINT [DF_Breeds_cached_date]  DEFAULT (getdate()) FOR [cached_date]
GO


