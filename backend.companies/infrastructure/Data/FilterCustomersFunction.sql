insert into dbo.Customers
values
	('Hello1', 'hellow1@gmail.com', 'ccs2', '+380778896655'),
	('Hello2', 'hellow2@gmail.com', 'ccs3', '+380778896655'),
	('Hello3', 'hellow3@gmail.com', 'ccs4', '+380333333333');

select * from Customers

select * from FilterCustomers('Hello', null,'ccs',null)
go

create function FilterCustomers(
	@name nvarchar(max), 
	@email nvarchar(max), 
	@companyName nvarchar(max), 
	@phone nvarchar(max)
)
returns table
	return (select * from Customers where 
		[Name]=ISNULL(@name, [Name]) 
	and [Email]=ISNULL(@email, [Email])
	and [CompanyName]=ISNULL(@companyName,[CompanyName])
	and [Phone]=ISNULL(@phone,[Phone])
	)
go

drop function dbo.FilterCustomers -- drop
go