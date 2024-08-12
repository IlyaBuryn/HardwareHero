INSERT INTO Regions (Id, Country, City)
VALUES 
    ('8a29a4f7-164b-4106-b534-dc5e9209f346', 'Belarus', NULL),
    ('aa3d6fc2-6129-4101-90ed-d49c0de13a67', 'Russia', 'St. Petersburg'),
    ('10691d98-278b-43a5-a72e-c95619525aa2', 'Russia', 'Moscow'),
    ('e1d77644-f691-4d44-a902-9065e60e82d1', 'Poland', NULL);

INSERT INTO Currencies (Id, Name, Icon)
VALUES
    ('2692c257-7248-41b6-9ff1-52d2149816bc', 'USD', 'USD.svg'),
    ('75c3f4c9-2784-4df7-bc25-42c3e983dae7', 'EUR', 'EUR.svg'),
    ('e51f88f7-b008-47fd-822b-cbb722ce593d', 'BYN', 'BYN.svg'),
    ('057689ab-bfa0-49b7-85b2-27fcfa4c7f14', 'RUB', 'RUB.svg'),
    ('a173c118-04ec-468a-89f9-e0a2b5d490b4', 'PLN', 'PLN.svg');

INSERT INTO ContributorConfirmInfos (Id, IsConfirmed, TimeStamp)
VALUES
    ('e4b1bab0-8958-4eaf-b371-4fba600e7938', 1, GETDATE()),
    ('e635e170-871f-4ab1-a80c-b9f275a3ec6e', 1, GETDATE()),
    ('1d6d658d-fa5d-4557-ad59-d3265f237837', 1, GETDATE()),
    ('ee8325d7-e7d7-47a5-ba8b-4638ec3ccfe8', 1, GETDATE()),
    ('70e657a3-754c-4aa4-982a-a536a7614882', 1, GETDATE()),
    ('8766ff10-bea3-43d2-8bf3-b5b389f2f261', 1, GETDATE()),
    ('518e3fc8-3bde-4d30-ab1c-ea0a81a220fc', 1, GETDATE()),
    ('9a468d7e-0502-4e7c-99ab-01c694ccb841', 1, GETDATE()),
    ('fde806f8-8ac2-47b0-8e90-3c8e046a5ba6', 1, GETDATE()),
    ('753482da-d39e-4bc3-a3ba-590c60acdc76', 1, GETDATE()),
    ('9d2c9e81-62a8-4224-82d4-c881f15db7ee', 1, GETDATE()),
    ('38aa959c-09c1-458a-91e9-7c7d5e670836', 1, GETDATE()),
    ('a3a11670-804b-4b9b-a6f6-1d08126cc777', 1, GETDATE()),
    ('67ff9526-1166-4523-9155-d94c993a1a6b', 1, GETDATE());

INSERT INTO SubscriptionPlans (Id, Price, PriorityLevel, DaysCount)
VALUES 
    ('a0f51305-ad76-43ed-b255-02a65721d0b2', 100.0, 1, 30),
    ('7f11de2f-33be-4285-9f4d-4376a820c43a', 500.0, 2, 30);

INSERT INTO SubscriptionPlanInfos (Id, PlanId)
VALUES
    ('7489a7e5-1c08-4125-b734-da05613b9468', 'a0f51305-ad76-43ed-b255-02a65721d0b2'),
    ('b411277e-d482-4f88-a3b8-0a85e4354a43', 'a0f51305-ad76-43ed-b255-02a65721d0b2');

