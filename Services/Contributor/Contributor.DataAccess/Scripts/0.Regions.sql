
IF NOT EXISTS (SELECT 1 FROM Regions)
BEGIN
    INSERT INTO Regions (Id, Country, Code, City)
    VALUES 
        ('8a29a4f7-164b-4106-b534-dc5e9209f346', 'Belarus', 'BY', NULL),
        ('aa3d6fc2-6129-4101-90ed-d49c0de13a67', 'Russia', 'RU', NULL),
        ('e1d77644-f691-4d44-a902-9065e60e82d1', 'Poland', 'PL', NULL),
        ('1d1e0818-1bff-41a0-b4a4-c39e71b6f5e3', 'France', 'FR', NULL),
        ('87454aca-2ca3-46ed-9fe5-23e1b4655d55', 'Germany', 'DE', NULL),
        ('bd01d71b-26ca-487b-ac1b-fdd29fd8cabd', 'Unated States', 'US', NULL);
END;