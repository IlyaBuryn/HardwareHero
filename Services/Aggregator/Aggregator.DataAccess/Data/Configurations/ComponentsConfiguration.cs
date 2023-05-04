using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Data.Configurations
{
    internal class ComponentsConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(256);
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Description).HasMaxLength(1024);

            builder.HasData
            (
                new Component()
                {
                    Id = new Guid("0712d311-71e5-4c5b-8f80-1b1b08180851"),
                    Name = "ASUS TUF Gaming NVIDIA GeForce RTX 3070 Ti",
                    Description = "ASUS TUF Gaming NVIDIA GeForce RTX 3070 Ti OC V2 Graphics Card (PCIe 4.0, 8GB GDDR6X, HDMI 2.1, DisplayPort 1.4a, Military-grade Certification, GPU Tweak III) TUF-RTX3070TI-O8G-V2-GAMING",
                    ImageList = new List<string> { "GPU1.png", "GPU2.png", "GPU3.png" },
                    SpecificationDictionary = new Dictionary<string, string> { 
                        { "Brand", "ASUS" }, { "Series", "TUF Gaming OC" }, { "Model", "TUF-RTX3070TI-O8G-V2-GAMING" },
                        { "Interface", "PCI Express 4.0" }, { "Chipset Manufacturer", "NVIDIA" }, { "GPU Series", "NVIDIA GeForce RTX 30 Series" }, { "GPU", "GeForce RTX 3070 Ti" },
                        { "Boost Clock", "1815 MHz" }, { "CUDA Cores", "6144 Cores" },
                        { "Memory Size", "8GB" }, { "Memory Interface", "256-Bit" }, { "Memory Type", "GDDR6X" },
                        { "DirectX", "DirectX 12" }, { "OpenGL", "OpenGL 4.6" },
                        { "Multi-Monitor Support", "4" }, { "HDMI", "2 x HDMI 2.1" }, { "DisplayPort", "3 x DisplayPort 1.4a" },
                        { "Max Resolution", "7680 x 4320" }, { "Cooler", "Triple Fans" }, { "Thermal Design Power", "290W" }, { "Recommended PSU Wattage", "750W" }, { "Power Connector", "2 x 8-Pin" },
                        { "Form Factor", "ATX" }, { "Max GPU Length", "300 mm" }, { "Slot Width", "2.7 Slot" } },
                    InitialPrice = 569.99m,
                },
                new Component()
                {
                    Id = new Guid("17bb6742-6611-4865-99f4-222610fb1b88"),
                    Name = "Intel Core i9-13900K",
                    Description = "Intel Core i9-13900K - Core i9 13th Gen Raptor Lake 24-Core (8P+16E) P-core Base Frequency: 3.0 GHz E-core Base Frequency: 2.2 GHz LGA 1700 125W Intel UHD Graphics 770 Desktop Processor - BX8071513900K",
                    ImageList = new List<string> { "CPU1.png", "CPU2.png", "CPU3.png", "CPU4.png" },
                    SpecificationDictionary = new Dictionary<string, string> { 
                        { "Brand", "Intel" }, { "Processors Type", "Desktop" }, { "Series", "Core i9 13th Gen" }, { "Name", "Core i9-13900K" }, { "Model", "BX8071513900K" },
                        { "CPU Socket Type", "LGA 1700" }, { "Core Name", "Raptor Lake" }, { "# of Cores", "24-Core (8P+16E)" }, { "# of Threads", "32" }, { "Operating Frequency", "P-core Base Frequency: 3.0 GHz\r\nE-core Base Frequency: 2.2 GHz" },
                        { "Max Turbo Frequency", "Intel Turbo Boost Max Technology 3.0 Frequency: Up to 5.7 GHz\r\nSingle P-core Turbo Frequency: Up to 5.4 GHz\r\nSingle E-core Turbo Frequency: Up to 4.3 GHz" },
                        { "L2 Cache", "32MB" }, { "L3 Cache", "36MB" }, { "64-Bit Support", "Yes" }, { "Hyper-Threading Support", "Yes" },
                        { "Memory Types", "DDR4 3200 / DDR5 5600" }, { "Memory Channel", "2" }, { "Max Memory Size", "128 GB" }, { "ECC Memory", "Supported" },
                        { "Integrated Graphics", "Intel UHD Graphics 770" }, { "Thermal Design Power", "125W" }, { "Windows 11", "Supported" } },
                    InitialPrice = 569.97m,
                },
                new Component()
                {
                    Id = new Guid("aca946fc-6165-4bc6-94cd-c1cf43b46f42"),
                    Name = "GIGABYTE Z590 AORUS PRO",
                    Description  = "GIGABYTE Z590 AORUS PRO AX LGA 1200 Intel Z590 ATX Motherboard with 4 x M.2, PCIe 4.0, USB 3.2 Gen2X2 Type-C, Intel WIFI 6, 2.5GbE LAN",
                    ImageList = new List<string> { "MB1.png", "MB2.png", "MB3.png", "MB4.png", "MB5.png" },
                    SpecificationDictionary = new Dictionary<string, string> {
                        { "Brand", "GIGABYTE" }, { "Model", "Z590 AORUS PRO AX" },
                        { "CPU Socket Type", "LGA 1200" },
                        { "Chipset", "Intel Z590" }, { "Onboard Video Chipset", "Supported only by CPU with integrated graphic" },
                        { "Number of Memory Slots", "4x288pin (DDR4)" }, { "Memory Standard", "DDR4 5400" }, { "Maximum Memory Supported", "128GB" }, { "Channel Supported", "Dual Channel" },
                        { "PCI Express 4.0 x16", "1 x PCI Express 4.0 x16" }, { "PCI Express 3.0 x16", "2 x PCI Express 3.0 x16" },
                        { "SATA 6Gb/s", "6 x SATA 6Gb/s" }, { "Intel Optane Ready", "Yes" }, { "SATA RAID", "0/1/5/10" }, { "M.2", "1" },
                        { "Audio Chipset", "Realtek ALC4080" }, { "Audio Channels", "7.1 Channels" },
                        { "LAN Chipset", "Intel 2.5GbE LAN chip" }, { "Max LAN Speed", "2.5Gbps" }, { "Wireless LAN", "ntel Wi-Fi 6 AX200\r\nWIFI a/b/g/n/ac/ax" }, { "Bluetooth", "Bluetooth 5.1" },
                        { "Form Factor", "ATX" }, { "LED Lighting", "RGB" }, { "Dimensions (W x L)", "12.0\" x 9.6\"" }, { "Power Pin", "1 x 24-pin ATX main power connector\r\n1 x 8-pin ATX 12V power connector\r\n1 x 4-pin ATX 12V power connector" },
                        { "Windows 11", "Supported" } },
                    InitialPrice = 289.99m,
                }
            );
        }
    }
}
