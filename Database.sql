Create Table Users(
Id int identity(1,1),
UserName nvarchar(100),
password nvarchar(500)
)


Create Table Category(
Id int identity(1,1) Primary key,
Name nvarchar(100),
)

Create Table SubCategory(
Id int identity(1,1) Primary Key,
Name nvarchar(100),
CategoryId int Foreign key references Category(Id)
)



Create Table Blogs(
Id int identity(1,1) Primary Key,
Headline nvarchar(max),
PublishedDate Datetime,
Description nvarchar(max),
ImageOne nvarchar(max),
ImageTwo nvarchar(max),
CategoryId int Foreign key references SubCategory(Id)
)