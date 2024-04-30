using DeviceId;
using DeviceId.Windows;
using DeviceId.Windows.Wmi;
using DeviceId.Linux;
//using DeviceId.Mac;
namespace TM.Desktop
{
    public static class DeviceID
    {
        public static string test()
        {
            var a = new DeviceIdBuilder().AddMachineName();
            return a.ToString();
        }
        public static string get()
        {
            return new DeviceIdBuilder()
                 .AddMachineName()
                 .AddOsVersion()
                 .OnWindows(windows => windows
                     //.AddProcessorId()
                     .AddMotherboardSerialNumber()
                     .AddSystemDriveSerialNumber())
                 .OnLinux(linux => linux
                     .AddMotherboardSerialNumber()
                     .AddSystemDriveSerialNumber())
                 .OnMac(mac => mac
                     .AddSystemDriveSerialNumber()
                     .AddPlatformSerialNumber())
                 .ToString();
        }
    }
}
