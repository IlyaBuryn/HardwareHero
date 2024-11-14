
IF NOT EXISTS (SELECT 1 FROM ContributorConfirmInfos)
BEGIN
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
        ('38aa959c-09c1-458a-91e9-7c7d5e670836', 1, GETDATE());
END;


IF NOT EXISTS (SELECT 1 FROM ContributorExcellences)
BEGIN
    INSERT INTO ContributorExcellences (Id, Name, LogoUrl, LogoName, Description, Phone, MainWebLink, MainApiLink, RegionId, CurrencyId)
    VALUES
        ('611cd651-28bf-4039-8c38-a9d40be8864d', 'fk.by', NULL, 'fk.by_logo.png', 'Is confirmed contributor (FK.by)', '+1234567890', NULL, NULL, '8a29a4f7-164b-4106-b534-dc5e9209f346', '2200add2-b26e-4b78-a2bd-e6721eca0bcb'),
        ('ae56b294-7daa-466a-b1a6-56c80deb4504', '4pc.by', NULL, '4pc.by_logo.png', 'Is confirmed contributor (4PC.by)', '+1234567890', NULL, NULL, '8a29a4f7-164b-4106-b534-dc5e9209f346', '2200add2-b26e-4b78-a2bd-e6721eca0bcb'),
        ('67fd959e-c9cf-4159-8a90-658be0f8884a', 'RAM.by', NULL, 'RAM.by_logo.png', 'Is confirmed contributor (RAM.by)', '+1234567890', NULL, NULL, '8a29a4f7-164b-4106-b534-dc5e9209f346', '2200add2-b26e-4b78-a2bd-e6721eca0bcb'),
        ('d628f359-e0fa-42a1-b9a6-989fd8be270a', '7745.by', NULL, '7745.by_logo.png', 'Is confirmed contributor (7745)', '+1234567890', NULL, NULL, '8a29a4f7-164b-4106-b534-dc5e9209f346', '2200add2-b26e-4b78-a2bd-e6721eca0bcb'),
        ('5c8c1ce1-eaf2-40b3-9f35-ba5431eb0c32', 'Technoby.by', NULL, 'Technoby.by_logo.png', 'Is confirmed contributor (Technoby.by)', '+1234567890', NULL, NULL, '8a29a4f7-164b-4106-b534-dc5e9209f346', '2200add2-b26e-4b78-a2bd-e6721eca0bcb'),
    
        ('ea5d7cfa-f3e6-4898-86bf-0eec7c2113c1', 'kns.ru', NULL, 'kns.ru_logo.png', 'Is confirmed contributor (KNS)', '+1234567890', NULL, NULL, 'aa3d6fc2-6129-4101-90ed-d49c0de13a67', '8cb73308-b77d-428b-9cf2-6ff3c1f33709'),
        ('44bce2da-50d1-418d-b220-6086f5c38def', 'compday.ru', NULL, 'compday.ru_logo.png', 'Is confirmed contributor (Compday)', '+1234567890', NULL, NULL, 'aa3d6fc2-6129-4101-90ed-d49c0de13a67', '8cb73308-b77d-428b-9cf2-6ff3c1f33709'),
        ('37cca2ef-5327-4238-8ffd-5d517b31ab96', 'xcom-shop.ru', NULL, 'xcom-shop.ru_logo.png', 'Is confirmed contributor (XCOM-shop)', '+1234567890', NULL, NULL, 'aa3d6fc2-6129-4101-90ed-d49c0de13a67', '8cb73308-b77d-428b-9cf2-6ff3c1f33709'),
        ('0673a4bc-8bff-4de8-bc5a-069e20267920', 'pc-arena.ru', NULL, 'pc-arena.ru_logo.png', 'Is confirmed contributor (PC-arena)', '+1234567890', NULL, NULL, 'aa3d6fc2-6129-4101-90ed-d49c0de13a67', '8cb73308-b77d-428b-9cf2-6ff3c1f33709'),
    
        ('0711c4c7-1387-4df7-9055-6b6294f94be3', 'allegro.pl', NULL, 'allegro.pl_logo.png', 'Is confirmed contributor (Allegro)', '+1234567890', NULL, NULL, 'e1d77644-f691-4d44-a902-9065e60e82d1', '0bb32ff2-4821-4828-9a6d-418c64a4989f'),
        ('2f6c0bf7-8b3f-4509-ac71-1b0f6bd1e10e', 'avans.pl', NULL, 'avans.pl_logo.png', 'Is confirmed contributor (Avans)', '+1234567890', NULL, NULL, 'e1d77644-f691-4d44-a902-9065e60e82d1', '0bb32ff2-4821-4828-9a6d-418c64a4989f'),
        ('4576e636-fa93-4fcb-935c-c1dc10cc7dad', 'MediaMarkt.pl', NULL, 'MediaMarkt.pl_logo.png', 'Is confirmed contributor (MediaMarkt)', '+1234567890', NULL, NULL, 'e1d77644-f691-4d44-a902-9065e60e82d1', '0bb32ff2-4821-4828-9a6d-418c64a4989f');
