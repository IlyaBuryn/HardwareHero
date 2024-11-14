
IF NOT EXISTS (SELECT 1 FROM SpecificationFilterTypes)
BEGIN
	INSERT INTO SpecificationFilterTypes (Id, Name) VALUES
		('e728422a-af74-426d-9178-820d5b30d49f', 'String'),
		('584462ea-401f-487e-9f06-b6d2c708be02', 'Int'),
		('627f4a75-b394-4928-a8fd-71a85f3d3ba2', 'Decimal'),
		('52d20167-cc42-41a8-a603-b9b160f72ecd', 'Boolean'),
		('431f7e6a-1676-4d43-84f2-55ad6c9d41ed', 'Range'),
		('5a6933e6-9d4e-4e7c-8881-d7c14b7029f6', 'List');
END;