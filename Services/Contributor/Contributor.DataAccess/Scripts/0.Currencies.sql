
IF NOT EXISTS (SELECT 1 FROM Currencies)
BEGIN
    INSERT INTO Currencies (Id, Code, Symbol)
    VALUES
        ('2200add2-b26e-4b78-a2bd-e6721eca0bcb', 'BYN', N'Br'),
        ('8cb73308-b77d-428b-9cf2-6ff3c1f33709', 'RUB', N'₽'),
        ('0bb32ff2-4821-4828-9a6d-418c64a4989f', 'PLN', N'zł'),
        ('93e7717c-04d4-447f-8dfd-4a669cfa1d36', 'USD', N'$'),
        ('b6d10808-c502-42b9-a07f-51406296f8a3', 'EUR', N'€'),
        ('a5df38ad-4501-41f5-bd61-52206b25c61c', 'GBP', N'£'),
        ('1d3b0d48-8ded-48e1-bcd7-508b599e348f', 'JPY', N'¥'),
        ('9ac95404-243e-409c-94c5-e448edd9aba5', 'CNY', N'¥'),
        ('2dd3f70a-fa7d-465b-a453-5d0177cc33a6', 'CHF', N'₣'),
        ('9f16b901-cb8b-4abf-91ba-e9041cf05ce9', 'AUD', N'$'),
        ('3d847c6e-d13f-4541-815f-ea0a2dda2c39', 'CAD', N'$');
END;