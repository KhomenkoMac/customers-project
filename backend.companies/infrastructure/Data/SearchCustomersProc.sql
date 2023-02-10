create proc SearchCustomers @pageNumber int, @itemsOnPage int, @name nvarchar(450), @email nvarchar(450), @companyName nvarchar(max), @phone nvarchar(50)
as
begin
	select * from dbo.FilterCustomers(@name, @email, @companyName, @phone)
	order by [Name]
	OFFSET (@pageNumber - 1) * @itemsOnPage rows 
	fetch next @itemsOnPage rows only
end
go


insert into dbo.Customers
values
	('Hello1 ccs4', 'hellow1ccs4@gmail.com', 'ccs4', '+380222222222'),
	('Hello2 ccs4', 'hellow2ccs4@gmail.com', 'ccs4', '+380333333123'),
	('Hello3 ccs4', 'hellow3ccs4@gmail.com', 'ccs4', '+380444444444');
go

exec dbo.SearchCustomers 1, 3, null, null, 'ccs4', null
go
