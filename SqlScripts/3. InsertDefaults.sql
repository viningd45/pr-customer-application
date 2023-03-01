USE Consulting

INSERT INTO dbo.Customers
(
    Name,
    PhoneNumber,
	AddressLineOne,
	AddressLineTwo,
	City,
	State,
	ZipCode,
	ZipCodeAdditional
)
VALUES
(   N'Daniel''s Bakery',  -- Name - nvarchar(70)
    N'3184582814',  -- PhoneNumber - nvarchar(10)
	'123 Fake Ln.',
	NULL,
	'Shreveport',
	'Louisiana',
	'71047',
	NULL
)

INSERT INTO dbo.CustomerOpenHours
(
    CustomerId,
	DayOfWeek,
    Opening,
    Closing
)
VALUES
(   
	1,         -- CustomerId - int
	1,         -- CustomerId - int
    '08:00', -- Opening - time
    '17:00'  -- Closing - time
)
,(   
	1,         -- CustomerId - int
	2,         -- CustomerId - int
    '08:00', -- Opening - time
    '17:00'  -- Closing - time
),(   
	1,         -- CustomerId - int
	3,         -- CustomerId - int
    '08:00', -- Opening - time
    '17:00'  -- Closing - time
),(   
	1,         -- CustomerId - int
	4,         -- CustomerId - int
    '08:00', -- Opening - time
    '17:00'  -- Closing - time
),(   
	1,         -- CustomerId - int
	5,         -- CustomerId - int
    '08:00', -- Opening - time
    '17:00'  -- Closing - time
)