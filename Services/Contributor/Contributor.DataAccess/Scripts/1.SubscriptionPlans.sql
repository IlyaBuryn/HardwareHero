
IF NOT EXISTS (SELECT 1 FROM SubscriptionPlans)
BEGIN
    INSERT INTO SubscriptionPlans (Id, Price, CurrencyId, DaysCount, PriorityLevel)
    VALUES 
        ('87182507-206d-4a1d-af30-9ed592caa7c8', 0, NULL, 1095, 0), -- Free plan
        ('b6cec949-a43d-46ad-9711-9f4c49c7e7bf', 700, '2200add2-b26e-4b78-a2bd-e6721eca0bcb', 40, 3), -- For BY
        ('07c36645-a3fe-4729-9823-23a154a9b499', 200, '93e7717c-04d4-447f-8dfd-4a669cfa1d36', 40, 3); -- For US
END;
    