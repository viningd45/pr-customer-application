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
(   N'Daniel''s Bakery',  
    N'3184582814',  
	'123 Fake Ln.',
	NULL,
	'Shreveport',
	'Louisiana',
	'71047',
	NULL
),
(   N'Superior Grill',  
    N'3188693243',  
	'6123 Line Ave.',
	NULL,
	'Shreveport',
	'Louisiana',
	'71106',
	NULL
),
(   N'Frymaster', 
    N'3188651711',  
	'5489 Campus Dr.',
	NULL,
	'Shreveport',
	'Louisiana',
	'71129',
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
	1,         
	1,         
    '08:00', 
    '17:00'  
)
,(   
	1,         
	2,         
    '08:00', 
    '17:00'  
),(   
	1,         
	3,         
    '08:00', 
    '17:00'  
),(   
	1,       
	4,      
    '08:00',
    '17:00' 
),(   
	1,       
	5,        
    '08:00', 
    '17:00' 
),
(   
	2,         
	0,         
    '11:00', 
    '22:00'  
),
(   
	2,         
	1,         
    '11:00', 
    '22:00'  
),
(   
	2,         
	2,         
    '11:00', 
    '22:00'  
),
(   
	2,         
	3,         
    '11:00', 
    '22:00'  
),
(   
	2,         
	4,         
    '11:00', 
    '22:00'  
),
(   
	2,         
	5,         
    '11:00', 
    '22:00'  
),
(   
	2,         
	6,         
    '11:00', 
    '22:00'  
),
(   
	3,         
	1,         
    '8:00', 
    '17:00'  
),
(   
	3,         
	2,         
    '8:00', 
    '17:00'  
),
(   
	3,         
	3,         
    '8:00', 
    '17:00'  
),
(   
	3,         
	4,         
    '8:00', 
    '17:00'  
),
(   
	3,         
	5,         
    '8:00', 
    '17:00'  
)

SELECT * FROM dbo.Customers c
LEFT JOIN dbo.CustomerOpenHours coo ON coo.CustomerId = c.Id
ORDER BY c.Id, coo.DayOfWeek