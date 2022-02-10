SELECT * FROM Customer
CREATE TABLE  Customer(
 id int primary key identity, 
 name varchar(20) not null,
 address varchar(100) not null,
 city varchar(30) not null,
 state varchar(30) not null,
)

-- Insert
Create PROC InsertCustomer (@id int, @name varchar(20), @address varchar(100), @city varchar(30), @state varchar(30))
AS
BEGIN
	if(exists(SELECT * FROM Customer where id = @id))
	BEGIN
		RETURN 0
	END
	ELSE
	BEGIN
		INSERT INTO Customer values ( @name, @address, @city, @state)
		return 1
	END
END

declare @flag int 
exec @flag = InsertCustomer 2,'Grace','111 MountainTop St.', 'Edmonton', 'AB' 
print @flag

-- GET BY ID
ALTER PROC GetCustomerByID (@id int)
AS
BEGIN
	SELECT * from Customer where id=@id
END


declare @name varchar(20), @address varchar(100), @city varchar(30),  @state varchar(30)
exec GetCustomerByID 1

print @name

-- DELETE
CREATE PROC DeleteCustomer (@id int)
AS
BEGIN
	DELETE from Customer where id = @id
END

-- GET ALL

--ALTER PROC GetAllCustomers (@id int out, @name varchar(20) out, @address varchar(100) out, @city varchar(30) out, @state varchar(30) out)
--AS
--BEGIN
--	SELECT @id=id, @name = name, @address = address, @city = city, @state=state FROM Customer
--END

--declare @id int, @name varchar(20), @address varchar(100), @city varchar(30),  @state varchar(30)
--exec GetAllCustomers @id out, @name out, @address out, @city out, @state out
--print @name

ALTER PROC GetAllCustomers 
AS
BEGIN
	SELECT * FROM Customer
END

Exec GetAllCustomers

-- Update 

Alter PROC UpdateCustomer (@id int,@address varchar(100), @city varchar(30),  @state varchar(30))
AS 
BEGIN
	UPDATE Customer SET address = @address, city=@city,state=@state  where id =@id
END

exec UpdateCustomer 2,'2213 Ainslie SW','Redeer','AB'

SELECT * FROM Customer