INSERT INTO ContributorExcellences (Id, Name, Logo, Description, Phone, MainWebLink, MainApiLink, RegionId, CurrencyId)
VALUES
    ('611cd651-28bf-4039-8c38-a9d40be8864d', 'fk.by', 'fk.by_logo.png', 'Is confirmed contributor (FK.by)', '+1234567890', NULL, NULL, '8a29a4f7-164b-4106-b534-dc5e9209f346', 'e51f88f7-b008-47fd-822b-cbb722ce593d'),
    ('ae56b294-7daa-466a-b1a6-56c80deb4504', '4pc.by', '4pc.by_logo.png', 'Is confirmed contributor (4PC.by)', '+1234567890', NULL, NULL, '8a29a4f7-164b-4106-b534-dc5e9209f346', 'e51f88f7-b008-47fd-822b-cbb722ce593d'),
    ('67fd959e-c9cf-4159-8a90-658be0f8884a', 'RAM.by', 'RAM.by_logo.png', 'Is confirmed contributor (RAM.by)', '+1234567890', NULL, NULL, '8a29a4f7-164b-4106-b534-dc5e9209f346', 'e51f88f7-b008-47fd-822b-cbb722ce593d'),
    ('d628f359-e0fa-42a1-b9a6-989fd8be270a', '7745.by', '7745.by_logo.png', 'Is confirmed contributor (7745)', '+1234567890', NULL, NULL, '8a29a4f7-164b-4106-b534-dc5e9209f346', 'e51f88f7-b008-47fd-822b-cbb722ce593d'),
    ('5c8c1ce1-eaf2-40b3-9f35-ba5431eb0c32', 'Technoby.by', 'Technoby.by_logo.png', 'Is confirmed contributor (Technoby.by)', '+1234567890', NULL, NULL, '8a29a4f7-164b-4106-b534-dc5e9209f346', 'e51f88f7-b008-47fd-822b-cbb722ce593d'),
    
    ('ea5d7cfa-f3e6-4898-86bf-0eec7c2113c1', 'kns.ru', 'kns.ru_logo.png', 'Is confirmed contributor (KNS)', '+1234567890', NULL, NULL, '10691d98-278b-43a5-a72e-c95619525aa2', '057689ab-bfa0-49b7-85b2-27fcfa4c7f14'),
    ('44bce2da-50d1-418d-b220-6086f5c38def', 'compday.ru', 'compday.ru_logo.png', 'Is confirmed contributor (Compday)', '+1234567890', NULL, NULL, '10691d98-278b-43a5-a72e-c95619525aa2', '057689ab-bfa0-49b7-85b2-27fcfa4c7f14'),
    ('37cca2ef-5327-4238-8ffd-5d517b31ab96', 'xcom-shop.ru', 'xcom-shop.ru_logo.png', 'Is confirmed contributor (XCOM-shop)', '+1234567890', NULL, NULL, '10691d98-278b-43a5-a72e-c95619525aa2', '057689ab-bfa0-49b7-85b2-27fcfa4c7f14'),
    ('0673a4bc-8bff-4de8-bc5a-069e20267920', 'pc-arena.ru', 'pc-arena.ru_logo.png', 'Is confirmed contributor (PC-arena)', '+1234567890', NULL, NULL, '10691d98-278b-43a5-a72e-c95619525aa2', '057689ab-bfa0-49b7-85b2-27fcfa4c7f14'),
    
    ('0711c4c7-1387-4df7-9055-6b6294f94be3', 'allegro.pl', 'allegro.pl_logo.png', 'Is confirmed contributor (Allegro)', '+1234567890', NULL, NULL, 'e1d77644-f691-4d44-a902-9065e60e82d1', 'a173c118-04ec-468a-89f9-e0a2b5d490b4'),
    ('2f6c0bf7-8b3f-4509-ac71-1b0f6bd1e10e', 'avans.pl', 'avans.pl_logo.png', 'Is confirmed contributor (Avans)', '+1234567890', NULL, NULL, 'e1d77644-f691-4d44-a902-9065e60e82d1', 'a173c118-04ec-468a-89f9-e0a2b5d490b4'),
    ('4576e636-fa93-4fcb-935c-c1dc10cc7dad', 'MediaMarkt.pl', 'MediaMarkt.pl_logo.png', 'Is confirmed contributor (MediaMarkt)', '+1234567890', NULL, NULL, 'e1d77644-f691-4d44-a902-9065e60e82d1', 'a173c118-04ec-468a-89f9-e0a2b5d490b4'),
    
    ('6ca8a01f-1b81-4446-85ca-0c4f52c02b72', 'planetacomp.com', 'planetacomp.com_logo.png', 'Is confirmed contributor (Planetacomp)', '+1234567890', NULL, NULL, 'aa3d6fc2-6129-4101-90ed-d49c0de13a67', '057689ab-bfa0-49b7-85b2-27fcfa4c7f14'),
    ('4cd8eaa2-a559-4257-b1f9-121601d32510', 'royal-computers.ru', 'royal-computers.ru_logo.png', 'Is confirmed contributor (royal-computers.ru)', '+1234567890', NULL, NULL, 'aa3d6fc2-6129-4101-90ed-d49c0de13a67', '057689ab-bfa0-49b7-85b2-27fcfa4c7f14');
   

