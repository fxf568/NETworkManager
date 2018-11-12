﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using NETworkManager.Models.Lookup;
using NETworkManager.Models.Network;

namespace NETworkManager.Models.Export
{
    public static class ExportManager
    {
        #region Variables
        private static readonly XDeclaration DefaultXDeclaration = new XDeclaration("1.0", "utf-8", "yes");
        #endregion

        #region Methods

        #region Export
        public static void Export(string filePath, ExportFileType fileType, ObservableCollection<HostInfo> collection)
        {
            switch (fileType)
            {
                case ExportFileType.CSV:
                    CreateCSV(collection, filePath);
                    break;
                case ExportFileType.XML:
                    CreateXML(collection, filePath);
                    break;
                case ExportFileType.JSON:
                    CreateJSON(collection, filePath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null);
            }
        }

        public static void Export(string filePath, ExportFileType fileType, ObservableCollection<PortInfo> collection)
        {
            switch (fileType)
            {
                case ExportFileType.CSV:
                    CreateCSV(collection, filePath);
                    break;
                case ExportFileType.XML:
                    CreateXML(collection, filePath);
                    break;
                case ExportFileType.JSON:
                    CreateJSON(collection, filePath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null);
            }
        }

        public static void Export(string filePath, ExportFileType fileType, ObservableCollection<PingInfo> collection)
        {
            switch (fileType)
            {
                case ExportFileType.CSV:
                    CreateCSV(collection, filePath);
                    break;
                case ExportFileType.XML:
                    CreateXML(collection, filePath);
                    break;
                case ExportFileType.JSON:
                    CreateJSON(collection, filePath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null);
            }
        }

        public static void Export(string filePath, ExportFileType fileType, ObservableCollection<TracerouteHopInfo> collection)
        {
            switch (fileType)
            {
                case ExportFileType.CSV:
                    CreateCSV(collection, filePath);
                    break;
                case ExportFileType.XML:
                    CreateXML(collection, filePath);
                    break;
                case ExportFileType.JSON:
                    CreateJSON(collection, filePath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null);
            }
        }
        #endregion

        #region CreateCSV
        private static void CreateCSV(IEnumerable<HostInfo> collection, string filePath)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{nameof(PingInfo.IPAddress)},{nameof(HostInfo.Hostname)},{nameof(HostInfo.MACAddress)},{nameof(HostInfo.Vendor)},{nameof(PingInfo.Bytes)},{nameof(PingInfo.Time)},{nameof(PingInfo.TTL)},{nameof(PingInfo.Status)}");

            foreach (var info in collection)
                stringBuilder.AppendLine($"{info.PingInfo.IPAddress},{info.Hostname},{info.MACAddress},{info.Vendor},{info.PingInfo.Bytes},{Ping.TimeToString(info.PingInfo.Status, info.PingInfo.Time, true)},{info.PingInfo.TTL},{info.PingInfo.Status}");

            System.IO.File.WriteAllText(filePath, stringBuilder.ToString());
        }

        private static void CreateCSV(IEnumerable<PortInfo> collection, string filePath)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{nameof(PortInfo.IPAddress)},{nameof(PortInfo.Hostname)},{nameof(PortInfo.Port)},{nameof(PortLookupInfo.Protocol)},{nameof(PortLookupInfo.Service)},{nameof(PortLookupInfo.Description)},{nameof(PortInfo.Status)}");

            foreach (var info in collection)
                stringBuilder.AppendLine($"{info.IPAddress},{info.Hostname},{info.Port},{info.LookupInfo.Protocol},{info.LookupInfo.Service},{info.LookupInfo.Description},{info.Status}");

