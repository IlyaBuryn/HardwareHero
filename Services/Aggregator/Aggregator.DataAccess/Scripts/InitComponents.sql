
INSERT INTO ComponentTypes (Id, Name, FullName, Description)
VALUES
    ('2afe282c-f2a4-46f8-a792-02efaf9fc1e8', 'CPU', 'Central Processing Unit', 'The central processing unit of a computer.'),
    ('5ba0f390-9cb5-482f-bf21-018e31abfcb8', 'GPU', 'Graphics Processing Unit', 'A specialized electronic circuit designed to accelerate graphics processing.'),
    ('985d1b53-a6e4-4020-93c5-b6db97cd5858', 'MB', 'Motherboard', 'The main printed circuit board that holds and connects the major components of a computer.'),
    ('cfd76409-4371-45e4-b019-5e5eb9b87082', 'Case', 'Computer Case', 'An enclosure used to house and protect computer components.'),
    ('91cc8d1f-7515-4e0f-83e5-a467ddb2fa55', 'Cooler', 'Cooling System', 'A device used to dissipate heat from computer components.'),
    ('3f201c7a-d061-4035-86bd-675eee30a1e7', 'RAM', 'Random Access Memory', 'A type of computer memory that is used to store data that is being used actively.'),
    ('6296915e-6348-48c5-8370-d0aaa32aea27', 'PSU', 'Power Supply Unit', 'A device that supplies electrical energy to an electrical load.'),
    ('cab506d7-b95c-4ae6-ab0c-777c4dffad7a', 'SD', 'Solid-State Drive', 'A storage device that uses NAND-based flash memory to store data.');

