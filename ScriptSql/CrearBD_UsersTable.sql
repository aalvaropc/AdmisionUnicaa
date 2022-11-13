create database MyCompany
go
use MyCompany
go
create table Users(
UserID int identity(1000,1) primary key,
LoginName nvarchar(100) unique not null,
Password nvarchar(100) not null,
FirstName nvarchar(100) not null,
LastName nvarchar(100) not null,
Email nvarchar(100) not null
)

insert into Users values ('admin', 'admin', 'Alvaro', 'Penia', 'alvaro@company.com')

declare @user nvarchar(100)='admin'
declare @pass nvarchar(100)='admin'
select * from Users where LoginName=@user and Password=@pass