            System.IO.File.WriteAllText(filePath, stringBuilder.ToString());
        }

        private static void CreateCSV(IEnumerable<PingInfo> collection, string filePath)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{nameof(PingInfo.Timestamp)},{nameof(PingInfo.IPAddress)},{nameof(PingInfo.Hostname)},{nameof(PingInfo.Bytes)},{nameof(PingInfo.Time)},{nameof(PingInfo.TTL)},{nameof(PingInfo.Status)}");

            foreach (var info in collection)
                stringBuilder.AppendLine($"{info.Timestamp},{info.IPAddress},{info.Hostname},{info.Bytes},{Ping.TimeToString(info.Status, info.Time, true)},{info.TTL},{info.Status}");

            System.IO.File.WriteAllText(filePath, stringBuilder.ToString());
        }

        private static void CreateCSV(IEnumerable<TracerouteHopInfo> collection, string filePath)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{nameof(TracerouteHopInfo.Hop)},{nameof(TracerouteHopInfo.Time1)},{nameof(TracerouteHopInfo.Time2)},{nameof(TracerouteHopInfo.Time3)},{nameof(TracerouteHopInfo.IPAddress)},{nameof(TracerouteHopInfo.Hostname)},{nameof(TracerouteHopInfo.Status1)},{nameof(TracerouteHopInfo.Status2)},{nameof(TracerouteHopInfo.Status3)}");

            foreach (var info in collection)
                stringBuilder.AppendLine($"{info.Hop},{Ping.TimeToString(info.Status1, info.Time1, true)},{Ping.TimeToString(info.Status2, info.Time2, true)},{Ping.TimeToString(info.Status3, info.Time3, true)},{info.IPAddress},{info.Hostname},{info.Status1},{info.Status2},{info.Status3}");

            System.IO.File.WriteAllText(filePath, stringBuilder.ToString());
        }
        #endregion

        #region CreateXML
        public static void CreateXML(IEnumerable<HostInfo> collection, string filePath)
        {
            var document = new XDocument(DefaultXDeclaration,

                new XElement(ApplicationViewManager.Name.IPScanner.ToString(),
                    new XElement(nameof(HostInfo) + "s",

                    from info in collection
                    select
                        new XElement(nameof(HostInfo),
                            new XElement(nameof(PingInfo.IPAddress), info.PingInfo.IPAddress),
                            new XElement(nameof(HostInfo.Hostname), info.Hostname),
                            new XElement(nameof(HostInfo.MACAddress), info.MACAddress),
                            new XElement(nameof(HostInfo.Vendor), info.Vendor),
                            new XElement(nameof(PingInfo.Bytes), info.PingInfo.Bytes),
                            new XElement(nameof(PingInfo.Time), Ping.TimeToString(info.PingInfo.Status, info.PingInfo.Time, true)),
                            new XElement(nameof(PingInfo.TTL), info.PingInfo.TTL),
                            new XElement(nameof(PingInfo.Status), info.PingInfo.Status)))));

            document.Save(filePath);
        }

        public static void CreateXML(IEnumerable<PortInfo> collection, string filePath)
        {
            var document = new XDocument(DefaultXDeclaration,

                new XElement(ApplicationViewManager.Name.PortScanner.ToString(),
                    new XElement(nameof(PortInfo) + "s",

                        from info in collection
                        select
                            new XElement(nameof(PortInfo),
                                new XElement(nameof(PortInfo.IPAddress), info.IPAddress),
                                new XElement(nameof(PortInfo.Hostname), info.Hostname),
                                new XElement(nameof(PortInfo.Port), info.Port),
                                new XElement(nameof(PortLookupInfo.Protocol), info.LookupInfo.Protocol),
                                new XElement(nameof(PortLookupInfo.Service), info.LookupInfo.Service),
                                new XElement(nameof(PortLookupInfo.Description), info.LookupInfo.Description),
                                new XElement(nameof(PortInfo.Status), info.Status)))));

            document.Save(filePath);
        }

        public static void CreateXML(IEnumerable<PingInfo> collection, string filePath)
        {
            var document = new XDocument(DefaultXDeclaration,

                new XElement(ApplicationViewManager.Name.Ping.ToString(),
                    new XElement(nameof(PingInfo) + "s",

                        from info in collection
                        select
                            new XElement(nameof(PingInfo),
                                new XElement(nameof(PingInfo.Timestamp), info.Timestamp),
                                new XElement(nameof(PingInfo.IPAddress), info.IPAddress),
                                new XElement(nameof(PingInfo.Hostname), info.Hostname),
                                new XElement(nameof(PingInfo.Bytes), info.Bytes),
                                new XElement(nameof(PingInfo.Time), Ping.TimeToString(info.Status, info.Time, true)),
                                new XElement(nameof(PingInfo.TTL), info.TTL),
                                new XElement(nameof(PingInfo.Status), info.Status)))));

            document.Save(filePath);
        }

        public static void CreateXML(IEnumerable<TracerouteHopInfo> collection, string filePath)
        {
            var document = new XDocument(DefaultXDeclaration,

                new XElement(ApplicationViewManager.Name.Traceroute.ToString(),
                    new XElement(nameof(TracerouteHopInfo) + "s",

                        from info in collection
                        select
                            new XElement(nameof(TracerouteHopInfo),
                                new XElement(nameof(TracerouteHopInfo.Hop), info.Hop),
                                new XElement(nameof(TracerouteHopInfo.Time1), Ping.TimeToString(info.Status1, info.Time1, true)),
                                new XElement(nameof(TracerouteHopInfo.Time2), Ping.TimeToString(info.Status2, info.Time2, true)),
                                new XElement(nameof(TracerouteHopInfo.Time3), Ping.TimeToString(info.Status3, info.Time3, true)),
                                new XElement(nameof(TracerouteHopInfo.IPAddress), info.IPAddress),
                                new XElement(nameof(TracerouteHopInfo.Hostname), info.Hostname),
                                new XElement(nameof(TracerouteHopInfo.Status1), info.Status1),
                                new XElement(nameof(TracerouteHopInfo.Status2), info.Status2),
                                new XElement(nameof(TracerouteHopInfo.Status3), info.Status3)))));

            document.Save(filePath);
        }
        #endregion

        #region CreateJSON
        // This might be a horror to maintain, but i have no other idea...
        public static void CreateJSON(ObservableCollection<HostInfo> collection, string filePath)
        {
            var jsonData = new object[collection.Count];

            for (var i = 0; i < collection.Count; i++)
            {
                jsonData[i] = new
                {
                    IPAddress = collection[i].PingInfo.IPAddress.ToString(),
                    collection[i].Hostname,
                    MACAddress = collection[i].MACAddress.ToString(),
                    collection[i].Vendor,
                    collection[i].PingInfo.Bytes,
                    Time = Ping.TimeToString(collection[i].PingInfo.Status, collection[i].PingInfo.Time, true),
                    collection[i].PingInfo.TTL,
                    Status = collection[i].PingInfo.Status.ToString()
                };
            }

            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonData, Formatting.Indented));
        }

        public static void CreateJSON(ObservableCollection<PortInfo> collection, string filePath)
        {
            var jsonData = new object[collection.Count];

            for (var i = 0; i < collection.Count; i++)
            {
                jsonData[i] = new
                {
                    IPAddress = collection[i].IPAddress.ToString(),
                    collection[i].Hostname,
                    collection[i].Port,
                    Protocol = collection[i].LookupInfo.Protocol.ToString(),
                    collection[i].LookupInfo.Service,
                    collection[i].LookupInfo.Description,
                    Status = collection[i].Status.ToString()
                };
            }

            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonData, Formatting.Indented));
        }

        public static void CreateJSON(ObservableCollection<PingInfo> collection, string filePath)
        {
            var jsonData = new object[collection.Count];

            for (var i = 0; i < collection.Count; i++)
            {
                jsonData[i] = new
                {
                    collection[i].Timestamp,
                    IPAddress = collection[i].IPAddress.ToString(),
                    collection[i].Hostname,
                    collection[i].Bytes,
                    Time = Ping.TimeToString(collection[i].Status, collection[i].Time, true),
                    collection[i].TTL,
                    Status = collection[i].Status.ToString()
                };
            }

            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonData, Formatting.Indented));
        }

        public static void CreateJSON(ObservableCollection<TracerouteHopInfo> collection, string filePath)
        {
            var jsonData = new object[collection.Count];

            for (var i = 0; i < collection.Count; i++)
            {
                jsonData[i] = new
                {
                    collection[i].Hop,
                    Time1 = Ping.TimeToString(collection[i].Status1, collection[i].Time1, true),
                    Time2 = Ping.TimeToString(collection[i].Status2, collection[i].Time2, true),
                    Time3 = Ping.TimeToString(collection[i].Status3, collection[i].Time3, true),
                    IPAddress = collection[i].IPAddress.ToString(),
                    collection[i].Hostname,
                    Status1 = collection[i].Status1.ToString(),
                    Status2 = collection[i].Status2.ToString(),
                    Status3 = collection[i].Status3.ToString()
                };
            }

            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonData, Formatting.Indented));
        }
        #endregion

        public static string GetFileExtensionAsString(ExportFileType fileExtension)
        {
            switch (fileExtension)
            {
                case ExportFileType.CSV:
                    return "CSV";
                case ExportFileType.XML:
                    return "XML";
                case ExportFileType.JSON:
                    return "JSON";
                default:
                    return string.Empty;
            }
        }
        #endregion

        public enum ExportFileType
        {
            CSV,
            XML,
            JSON
        }
    }
}