END;


IF NOT EXISTS (SELECT 1 FROM Contributors)
BEGIN
    INSERT INTO Contributors (Id, UserId, ContributorConfirmInfoId, ContributorExcellenceId, SubscriptionPlanInfoId)
    VALUES
        ('b863dd75-613b-4811-8977-19d79677c48f', 'a9caa7b2-109b-4c21-bc24-749ff87b9b18', 'e4b1bab0-8958-4eaf-b371-4fba600e7938', '611cd651-28bf-4039-8c38-a9d40be8864d', NULL),
        ('d962a3b8-34e4-40d0-a761-2f12668725ea', '274f801d-2117-48ff-96f7-ecb9b193bc7f', 'e635e170-871f-4ab1-a80c-b9f275a3ec6e', 'ae56b294-7daa-466a-b1a6-56c80deb4504', NULL),
        ('e31b2459-c089-4244-884c-f633fa7b66d8', '46f5064b-a6fe-4b58-b303-9ed344700195', '1d6d658d-fa5d-4557-ad59-d3265f237837', '67fd959e-c9cf-4159-8a90-658be0f8884a', NULL),
        ('72f01826-2460-491f-9f80-f581c38971cc', '7c5086ea-4faf-4db2-91a4-c1217a2f3029', 'ee8325d7-e7d7-47a5-ba8b-4638ec3ccfe8', 'd628f359-e0fa-42a1-b9a6-989fd8be270a', NULL),
        ('3526182b-65c5-4cbd-bac3-801ecd323726', 'b0a3bda2-525d-42c0-b7ff-8b0f68b4ca84', '70e657a3-754c-4aa4-982a-a536a7614882', '5c8c1ce1-eaf2-40b3-9f35-ba5431eb0c32', NULL),

        ('055a00a2-fca3-4bd0-8aa0-a57e02ac55f3', '17f87d98-17f0-4708-a7ff-0cb4ec09b58a', '8766ff10-bea3-43d2-8bf3-b5b389f2f261', 'ea5d7cfa-f3e6-4898-86bf-0eec7c2113c1', NULL),
        ('db3a936d-8681-4453-806e-519a05d6a6cc', '8bc0e747-443a-4f62-a05a-6e7d8cb1516f', '518e3fc8-3bde-4d30-ab1c-ea0a81a220fc', '44bce2da-50d1-418d-b220-6086f5c38def', NULL),
        ('bfabfc78-3689-4ad2-8e46-b0814b340507', '02e91bcd-c2f5-4025-8f2f-5bac70b6924c', '9a468d7e-0502-4e7c-99ab-01c694ccb841', '37cca2ef-5327-4238-8ffd-5d517b31ab96', NULL),
        ('e25ac60c-ad9d-45ac-97f0-431227564b01', 'ffcd6b86-9327-4b7a-b2ad-ec13cf531d3f', 'fde806f8-8ac2-47b0-8e90-3c8e046a5ba6', '0673a4bc-8bff-4de8-bc5a-069e20267920', NULL),

        ('bbea4186-9e3a-44de-a626-fb7f88f70d27', '67bfe5a9-28e2-4c55-8549-888556d2a670', '753482da-d39e-4bc3-a3ba-590c60acdc76', '0711c4c7-1387-4df7-9055-6b6294f94be3', NULL),
        ('e87fb692-d01b-4e5a-96b4-423d51860897', '34302079-5037-499e-8703-9920be62adf7', '9d2c9e81-62a8-4224-82d4-c881f15db7ee', '2f6c0bf7-8b3f-4509-ac71-1b0f6bd1e10e', NULL),
        ('7e23282c-0caa-4548-83da-9c0bf1f554ec', '0a3a8a9f-9bb3-4e06-a050-20c953855795', '38aa959c-09c1-458a-91e9-7c7d5e670836', '4576e636-fa93-4fcb-935c-c1dc10cc7dad', NULL);
END;