INSERT INTO Components (Id, Name, Description, ComponentTypeId)
VALUES
    ('18ea6e49-ffd4-479d-be13-eb83c9ef10dd', 'AMD Ryzen 9 7950X3D', 'AMD Ryzen 9 7950X3D - Ryzen 9 7000 Series 16-Core 4.2 GHz Socket AM5 120W AMD Radeon Graphics Desktop Processor - 100-100000908WOF', '2afe282c-f2a4-46f8-a792-02efaf9fc1e8'),
    ('c23d9f9a-85be-4ab6-9d7b-2d6e806e0290', 'Intel Core i7-12700KF', 'Intel Core i7-12700KF - Core i7 12th Gen Alder Lake 12-Core (8P+4E) 3.6 GHz LGA 1700 125W Desktop Processor - BX8071512700KF', '2afe282c-f2a4-46f8-a792-02efaf9fc1e8'),
    ('207fd2c4-861b-49cd-992d-85d2477a5d73', 'AMD Ryzen 7 7800X3D', 'AMD Ryzen 7 7800X3D - Ryzen 7 7000 Series 8-Core Socket AM5 120W AMD Radeon Graphics Desktop Processor - 100-100000910WOF', '2afe282c-f2a4-46f8-a792-02efaf9fc1e8'),
    ('60092e5d-08e3-4c3a-b27f-537dc0726a3a', 'AMD Ryzen 7 5800X', 'AMD Ryzen 7 5800X - Ryzen 7 5000 Series Vermeer (Zen 3) 8-Core 3.8 GHz Socket AM4 105W None Integrated Graphics Desktop Processor - 100-100000063WOF', '2afe282c-f2a4-46f8-a792-02efaf9fc1e8'),
    ('53777784-e82c-4a42-998a-9c54634943bd', 'Intel Core i5-12600K', 'Intel Core i5-12600K - Core i5 12th Gen Alder Lake 10-Core (6P+4E) 3.7 GHz LGA 1700 125W Intel UHD Graphics 770 Desktop Processor - BX8071512600K', '2afe282c-f2a4-46f8-a792-02efaf9fc1e8'),
    ('dfb7c67f-f325-41b7-9a21-f68a12cb2d03', 'AMD Ryzen 9 5950X', 'AMD Ryzen 9 5950X - Ryzen 9 5000 Series Vermeer (Zen 3) 16-Core 3.4 GHz Socket AM4 105W None Integrated Graphics Desktop Processor - 100-100000059WOF', '2afe282c-f2a4-46f8-a792-02efaf9fc1e8'),
    ('6bc9e0fb-16a4-4745-99e4-4c8dda32d635', 'Intel Core i7-12700K', 'Intel Core i7-12700K - Core i7 12th Gen Alder Lake 12-Core (8P+4E) 3.6 GHz LGA 1700 125W Intel UHD Graphics 770 Desktop Processor - BX8071512700K', '2afe282c-f2a4-46f8-a792-02efaf9fc1e8'),
    ('b1c3e4cd-3283-42dd-a6b5-62ac5cbcb595', 'AMD Ryzen 7 5800X3D', 'AMD Ryzen 7 5800X3D - Ryzen 7 5000 Series 8-Core 3.4 GHz Socket AM4 105W None Integrated Graphics Desktop Processor - 100-100000651WOF', '2afe282c-f2a4-46f8-a792-02efaf9fc1e8'),
    ('3dcd56dc-bdc4-4090-9841-e7b1c283957c', 'Intel Core i9-12900K', 'Intel Core i9-12900K - Core i9 12th Gen Alder Lake 16-Core (8P+8E) 3.2 GHz LGA 1700 125W Intel UHD Graphics 770 Desktop Processor - BX8071512900K', '2afe282c-f2a4-46f8-a792-02efaf9fc1e8'),

    ('d5947087-a778-4291-af5f-272f0e33a504', 'ASUS TUF Gaming NVIDIA GeForce RTX 3070 Ti OC', 'ASUS TUF Gaming NVIDIA GeForce RTX 3070 Ti OC V2 Graphics Card (PCIe 4.0, 8GB GDDR6X, HDMI 2.1, DisplayPort 1.4a, Military-grade Certification, GPU Tweak III) TUF-RTX3070TI-O8G-V2-GAMING', '5ba0f390-9cb5-482f-bf21-018e31abfcb8'),
    ('6df9893e-db9a-4708-ac17-7f58a108e3b2', 'ASUS Dual GeForce RTX 4070 12GB', 'ASUS Dual GeForce RTX 4070 12GB GDDR6X, IP5X, Auto-Extreme Technology, 144-Hour Validation Program, HDMI 2.1a, DP 1.4a, DUAL-RTX4070-12G', '5ba0f390-9cb5-482f-bf21-018e31abfcb8'),
    ('d65de006-8250-4f86-9daf-566d19c2f379', '51Risc AMD Radeon RX 580', '51Risc AMD Radeon RX 580 DirectX 12 RX580 8G 256-Bit GDDR5 PCI Express 3.0 x16 HDCP Gaming Video Card Mining GPU', '5ba0f390-9cb5-482f-bf21-018e31abfcb8'),
    ('b3efd4c1-2507-4a24-8b6d-20aea4848d73', 'GIGABYTE WINDFORCE GeForce RTX 4070 12GB GDDR6X', 'GIGABYTE WINDFORCE GeForce RTX 4070 12GB GDDR6X PCI Express 4.0 x16 ATX Video Card GV-N4070WF3OC-12GD', '5ba0f390-9cb5-482f-bf21-018e31abfcb8'),
    ('7dcfc16c-678a-4ce8-aafd-5053ddb4ffb4', 'GeForce GTX 1660 SUPER 6GB GDDR6', 'GeForce GTX 1660 SUPER 6GB GDDR6 PCI Express 3.0 x16 Video Card GTX 1660 SUPER', '5ba0f390-9cb5-482f-bf21-018e31abfcb8'),
    ('0d43b538-65eb-4bcf-9883-85ea289acd4d', '51RISC Radeon RX5600XT 6G', '51RISC Radeon RX5600XT 6G Graphic Card GDDR6 6GB 128Bit Gaming Computer GPU RX5600XT 8GD6 GA Desktop Video Card 2 Fans', '5ba0f390-9cb5-482f-bf21-018e31abfcb8'),
    ('506e0ca2-140b-49ae-b6b8-e03a75227766', 'ASRock Radeon RX 6600 8GB', 'ASRock Radeon RX 6600 8GB GDDR6 PCI Express 4.0 Video Card RX6600 CLD 8G', '5ba0f390-9cb5-482f-bf21-018e31abfcb8'),

    ('c673342f-622d-4541-9525-4c9ad30943a8', 'GIGABYTE B760M AORUS ELITE AX', 'GIGABYTE B760M AORUS ELITE AX LGA 1700 Intel B760 M-ATX Motherboard with DDR5, Dual M.2, PCIe 4.0, USB 3.2 Gen2X2 Type-C, Intel Wi-Fi 6E, 2.5GbE LAN, Q-Flash Plus, PCIe EZ-Latch', '985d1b53-a6e4-4020-93c5-b6db97cd5858'),
    ('a9d592c4-9ea3-42d5-8051-d889f15bc3c8', 'ASUS ROG Maximus Z690 Hero(WiFi 6E)', 'ASUS ROG Maximus Z690 Hero(WiFi 6E) LGA 1700(Intel 12th&13th Gen) ATX gaming motherboard(PCIe 5.0,DDR5, 20+1 90A power stages,2.5Gb LAN,Bluetooth V5.2,2x Thunderbolt 4 ports,5xM.2/NVMe SSD,Front panel USB 3.2 Gen 2x2 Type-C connector)', '985d1b53-a6e4-4020-93c5-b6db97cd5858'),
    ('10a0c02a-effe-4bf8-b161-89ecbecc32ec', 'GIGABYTE B550M DS3H AC', 'GIGABYTE B550M DS3H AC AM4 AMD B550 SATA 6Gb/s Micro ATX AMD Motherboard', '985d1b53-a6e4-4020-93c5-b6db97cd5858'),
    ('29476666-5df4-47b6-abd0-bd0774f0c462', 'ASUS TUF Gaming Z790-Plus', 'ASUS TUF Gaming Z790-Plus WiFi LGA 1700(Intel 12th&13th Gen) ATX gaming motherboard(PCIe 5.0, DDR5,4xM.2 Slots,16+1 DrMOS,WiFi 6E,Intel  2.5Gb LAN,front USB 3.2 Gen 2 Type-C,Thunderbolt 4(USB4), ,Aura RGB lighting)', '985d1b53-a6e4-4020-93c5-b6db97cd5858'),
    ('8f69977a-27b4-4188-a478-b3bff4150150', 'ASRock B650M-HDV/M.2', 'ASRock B650M-HDV/M.2 Socket AM5 Ryzen 7000 Micro ATX Motherboard', '985d1b53-a6e4-4020-93c5-b6db97cd5858'),
    ('54ebb54f-1aaf-424d-bff4-c444ceb1c959', 'X99E8I', 'X99E8I Motherboard Support XEON E5 LGA2011-3 DDR4 ECC REG Memory NVME M.2 WIFI USB3.0 ATX Server Support Turbo boost', '985d1b53-a6e4-4020-93c5-b6db97cd5858'),
    ('15a19f9b-9f68-443d-a62e-12f46635cddd', 'B75M', 'B75M Desktop Motherboard B75 LGA 1155 for i3 i5 i7 CPU Support DDR33 Memory 3*USB 3.0 SATA 3.0 Up to 16GB', '985d1b53-a6e4-4020-93c5-b6db97cd5858'),
    ('c9cc9dd5-35e7-4dd5-a483-c66fd01a2e41', 'ASUS ROG Strix Z690-F Gaming', 'ASUS ROG Strix Z690-F Gaming WiFi 6E LGA 1700(Intel 12th&13th Gen) ATX gaming motherboard(PCIe 5.0, DDR5,16+1 power stages,2.5 Gb LAN,Bluetooth v5.2,Thunderbolt 4,4xM.2/NVMe SSD and Front panel USB 3.2 Gen 2x2 Type-C connector)', '985d1b53-a6e4-4020-93c5-b6db97cd5858'),

    ('9f17447a-8bb5-4496-be07-cb9a27479533', 'Corsair 4000D Airflow', 'Corsair 4000D Airflow CC-9011200-WW Black Steel / Plastic / Tempered Glass ATX Mid Tower Computer Case', 'cfd76409-4371-45e4-b019-5e5eb9b87082'),
    ('fb9c6b4e-9b97-4c93-897d-f159735935d6', 'Fractal Design North', 'Fractal Design North ATX mATX Mid Tower PC Case - Charcoal Black Chassis with Walnut Front and Mesh Side Panel', 'cfd76409-4371-45e4-b019-5e5eb9b87082'),
    ('89fb1e2e-7540-4fca-9ea5-94c38195010c', 'Corsair 4000D Airflow CC-9011201', 'Corsair 4000D Airflow CC-9011201-WW White Steel / Plastic / Tempered Glass ATX Mid Tower Computer Case', 'cfd76409-4371-45e4-b019-5e5eb9b87082'),
    ('b5b42281-6b59-4ee1-9590-f94953a6dcbd', 'Phanteks Eclipse G360A', 'Phanteks Eclipse G360A PH-EC360ATG_DBK02 Black Steel / Tempered Glass ATX Mid Tower Computer Case', 'cfd76409-4371-45e4-b019-5e5eb9b87082'),
    ('1a4c7538-a7eb-4507-aa55-16857aef18d6', 'HYTE Y40 Mainstream', 'HYTE Y40 Mainstream Vertical GPU Case ATX Mid Tower Gaming Case with PCI Express 4.0 x 16 Riser Cable Included, Black', 'cfd76409-4371-45e4-b019-5e5eb9b87082'),
    ('84c34d24-b8e5-4048-9eaf-b494c584c744', 'CORSAIR 7000D AIRFLOW', 'CORSAIR 7000D AIRFLOW Full-Tower ATX PC Case', 'cfd76409-4371-45e4-b019-5e5eb9b87082'),

    ('58556804-5b1e-49da-8c80-e29de6fc273b', 'NZXT Kraken', 'NZXT Kraken 360mm - RL-KN360-B1 AIO CPU Liquid Cooler - LCD Display - 3 x F120P Static Pressure Fan Radiator Fans LGA 1700 / AM5 Compatible', '91cc8d1f-7515-4e0f-83e5-a467ddb2fa55'),
    ('27497fd7-531e-473f-9ead-222afd0a9b4a', 'DeepCool AK620', 'DeepCool AK620 High-Performance CPU Cooler, Dual-Tower Design, 2x 120mm Fluid Dynamic Bearing Fans, 6 Copper Heat Pipes, 260W Heat Dissipation, Black.', '91cc8d1f-7515-4e0f-83e5-a467ddb2fa55'),
    ('c4a08c40-ab2c-4346-8190-09ba0bb53ac2', 'Noctua NH-U12S', 'Noctua NH-U12S, Premium CPU Cooler with NF-F12 120mm Fan (Brown)', '91cc8d1f-7515-4e0f-83e5-a467ddb2fa55'),
    ('ec48d769-bb82-44f5-9d5b-fa85672df375', 'Noctua NH-L9i', 'Noctua NH-L9i, Premium Low-Profile CPU Cooler for Intel LGA1200 & LGA115x (Brown)', '91cc8d1f-7515-4e0f-83e5-a467ddb2fa55'),
    ('a0b85774-7131-4612-a0c4-6d0ec29484ee', 'Cryo-PC CPC-07500', 'Cryo-PC CPC-07500, Low-Profile Intel Stock CPU Cooler with 4pin PWM Fan, Tool-free Quick Snap Installation, LGA 775 1155 1156 1366 2011 1200', '91cc8d1f-7515-4e0f-83e5-a467ddb2fa55'),
    
    ('35055e95-fda4-44e5-9181-5be27d1e1588', 'OLOy Blade RGB (OLOY) 64GB (2 x 32GB) DDR5 6400', 'OLOy Blade RGB (OLOY) 64GB (2 x 32GB) DDR5 6400 (PC5 51200) Desktop Memory Model ND5U3264320IRKDE', '3f201c7a-d061-4035-86bd-675eee30a1e7'),
    ('4cf69ad1-099c-43e8-a5ce-971dd1757f65', 'Crucial Pro 64GB (2 x 32GB) DDR4 3200', 'Crucial Pro 64GB (2 x 32GB) 288-Pin PC RAM DDR4 3200 (PC4 25600) Desktop Memory Model CP2K32G4DFRA32A', '3f201c7a-d061-4035-86bd-675eee30a1e7'),
    ('b3290472-e196-48ee-bd09-5b506510c2b1', 'G.SKILL Trident Z5 RGB Series 32GB (2 x 16GB) DDR5 6000', 'G.SKILL Trident Z5 RGB Series 32GB (2 x 16GB) 288-Pin PC RAM DDR5 6000 (PC5 48000) Desktop Memory Model F5-6000J3636F16GX2-TZ5RK', '3f201c7a-d061-4035-86bd-675eee30a1e7'),
    ('3f024cb7-73aa-42e3-bbe0-8b798962227e', 'Crucial 32GB (2 x 16GB) 260-Pin SO-DIMM DDR4 3200', 'Crucial 32GB (2 x 16GB) 260-Pin DDR4 SO-DIMM DDR4 3200 (PC4 25600) Laptop Memory Model CT2K16G4SFRA32A', '3f201c7a-d061-4035-86bd-675eee30a1e7'),
    ('b7972fd1-bfa3-4aea-8c87-43603406af60', 'OLOy Blade RGB 16GB (2 x 8GB) DDR5 5600', 'OLOy Blade RGB 16GB (2 x 8GB) DDR5 5600 (PC5 44800) Desktop Memory Model MD5U0856360BRKDE', '3f201c7a-d061-4035-86bd-675eee30a1e7'),
    ('3cabc72e-1967-4249-bd9e-31f98fd5107b', 'Crucial 64GB (2 x 32GB) 262-Pin SO-DIMM DDR5 4800', 'Crucial 64GB (2 x 32GB) 262-Pin DDR5 SO-DIMM DDR5 4800 (PC5 38400) Laptop Memory Model CT2K32G48C40S5', '3f201c7a-d061-4035-86bd-675eee30a1e7'),
    ('b8b92837-d6d1-40df-b6fe-eb92c645da72', 'CORSAIR Vengeance LPX 64GB (2 x 32GB) DDR4 3200', 'CORSAIR Vengeance LPX 64GB (2 x 32GB) 288-Pin PC RAM DDR4 3200 (PC4 25600) Desktop Memory Model CMK64GX4M2E3200C16', '3f201c7a-d061-4035-86bd-675eee30a1e7'),
    ('fb269342-7866-4e3a-90b2-3e57a24f016c', 'G.SKILL Ripjaws S5 Series 32GB (2 x 16GB) DDR5 5600', 'G.SKILL Ripjaws S5 Series 32GB (2 x 16GB) DDR5 5600 Desktop Memory Model F5-5600J3636C16GX2-RS5W', '3f201c7a-d061-4035-86bd-675eee30a1e7'),

    ('8ecaaeed-f488-445c-ac22-bab1e158198e', 'CORSAIR RMx Series', 'CORSAIR RMx Series (2021) RM750x CP-9020199-NA 750 W ATX12V / EPS12V 80 PLUS GOLD Certified Full Modular Power Supply', '6296915e-6348-48c5-8370-d0aaa32aea27'),
    ('fd5006c6-9ad3-495f-85f0-2a87696947f2', 'EVGA SuperNOVA 1600', 'EVGA SuperNOVA 1600 T2 220-T2-1600-X1 80+ TITANIUM 1600W Fully Modular EVGA ECO Mode Includes FREE Power On Self Tester Power Supply', '6296915e-6348-48c5-8370-d0aaa32aea27'),
    ('69b91dcc-09c3-41af-8bc5-cbdf4083766e', 'EVGA SuperNOVA 850 P5', 'EVGA SuperNOVA 850 P5, 80 Plus Platinum 850W, Fully Modular, Eco Mode with FDB Fan, 10 Year Warranty, Includes Power ON Self Tester, Compact 150mm Size, Power Supply 220-P5-0850-X1', '6296915e-6348-48c5-8370-d0aaa32aea27'),
    ('7160fec0-2f00-4469-afe5-5816ed082e83', 'Super Flower Leadex V Gold PRO', 'Super Flower Leadex V Gold PRO 1000W ATX 80 PLUS GOLD Certified Power Supply, Smallest 130mm 1000W ATX PSU, Patent Super Connectors, Full Modular, Ultra-Flexible Flat Ribbon Cab, SF-1000F14TG', '6296915e-6348-48c5-8370-d0aaa32aea27'),
    ('ab06f0a9-d8a8-4320-9a6e-11858ddbdd34', 'MSI - MPG A850G PCIE 5.0', 'MSI - MPG A850G PCIE 5.0, 80 GOLD Full Modular Gaming PSU, 12VHPWR Cable, 4080 4070 ATX 3.0 Compatible, 850W Power Supply', '6296915e-6348-48c5-8370-d0aaa32aea27'),
    ('cad24e2f-ceb3-4a0a-8863-bba1f7850ef3', 'CORSAIR RM850e', 'CORSAIR RM850e Fully Modular Low-Noise ATX Power Supply - ATX 3.0 & PCIe 5.0 Compliant - 105 C-Rated Capacitors - 80 PLUS Gold  Efficiency - Modern Standby Support', '6296915e-6348-48c5-8370-d0aaa32aea27'),
    ('25418d43-ee71-44db-868a-77cec0d873ee', 'CORSAIR RM1000e', 'CORSAIR RM1000e Fully Modular Low-Noise ATX Power Supply - ATX 3.0 & PCIe 5.0 Compliant - 105 C-Rated Capacitors - 80 PLUS Gold  Efficiency - Modern Standby Support', '6296915e-6348-48c5-8370-d0aaa32aea27'),
    ('785f049f-75ea-4713-8755-44d07890d1ce', 'ASUS ROG Thor', 'ASUS ROG Thor ROG-THOR-1000P2-GAMING 1000 W ATX12V 80 PLUS PLATINUM Certified Power Supply', '6296915e-6348-48c5-8370-d0aaa32aea27'),

    ('c51643d1-3f46-4bed-bb8d-42e8c8ce521d', 'Seagate Exos X20', 'Seagate Exos X20 ST20000NM007D 20TB 7200 RPM 256MB Cache SATA 6.0Gb/s 3.5" Internal Hard Drive', 'cab506d7-b95c-4ae6-ab0c-777c4dffad7a'),
    ('978addb6-cbc4-4d76-8c14-d2279dcdcd0f', 'Seagate BarraCuda ST8000DM004-NE', 'Seagate BarraCuda ST8000DM004-NE 8TB 5400 RPM 256MB Cache SATA 6.0Gb/s 3.5" Internal Hard Drive Bare Drive', 'cab506d7-b95c-4ae6-ab0c-777c4dffad7a'),
    ('c57489d2-3f72-411e-889c-62cc364174b1', 'WD Red Plus 4TB NAS', 'WD Red Plus 4TB NAS Hard Disk Drive - 5400 RPM Class SATA 6Gb/s, CMR, 256MB Cache, 3.5 Inch - WD40EFPX', 'cab506d7-b95c-4ae6-ab0c-777c4dffad7a'),
    ('75a8b8b0-04e8-4d4f-9462-79ccb13a03fb', 'Team Group MS30 M.2 2280', 'Team Group MS30 M.2 2280 2TB SATA III TLC Internal Solid State Drive (SSD) TM8PS7002T0C101', 'cab506d7-b95c-4ae6-ab0c-777c4dffad7a'),
    ('3b4d9d95-b96a-4fa0-83b1-2d4432eec939', 'AITC KINGSMAN SK150', 'AITC KINGSMAN SK150 2.5" 1TB SATA III 3D NAND Internal Solid State Drive (SSD)', 'cab506d7-b95c-4ae6-ab0c-777c4dffad7a'),
    ('6e889ab7-bf7e-4058-ac4a-8c64a242229f', 'Crucial MX500', 'Crucial MX500 2TB 3D NAND SATA 2.5 Inch Internal SSD, up to 560 MB/s  - CT2000MX500SSD1', 'cab506d7-b95c-4ae6-ab0c-777c4dffad7a'),
    ('4d120703-a73d-4941-856e-24b69f999d4b', 'Silicon Power', 'Silicon Power 2TB UD90 NVMe 4.0 Gen4 PCIe M.2 SSD R/W up to 5,000/4,800 MB/s', 'cab506d7-b95c-4ae6-ab0c-777c4dffad7a');