INSERT INTO Contributors (Id, UserId, ContributorConfirmInfoId, ContributorExcellenceId, SubscriptionPlanInfoId)
VALUES
    ('b863dd75-613b-4811-8977-19d79677c48f', 'a9caa7b2-109b-4c21-bc24-749ff87b9b18', 'e4b1bab0-8958-4eaf-b371-4fba600e7938', '611cd651-28bf-4039-8c38-a9d40be8864d', '7489a7e5-1c08-4125-b734-da05613b9468'),
    ('d962a3b8-34e4-40d0-a761-2f12668725ea', '274f801d-2117-48ff-96f7-ecb9b193bc7f', 'e635e170-871f-4ab1-a80c-b9f275a3ec6e', 'ae56b294-7daa-466a-b1a6-56c80deb4504', NULL),
    ('e31b2459-c089-4244-884c-f633fa7b66d8', '46f5064b-a6fe-4b58-b303-9ed344700195', '1d6d658d-fa5d-4557-ad59-d3265f237837', '67fd959e-c9cf-4159-8a90-658be0f8884a', NULL),
    ('72f01826-2460-491f-9f80-f581c38971cc', '7c5086ea-4faf-4db2-91a4-c1217a2f3029', 'ee8325d7-e7d7-47a5-ba8b-4638ec3ccfe8', 'd628f359-e0fa-42a1-b9a6-989fd8be270a', 'b411277e-d482-4f88-a3b8-0a85e4354a43'),
    ('3526182b-65c5-4cbd-bac3-801ecd323726', 'b0a3bda2-525d-42c0-b7ff-8b0f68b4ca84', '70e657a3-754c-4aa4-982a-a536a7614882', '5c8c1ce1-eaf2-40b3-9f35-ba5431eb0c32', NULL),

    ('055a00a2-fca3-4bd0-8aa0-a57e02ac55f3', '17f87d98-17f0-4708-a7ff-0cb4ec09b58a', '8766ff10-bea3-43d2-8bf3-b5b389f2f261', 'ea5d7cfa-f3e6-4898-86bf-0eec7c2113c1', NULL),
    ('db3a936d-8681-4453-806e-519a05d6a6cc', '8bc0e747-443a-4f62-a05a-6e7d8cb1516f', '518e3fc8-3bde-4d30-ab1c-ea0a81a220fc', '44bce2da-50d1-418d-b220-6086f5c38def', NULL),
    ('bfabfc78-3689-4ad2-8e46-b0814b340507', '02e91bcd-c2f5-4025-8f2f-5bac70b6924c', '9a468d7e-0502-4e7c-99ab-01c694ccb841', '37cca2ef-5327-4238-8ffd-5d517b31ab96', NULL),
    ('e25ac60c-ad9d-45ac-97f0-431227564b01', 'ffcd6b86-9327-4b7a-b2ad-ec13cf531d3f', 'fde806f8-8ac2-47b0-8e90-3c8e046a5ba6', '0673a4bc-8bff-4de8-bc5a-069e20267920', NULL),

    ('bbea4186-9e3a-44de-a626-fb7f88f70d27', '67bfe5a9-28e2-4c55-8549-888556d2a670', '753482da-d39e-4bc3-a3ba-590c60acdc76', '0711c4c7-1387-4df7-9055-6b6294f94be3', NULL),
    ('e87fb692-d01b-4e5a-96b4-423d51860897', '34302079-5037-499e-8703-9920be62adf7', '9d2c9e81-62a8-4224-82d4-c881f15db7ee', '2f6c0bf7-8b3f-4509-ac71-1b0f6bd1e10e', NULL),
    ('7e23282c-0caa-4548-83da-9c0bf1f554ec', '0a3a8a9f-9bb3-4e06-a050-20c953855795', '38aa959c-09c1-458a-91e9-7c7d5e670836', '4576e636-fa93-4fcb-935c-c1dc10cc7dad', NULL),

    ('e2d66c0b-f470-427b-a8cf-241fba0c6c84', 'bad8a170-deb8-44e7-965a-2f660079d5ed', 'a3a11670-804b-4b9b-a6f6-1d08126cc777', '6ca8a01f-1b81-4446-85ca-0c4f52c02b72', NULL),
    ('94115dcb-c1de-4075-b245-f6df31c2f6c4', '373ec651-0d88-45f4-90dd-4b2a98500ecb', '67ff9526-1166-4523-9155-d94c993a1a6b', '4cd8eaa2-a559-4257-b1f9-121601d32510', NULL);