INSERT INTO ComponentImages (Id, ComponentId, Image)
VALUES
    (NEWID(), '18ea6e49-ffd4-479d-be13-eb83c9ef10dd', 'https://drive.google.com/uc?export=view&id=1sTrdqi4fQQwmqSxrFmlOiKtppIPdcF_9'),
    (NEWID(), '18ea6e49-ffd4-479d-be13-eb83c9ef10dd', 'https://drive.google.com/uc?export=view&id=1o7L5oODX237fQJJQYzxeHQ4NrU2c81g6'),
    (NEWID(), '18ea6e49-ffd4-479d-be13-eb83c9ef10dd', 'https://drive.google.com/uc?export=view&id=1lN3HKMQikdO5tSQlqZ6rFOJ7KmKJ-a_1'),

    (NEWID(), 'c23d9f9a-85be-4ab6-9d7b-2d6e806e0290', 'https://drive.google.com/uc?export=view&id=1NkfSeUKx9dnSFTqa7bvaLMsL0ojeE9fY'),
    (NEWID(), 'c23d9f9a-85be-4ab6-9d7b-2d6e806e0290', 'https://drive.google.com/uc?export=view&id=1ail6Og0SQpuTKnNkMlFHctzK2uv6Rehv'),
    (NEWID(), 'c23d9f9a-85be-4ab6-9d7b-2d6e806e0290', 'https://drive.google.com/uc?export=view&id=1cF1tDv0shNkMljTdZRVGX3RrzlEcBVYd'),

    (NEWID(), '207fd2c4-861b-49cd-992d-85d2477a5d73', 'https://drive.google.com/uc?export=view&id=1TM0ibzgp6PNXjZ9CapDA3gm-lI2dNvTg'),
    (NEWID(), '207fd2c4-861b-49cd-992d-85d2477a5d73', 'https://drive.google.com/uc?export=view&id=1AGmEcihOsszWHreXr6NpDUaW9snJIU4s'),
    (NEWID(), '207fd2c4-861b-49cd-992d-85d2477a5d73', 'https://drive.google.com/uc?export=view&id=1HkxxUIu5ft4O8EMa-ZjOz5gySWSmIwmZ'),

    (NEWID(), '60092e5d-08e3-4c3a-b27f-537dc0726a3a', 'https://drive.google.com/uc?export=view&id=10VlVZx76_h3MeLk2VUBypFfqxlq-t7_i'),
    (NEWID(), '60092e5d-08e3-4c3a-b27f-537dc0726a3a', 'https://drive.google.com/uc?export=view&id=1zgTLQ3V33ZP8ry4qPaDJ-12BK6Vw1G2v'),
    (NEWID(), '60092e5d-08e3-4c3a-b27f-537dc0726a3a', 'https://drive.google.com/uc?export=view&id=143CzRoTW-4kbNUxQD81g4RuA07AHUmkk'),

    (NEWID(), '53777784-e82c-4a42-998a-9c54634943bd', 'https://drive.google.com/uc?export=view&id=1KBIs4CLKwpxPFVT-tyCouO_qrVBfLSSo'),
    (NEWID(), '53777784-e82c-4a42-998a-9c54634943bd', 'https://drive.google.com/uc?export=view&id=1op7WCrubVEYVN2PZ3Fr5Q9xHzYe0pEXG'),
    (NEWID(), '53777784-e82c-4a42-998a-9c54634943bd', 'https://drive.google.com/uc?export=view&id=1g7OhmpDudUyIiHOBa3V0rFl5K5boVMr6'),

    (NEWID(), 'dfb7c67f-f325-41b7-9a21-f68a12cb2d03', 'https://drive.google.com/uc?export=view&id=1QmOTtoZBCMVLwk1dj6-0RP8rUfKfxaKd'),
    (NEWID(), 'dfb7c67f-f325-41b7-9a21-f68a12cb2d03', 'https://drive.google.com/uc?export=view&id=1jtsAjPlZD6zFXgvPMYbSzolX1SIRIC8A'),
    (NEWID(), 'dfb7c67f-f325-41b7-9a21-f68a12cb2d03', 'https://drive.google.com/uc?export=view&id=1wO0fb7IeiFywkhHpTFtfEtow0R5BQaRE'),

    (NEWID(), '6bc9e0fb-16a4-4745-99e4-4c8dda32d635', 'https://drive.google.com/uc?export=view&id=1JKzGNqJP01xV5Z-SXnd8z-nff1PfzZZx'),
    (NEWID(), '6bc9e0fb-16a4-4745-99e4-4c8dda32d635', 'https://drive.google.com/uc?export=view&id=1GHsnbpeTJiUyeipZCHY96cKFIYA441f4'),
    (NEWID(), '6bc9e0fb-16a4-4745-99e4-4c8dda32d635', 'https://drive.google.com/uc?export=view&id=1P6aInGunfKRebdZila33UwoFdOZ_4Aim'),

    (NEWID(), 'b1c3e4cd-3283-42dd-a6b5-62ac5cbcb595', 'https://drive.google.com/uc?export=view&id=14oS_uCy7qO7LMomyxQIinyBu_dgNjqEx'),
    (NEWID(), 'b1c3e4cd-3283-42dd-a6b5-62ac5cbcb595', 'https://drive.google.com/uc?export=view&id=1LW5MUk8Qi0R41atcxSYWr3hkKi2m1ldj'),
    (NEWID(), 'b1c3e4cd-3283-42dd-a6b5-62ac5cbcb595', 'https://drive.google.com/uc?export=view&id=1i4PrL1d7wccqr0grf-17V6tQbptWF75t'),

    (NEWID(), '3dcd56dc-bdc4-4090-9841-e7b1c283957c', 'https://drive.google.com/uc?export=view&id=1_3QcjuPc4D8I5MApZecHbOsWcnDZnoVm'),
    (NEWID(), '3dcd56dc-bdc4-4090-9841-e7b1c283957c', 'https://drive.google.com/uc?export=view&id=12q90ZoqBAqWfgmme-cWigmw3ZfpTeEMr'),
    (NEWID(), '3dcd56dc-bdc4-4090-9841-e7b1c283957c', 'https://drive.google.com/uc?export=view&id=1pLHZlT5socfBjGw-qhBDpL2ZPwtTupYz'),

    (NEWID(), 'd5947087-a778-4291-af5f-272f0e33a504', 'https://drive.google.com/uc?export=view&id=1gaUzsi-IvPl9P1d2RDbWuIb5g0wYz6Av'),
    (NEWID(), 'd5947087-a778-4291-af5f-272f0e33a504', 'https://drive.google.com/uc?export=view&id=1wNAxBq5HGZBQeU4-upmvUWIJ_lmniX7r'),
    (NEWID(), 'd5947087-a778-4291-af5f-272f0e33a504', 'https://drive.google.com/uc?export=view&id=1lJV3-4_Me57zgPNISdlWck-rTb3zJirQ'),

    (NEWID(), '6df9893e-db9a-4708-ac17-7f58a108e3b2', 'https://drive.google.com/uc?export=view&id=1i73-PxHEQx1ATQi4EJhHxg4bz3PasoOA'),
    (NEWID(), '6df9893e-db9a-4708-ac17-7f58a108e3b2', 'https://drive.google.com/uc?export=view&id=143pD23FC9e797C4qGOUuU9_ICmzQzDPy'),
    (NEWID(), '6df9893e-db9a-4708-ac17-7f58a108e3b2', 'https://drive.google.com/uc?export=view&id=1ULaZmj0qLJ4f0TyxFU9eWfg7Z_dC5ceg'),

    (NEWID(), 'd65de006-8250-4f86-9daf-566d19c2f379', 'https://drive.google.com/uc?export=view&id=15Le9K_b80VtIQPGVaYgR0bhF8AL_2X71'),
    (NEWID(), 'd65de006-8250-4f86-9daf-566d19c2f379', 'https://drive.google.com/uc?export=view&id=1wIvdZqM0B4mArT2L5hiFKBukvwpcrWgN'),
    (NEWID(), 'd65de006-8250-4f86-9daf-566d19c2f379', 'https://drive.google.com/uc?export=view&id=1J7V9JlU-5bgQzVQdhq6wi-Id4q2MDG97'),

    (NEWID(), 'b3efd4c1-2507-4a24-8b6d-20aea4848d73', 'https://drive.google.com/uc?export=view&id=1tf6B4_9Fw8VIQAdvtswr-Yio3l_KdssQ'),
    (NEWID(), 'b3efd4c1-2507-4a24-8b6d-20aea4848d73', 'https://drive.google.com/uc?export=view&id=1o2THcEVA0X367hdgyX5uXHKUJwSTBiq-'),
    (NEWID(), 'b3efd4c1-2507-4a24-8b6d-20aea4848d73', 'https://drive.google.com/uc?export=view&id=1827PKog1WW1jiKNqOUWDZjMTogMhja67'),

    (NEWID(), '7dcfc16c-678a-4ce8-aafd-5053ddb4ffb4', 'https://drive.google.com/uc?export=view&id=1DiGq7vC0lOKPGbdvcD6GHeDGWK_ncR8E'),
    (NEWID(), '7dcfc16c-678a-4ce8-aafd-5053ddb4ffb4', 'https://drive.google.com/uc?export=view&id=1Sq7aI-gJBYdyi5Lyk5n3M6YLb_euf46e'),
    (NEWID(), '7dcfc16c-678a-4ce8-aafd-5053ddb4ffb4', 'https://drive.google.com/uc?export=view&id=1MRZz_jY9t0ny-0qXQOSh4QgkadpTaF5m'),

    (NEWID(), '0d43b538-65eb-4bcf-9883-85ea289acd4d', 'https://drive.google.com/uc?export=view&id=1ysnf_uhSyPYFWvRSNHnFIksgn_3-sFjR'),
    (NEWID(), '0d43b538-65eb-4bcf-9883-85ea289acd4d', 'https://drive.google.com/uc?export=view&id=1yxJ-0UdBUBDq2QU11urY8fwTSh_zGOji'),
    (NEWID(), '0d43b538-65eb-4bcf-9883-85ea289acd4d', 'https://drive.google.com/uc?export=view&id=1zWaW1aY7cciCVpxFZJmVf1rAz2Qiy-oo'),

    (NEWID(), '506e0ca2-140b-49ae-b6b8-e03a75227766', 'https://drive.google.com/uc?export=view&id=1VOI7FYZSMHulCk-3RpJuPh_NU4zsBwHa'),
    (NEWID(), '506e0ca2-140b-49ae-b6b8-e03a75227766', 'https://drive.google.com/uc?export=view&id=1iWCazz_g49X5W5nOkAftGYU-gwh9ZaH5'),
    (NEWID(), '506e0ca2-140b-49ae-b6b8-e03a75227766', 'https://drive.google.com/uc?export=view&id=1vtsXePWL8eTgwKDWuXPmRtY0m3sT1Brh'),

    (NEWID(), 'c673342f-622d-4541-9525-4c9ad30943a8', 'https://drive.google.com/uc?export=view&id=1gNpp_JyJJrYPAtbvq7AqH5BlRzmFjCVT'),
    (NEWID(), 'c673342f-622d-4541-9525-4c9ad30943a8', 'https://drive.google.com/uc?export=view&id=1sUTLNRWohCd91dyCZyyrPjFMSA73Ityn'),
    (NEWID(), 'c673342f-622d-4541-9525-4c9ad30943a8', 'https://drive.google.com/uc?export=view&id=1DsrErYkDhjvnbzcl50_3HdlRD1aoTboh'),

    (NEWID(), 'a9d592c4-9ea3-42d5-8051-d889f15bc3c8', 'https://drive.google.com/uc?export=view&id=1lsFfRAKLHUoQFNCnvvZu4cgHfsTspn6K'),
    (NEWID(), 'a9d592c4-9ea3-42d5-8051-d889f15bc3c8', 'https://drive.google.com/uc?export=view&id=1MZz5jfCIBHzUpzY7qFQcNkvy97EvkIsR'),
    (NEWID(), 'a9d592c4-9ea3-42d5-8051-d889f15bc3c8', 'https://drive.google.com/uc?export=view&id=1Zraz6SsQnjzOd3E1ZWIRPJ28yBngNeOU'),

    (NEWID(), '10a0c02a-effe-4bf8-b161-89ecbecc32ec', 'https://drive.google.com/uc?export=view&id=1r9RP75u5uY988-dei4h3YrBOdQQZN5A7'),
    (NEWID(), '10a0c02a-effe-4bf8-b161-89ecbecc32ec', 'https://drive.google.com/uc?export=view&id=1H4PBteHWlCIH53Lc0TQdhaY5cnOUwHyE'),
    (NEWID(), '10a0c02a-effe-4bf8-b161-89ecbecc32ec', 'https://drive.google.com/uc?export=view&id=1rfmF1CmlAflZ2opaw_bCtcwB4lUZsmqm'),

    (NEWID(), '29476666-5df4-47b6-abd0-bd0774f0c462', 'https://drive.google.com/uc?export=view&id=1x1UcITfQbzpJrgHzGjqtIeJIp6kfFBL8'),
    (NEWID(), '29476666-5df4-47b6-abd0-bd0774f0c462', 'https://drive.google.com/uc?export=view&id=1-R6DZuktm0VHuMOWES1H3gBb6MPbm30R'),
    (NEWID(), '29476666-5df4-47b6-abd0-bd0774f0c462', 'https://drive.google.com/uc?export=view&id=1f9DvXkMHuwv889JLp_Jy8sGnz28nWwPP'),

    (NEWID(), '8f69977a-27b4-4188-a478-b3bff4150150', 'https://drive.google.com/uc?export=view&id=1kfn8nfcNNld8HhDCOy4VhaGhbHKL4r6O'),
    (NEWID(), '8f69977a-27b4-4188-a478-b3bff4150150', 'https://drive.google.com/uc?export=view&id=11isaW_GxN8gfbmJLo0a8RoVL5q9DXf5B'),
    (NEWID(), '8f69977a-27b4-4188-a478-b3bff4150150', 'https://drive.google.com/uc?export=view&id=1yMfVBQResthpbsmtykj_QGfFFxv5dVxR'),

    (NEWID(), '54ebb54f-1aaf-424d-bff4-c444ceb1c959', 'https://drive.google.com/uc?export=view&id=1zr4RIONMm0y8iwTdxu81gLUxE7afJWQb'),
    (NEWID(), '54ebb54f-1aaf-424d-bff4-c444ceb1c959', 'https://drive.google.com/uc?export=view&id=1D4yYjTGXIwTe97prrQwVyGvwwmfSMtrE'),
    (NEWID(), '54ebb54f-1aaf-424d-bff4-c444ceb1c959', 'https://drive.google.com/uc?export=view&id=1mtI4Soo0BwWlI4tuVi3A-b2dA4xABaeB'),

    (NEWID(), '15a19f9b-9f68-443d-a62e-12f46635cddd', 'https://drive.google.com/uc?export=view&id=1kW9Mfex5e7bjb6xzQcNA8wv2O1ECFrAj'),
    (NEWID(), '15a19f9b-9f68-443d-a62e-12f46635cddd', 'https://drive.google.com/uc?export=view&id=188rjoWf3-aQ3diRrImIe6UIdsgsXczKH'),
    (NEWID(), '15a19f9b-9f68-443d-a62e-12f46635cddd', 'https://drive.google.com/uc?export=view&id=19EOdxGpNHOYbGKITdCDz3-leAmdqghzu'),

    (NEWID(), 'c9cc9dd5-35e7-4dd5-a483-c66fd01a2e41', 'https://drive.google.com/uc?export=view&id=1Caw7qPh-NtSijDxesje1WTbKTCSlpWpU'),
    (NEWID(), 'c9cc9dd5-35e7-4dd5-a483-c66fd01a2e41', 'https://drive.google.com/uc?export=view&id=1f6ofOipai36rRzw84NPTPQP3SQAkkvHn'),
    (NEWID(), 'c9cc9dd5-35e7-4dd5-a483-c66fd01a2e41', 'https://drive.google.com/uc?export=view&id=1GWW5KtYxU5DwlnW1KnCQ0SHHagn_Lr8m'),

    (NEWID(), '9f17447a-8bb5-4496-be07-cb9a27479533', 'https://drive.google.com/uc?export=view&id=1Bl98nDc1COwaJHg65gMXS_Lur02uBH1a'),
    (NEWID(), '9f17447a-8bb5-4496-be07-cb9a27479533', 'https://drive.google.com/uc?export=view&id=1HmY8Y03-y9SwYQYLHvIOBvaU-YsCyBVa'),
    (NEWID(), '9f17447a-8bb5-4496-be07-cb9a27479533', 'https://drive.google.com/uc?export=view&id=1GHnvWbYJ3dD2U7KrTwpSFCAZTTfII97-'),

    (NEWID(), 'fb9c6b4e-9b97-4c93-897d-f159735935d6', 'https://drive.google.com/uc?export=view&id=1TKujA7QgKFHaZsI2yrV-DVKwv9EwzVKw'),
    (NEWID(), 'fb9c6b4e-9b97-4c93-897d-f159735935d6', 'https://drive.google.com/uc?export=view&id=1IquV-P0qtIAqOecyKRjQVBD6vO47biyV'),
    (NEWID(), 'fb9c6b4e-9b97-4c93-897d-f159735935d6', 'https://drive.google.com/uc?export=view&id=1N6rCDit2mal2xMQrChT5c8d7Q2K_OYHA'),

    (NEWID(), '89fb1e2e-7540-4fca-9ea5-94c38195010c', 'https://drive.google.com/uc?export=view&id=1EdEbBoq_hw-U50BFDZox2bHJcOadVxMB'),
    (NEWID(), '89fb1e2e-7540-4fca-9ea5-94c38195010c', 'https://drive.google.com/uc?export=view&id=1_vyN3zk2_oq2fmTP2KdcPbdaq_yzTvan'),
    (NEWID(), '89fb1e2e-7540-4fca-9ea5-94c38195010c', 'https://drive.google.com/uc?export=view&id=1UUWyqo_74WubN2PKz8ZtDVRTn1uUHjfJ'),

    (NEWID(), 'b5b42281-6b59-4ee1-9590-f94953a6dcbd', 'https://drive.google.com/uc?export=view&id=1Iy_sBLrddmURcmm8UsGMVrQgThDqC0vN'),
    (NEWID(), 'b5b42281-6b59-4ee1-9590-f94953a6dcbd', 'https://drive.google.com/uc?export=view&id=1cTWVn6jIOhO457PbEWDBJksD79nCdzJz'),
    (NEWID(), 'b5b42281-6b59-4ee1-9590-f94953a6dcbd', 'https://drive.google.com/uc?export=view&id=1wHTlxpYm2G41VWamqAAG3JiVnDYmecUf'),

    (NEWID(), '1a4c7538-a7eb-4507-aa55-16857aef18d6', 'https://drive.google.com/uc?export=view&id=1ATYbLbHRTd2pw3Ezz70vuUPCdD75ZeD9'),
    (NEWID(), '1a4c7538-a7eb-4507-aa55-16857aef18d6', 'https://drive.google.com/uc?export=view&id=1e4N0vfxNEbsx50s_zOJ_wvw0MOWg_IrJ'),
    (NEWID(), '1a4c7538-a7eb-4507-aa55-16857aef18d6', 'https://drive.google.com/uc?export=view&id=1fibxPXpQCxt4QlBHHFUedqzRVYZ3cqUT'),

    (NEWID(), '84c34d24-b8e5-4048-9eaf-b494c584c744', 'https://drive.google.com/uc?export=view&id=1tGTywUieD2oVYoQzAggZcb0GpyCXQ9bk'),
    (NEWID(), '84c34d24-b8e5-4048-9eaf-b494c584c744', 'https://drive.google.com/uc?export=view&id=1pa-yWLAfEfHAGboTXi_0CssVI5SVTq6q'),
    (NEWID(), '84c34d24-b8e5-4048-9eaf-b494c584c744', 'https://drive.google.com/uc?export=view&id=19gK0tv811zGdZ9eqnYbJlHOsgSTlBpIL'),

    (NEWID(), '58556804-5b1e-49da-8c80-e29de6fc273b', 'https://drive.google.com/uc?export=view&id=1Mevo_es2s12JWHFK3fc0QwliEF8XsYU-'),
    (NEWID(), '58556804-5b1e-49da-8c80-e29de6fc273b', 'https://drive.google.com/uc?export=view&id=1WkDqU5NxU9CQBc0ee2j0RjXjOBKSyxrG'),

    (NEWID(), '27497fd7-531e-473f-9ead-222afd0a9b4a', 'https://drive.google.com/uc?export=view&id=1T_CRsp_V6L_jZKstbEQL5KPw3EjFksUY'),
    (NEWID(), '27497fd7-531e-473f-9ead-222afd0a9b4a', 'https://drive.google.com/uc?export=view&id=1TaGBcfhw7v6J2uKVS_mWnHr2cQFj8_NS'),

    (NEWID(), 'c4a08c40-ab2c-4346-8190-09ba0bb53ac2', 'https://drive.google.com/uc?export=view&id=1hW5iLGOh_zNLCi0rEfr6HcnV3fXb3OHl'),
    (NEWID(), 'c4a08c40-ab2c-4346-8190-09ba0bb53ac2', 'https://drive.google.com/uc?export=view&id=1o5wej_gm1d-IZjUJY22Uxa92XqAje63h'),

    (NEWID(), 'ec48d769-bb82-44f5-9d5b-fa85672df375', 'https://drive.google.com/uc?export=view&id=1BC5kXL2RiS5aVQXZaDCP0qanwC9kj9OE'),
    (NEWID(), 'ec48d769-bb82-44f5-9d5b-fa85672df375', 'https://drive.google.com/uc?export=view&id=1SrtlEmVnUUd1kAMvXJTCP3LE8GyQ9j46'),

    (NEWID(), 'a0b85774-7131-4612-a0c4-6d0ec29484ee', 'https://drive.google.com/uc?export=view&id=175w1V8LzAo0eA3MENFI224-6zjaNf9I1'),
    (NEWID(), 'a0b85774-7131-4612-a0c4-6d0ec29484ee', 'https://drive.google.com/uc?export=view&id=1CkJdkK9qIfuzC580Q2NCbYSoIevGOoye'),

    (NEWID(), '35055e95-fda4-44e5-9181-5be27d1e1588', 'https://drive.google.com/uc?export=view&id=1YraCszJtadmNUBx8cZAAWTqfc3HjQQ6x'),
    (NEWID(), '35055e95-fda4-44e5-9181-5be27d1e1588', 'https://drive.google.com/uc?export=view&id=1dBcdUmbYVntxSLBNTW-ADAu2yqJdQAG6'),
    (NEWID(), '35055e95-fda4-44e5-9181-5be27d1e1588', 'https://drive.google.com/uc?export=view&id=1XyUUu8jfjgDFOK1sgKOkw0COeYi8iutL'),

    (NEWID(), '4cf69ad1-099c-43e8-a5ce-971dd1757f65', 'https://drive.google.com/uc?export=view&id=1_sXdoU_UVI4Mb5xsCncWh-4TM1pvm9iJ'),
    (NEWID(), '4cf69ad1-099c-43e8-a5ce-971dd1757f65', 'https://drive.google.com/uc?export=view&id=1v7PiYdYL-TOoTfz30vRvxdh7u7h-xqBE'),
    (NEWID(), '4cf69ad1-099c-43e8-a5ce-971dd1757f65', 'https://drive.google.com/uc?export=view&id=1o7DQHv6Nv7blEGrdHmvbDVsb_I3oF_4v'),

    (NEWID(), 'b3290472-e196-48ee-bd09-5b506510c2b1', 'https://drive.google.com/uc?export=view&id=1pYjbCOz6XgzOvGIVANkS_U0Cw-6_oDYO'),
    (NEWID(), 'b3290472-e196-48ee-bd09-5b506510c2b1', 'https://drive.google.com/uc?export=view&id=1Lig22VgGBTYyfMUZFmRK0WXmuWvZiw39'),
    (NEWID(), 'b3290472-e196-48ee-bd09-5b506510c2b1', 'https://drive.google.com/uc?export=view&id=1NB3HC4uh3c4zcSOFepqBCu873Ht1HGFe'),

    (NEWID(), '3f024cb7-73aa-42e3-bbe0-8b798962227e', 'https://drive.google.com/uc?export=view&id=1v1L5cTx8zpr-JWNUwEnMf2vSUQE4KH2Z'),
    (NEWID(), '3f024cb7-73aa-42e3-bbe0-8b798962227e', 'https://drive.google.com/uc?export=view&id=1dvapVxQ1BpVptbu-tb9mZMdkZIQiku-d'),
    (NEWID(), '3f024cb7-73aa-42e3-bbe0-8b798962227e', 'https://drive.google.com/uc?export=view&id=1gizUcQHt6u1aUAmrLwOb7W4RhZydmmFo'),

    (NEWID(), 'b7972fd1-bfa3-4aea-8c87-43603406af60', 'https://drive.google.com/uc?export=view&id=1VQBdgp3I2_KQbbd1yME462AOlMuZjnVq'),
    (NEWID(), 'b7972fd1-bfa3-4aea-8c87-43603406af60', 'https://drive.google.com/uc?export=view&id=16rGJGxYVsaA1ecjlHzatXnIlOodFvUO5'),
    (NEWID(), 'b7972fd1-bfa3-4aea-8c87-43603406af60', 'https://drive.google.com/uc?export=view&id=1kndNvTZkt3VRaObVFNZm4ZPCFFz_pcsE'),

    (NEWID(), '3cabc72e-1967-4249-bd9e-31f98fd5107b', 'https://drive.google.com/uc?export=view&id=1c2RVHV0nc05VIeQvK04LpOMGoI9koK-3'),
    (NEWID(), '3cabc72e-1967-4249-bd9e-31f98fd5107b', 'https://drive.google.com/uc?export=view&id=1OsU-AxmqfGcvGYWU8cSSJkIIqyXhvmfU'),
    (NEWID(), '3cabc72e-1967-4249-bd9e-31f98fd5107b', 'https://drive.google.com/uc?export=view&id=1STkcbBrwbvZ-KVbhyy6pCt91gc9xhIEd'),

    (NEWID(), 'b8b92837-d6d1-40df-b6fe-eb92c645da72', 'https://drive.google.com/uc?export=view&id=1XFNoJM6bfxZoTKBa4itr0z3wEt-V3-HS'),
    (NEWID(), 'b8b92837-d6d1-40df-b6fe-eb92c645da72', 'https://drive.google.com/uc?export=view&id=1zINZ8kQuBBk1FDUGuyE9LHcpMTcJCao3'),
    (NEWID(), 'b8b92837-d6d1-40df-b6fe-eb92c645da72', 'https://drive.google.com/uc?export=view&id=1AHUDe4n7XrvmHZLX8tQVvXr_ujXxCGnl'),

    (NEWID(), 'fb269342-7866-4e3a-90b2-3e57a24f016c', 'https://drive.google.com/uc?export=view&id=1gaHEFhumCyQuKp0oWBbBIrmmOSkvq5i9'),
    (NEWID(), 'fb269342-7866-4e3a-90b2-3e57a24f016c', 'https://drive.google.com/uc?export=view&id=1HBvqHRg8y0KdkmouH1he8eDjeXujMlM3'),
    (NEWID(), 'fb269342-7866-4e3a-90b2-3e57a24f016c', 'https://drive.google.com/uc?export=view&id=1X45RJACuFl_wOz7Iok2PlxuTEN2Dy7Mu'),

    (NEWID(), '8ecaaeed-f488-445c-ac22-bab1e158198e', 'https://drive.google.com/uc?export=view&id=1-gNcnlXcIYmArikNOpSN5dQWCf1DrSEi'),
    (NEWID(), '8ecaaeed-f488-445c-ac22-bab1e158198e', 'https://drive.google.com/uc?export=view&id=10rkINVT0dKY6U3PHjNcA8SAKTibCj3rQ'),

    (NEWID(), 'fd5006c6-9ad3-495f-85f0-2a87696947f2', 'https://drive.google.com/uc?export=view&id=1q7NTSP7gMWH4B_TCop2EQGNT1K7MBQxs'),
    (NEWID(), 'fd5006c6-9ad3-495f-85f0-2a87696947f2', 'https://drive.google.com/uc?export=view&id=1_OSVR-rZl9msRfyVuDP7C6g7eeQUnYCp'),

    (NEWID(), '69b91dcc-09c3-41af-8bc5-cbdf4083766e', 'https://drive.google.com/uc?export=view&id=1Q1ecSs7KIutDsyFeBuNfAcenUp0KC8Ao'),
    (NEWID(), '69b91dcc-09c3-41af-8bc5-cbdf4083766e', 'https://drive.google.com/uc?export=view&id=1snmntGOqfNWoYluRHTXTT4GNzyL-nn2b'),

    (NEWID(), '7160fec0-2f00-4469-afe5-5816ed082e83', 'https://drive.google.com/uc?export=view&id=1kZWWnwCZokrBg2oeOoU9t9pBfNWlKS7L'),
    (NEWID(), '7160fec0-2f00-4469-afe5-5816ed082e83', 'https://drive.google.com/uc?export=view&id=1hpmG6JS71bo5WUrlUZc1QiWT7yZVuhlk'),

    (NEWID(), 'ab06f0a9-d8a8-4320-9a6e-11858ddbdd34', 'https://drive.google.com/uc?export=view&id=1jDEIKT-D0gf2rpmP1kGTQbjsI-5IJwhh'),
    (NEWID(), 'ab06f0a9-d8a8-4320-9a6e-11858ddbdd34', 'https://drive.google.com/uc?export=view&id=1m1Iw-ya4gp5UhtRbTLX-QtLJI7gMuqYg'),

    (NEWID(), 'cad24e2f-ceb3-4a0a-8863-bba1f7850ef3', 'https://drive.google.com/uc?export=view&id=1uCTNTZa2Yk9xl_Z8SIQpGMKoStO2u5W7'),
    (NEWID(), 'cad24e2f-ceb3-4a0a-8863-bba1f7850ef3', 'https://drive.google.com/uc?export=view&id=1UJDE5WP5XLR1T0-6VAYxfomc6xEv294o'),

    (NEWID(), '25418d43-ee71-44db-868a-77cec0d873ee', 'https://drive.google.com/uc?export=view&id=1HxQvjJqUApmdkSKLLBlA9zkj3Qq3O2jP'),
    (NEWID(), '25418d43-ee71-44db-868a-77cec0d873ee', 'https://drive.google.com/uc?export=view&id=1_-c4XcBEqFMxO9nXfPR6k5GhdlUc7sJI'),

    (NEWID(), '785f049f-75ea-4713-8755-44d07890d1ce', 'https://drive.google.com/uc?export=view&id=1Xff6ubL1THQoqZZTmVX5REmzMo1dMljN'),
    (NEWID(), '785f049f-75ea-4713-8755-44d07890d1ce', 'https://drive.google.com/uc?export=view&id=1ro45EJyAWJX9ZcIgapP5twOAJsMYyrgi'),

    (NEWID(), 'c51643d1-3f46-4bed-bb8d-42e8c8ce521d', 'https://drive.google.com/uc?export=view&id=1yAnExd-2n060POLtwZW-tNqPHHaZvQC9'),
    (NEWID(), 'c51643d1-3f46-4bed-bb8d-42e8c8ce521d', 'https://drive.google.com/uc?export=view&id=1__xmb9b53USuRCERpaGp0NVyb7CCLtJX'),

    (NEWID(), '978addb6-cbc4-4d76-8c14-d2279dcdcd0f', 'https://drive.google.com/uc?export=view&id=1pz97_XPjLTXGFkAF82YWZlvxHFRECDf7'),
    (NEWID(), '978addb6-cbc4-4d76-8c14-d2279dcdcd0f', 'https://drive.google.com/uc?export=view&id=1JOuHzq_NAlizlblEAd0UKhDMLQiH30VB'),

    (NEWID(), 'c57489d2-3f72-411e-889c-62cc364174b1', 'https://drive.google.com/uc?export=view&id=1It9ic0iZpFDOyg_btbh9xMDtVOsrObjP'),
    (NEWID(), 'c57489d2-3f72-411e-889c-62cc364174b1', 'https://drive.google.com/uc?export=view&id=1dQwios3Fb3mymfwCxrg8BUILH19zKsdy'),

    (NEWID(), '75a8b8b0-04e8-4d4f-9462-79ccb13a03fb', 'https://drive.google.com/uc?export=view&id=16dmsryba6ZQpSVQZrqiNDrjcX0NezRPy'),
    (NEWID(), '75a8b8b0-04e8-4d4f-9462-79ccb13a03fb', 'https://drive.google.com/uc?export=view&id=1ifQprt1XoF65FEwc5vuHY_pfvYRUItIb'),

    (NEWID(), '3b4d9d95-b96a-4fa0-83b1-2d4432eec939', 'https://drive.google.com/uc?export=view&id=1hNnir3LpACEO25yOrLdD-SNKJKXWrq4V'),
    (NEWID(), '3b4d9d95-b96a-4fa0-83b1-2d4432eec939', 'https://drive.google.com/uc?export=view&id=1eLYfhFFB9PfrXZ74JLOJZ53JdGK7l3aH'),

    (NEWID(), '6e889ab7-bf7e-4058-ac4a-8c64a242229f', 'https://drive.google.com/uc?export=view&id=1_AdsMF5tCZBwot5HFTcIXWNwY2eMgz9N'),
    (NEWID(), '6e889ab7-bf7e-4058-ac4a-8c64a242229f', 'https://drive.google.com/uc?export=view&id=1iab4lyGK0ucd5Guc6r_zsz73weps2NR3'),

    (NEWID(), '4d120703-a73d-4941-856e-24b69f999d4b', 'https://drive.google.com/uc?export=view&id=1iMjNg7GZuiM-J1bOTxvYspa_bcPn_jTV'),
    (NEWID(), '4d120703-a73d-4941-856e-24b69f999d4b', 'https://drive.google.com/uc?export=view&id=1TCxpKJkc-kkDB-qte8Bm9izBu8wIUB3Z');


CREATE TABLE #TempAttributes (
    ComponentId UNIQUEIDENTIFIER,
    AttributeName NVARCHAR(MAX),
    AttributeValue NVARCHAR(MAX)
);

INSERT INTO #TempAttributes (ComponentId, AttributeName, AttributeValue)
SELECT 
    [id] AS ComponentId, 
    [key] AS AttributeName, 
    [value] AS AttributeValue
FROM OPENROWSET (
    BULK '/src/Services/Aggregator/Aggregator.DataAccess/Scripts/componentsAttributes.json',
    SINGLE_CLOB
) AS j
CROSS APPLY OPENJSON(j.BulkColumn) 
WITH (
    id UNIQUEIDENTIFIER,
    attributes NVARCHAR(MAX) AS JSON
) AS sub
CROSS APPLY OPENJSON(sub.attributes) AS attr;


INSERT INTO ComponentAttributes (Id, ComponentId, AttributeName, AttributeValue)
SELECT NEWID(), ComponentId, AttributeName, AttributeValue
FROM #TempAttributes;

DROP TABLE #TempAttributes;
