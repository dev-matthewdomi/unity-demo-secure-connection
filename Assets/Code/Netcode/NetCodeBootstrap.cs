using System;
using Code.SecureDriver;
using Unity.NetCode;
using UnityEngine.Scripting;

namespace Code.Netcode
{
    [Preserve]
    public class NetCodeBootstrap : ClientServerBootstrap
    {
        private enum TestConfiguration
        {
            // Use the default automatic connect boostrap
            AutomaticBootstrap,
        
            //  Click the button to trigger ConnectionSystem to tell the NetworkStreamDriver to listen and connect the
            //  client by adding NetworkStreamRequestListen/Connect components to the singleton entity
            //  TODO: For some reason this is broken because the NetworkStreamDriver singleton is destroyed on server listen
            ManualConnectionComponents,
        
            //  Click the button to trigger ConnectionSystem to tell the NetworkStreamDriver to listen and connect the
            //  client by calling the Listen/Connect methods directly on the singleton
            ManualConnectionDirect
        }
        
        public const ushort Port = 7979;
        public static readonly bool UseManualConnect;
        public static readonly bool UseComponentsToConnect;
        
        // Modify this to test out the different connection flows
        private const TestConfiguration TestConfig = TestConfiguration.ManualConnectionDirect;
        private const bool UseSecureDriver = true;

        static NetCodeBootstrap()
        {
            switch (TestConfig)
            {
                case TestConfiguration.AutomaticBootstrap:
                {
                    UseManualConnect = false;
                    UseComponentsToConnect = false;
                    break;
                }
                case TestConfiguration.ManualConnectionComponents:
                {
                    UseManualConnect = true;
                    UseComponentsToConnect = true;
                    break;
                }
                case TestConfiguration.ManualConnectionDirect:
                {
                    UseManualConnect = true;
                    UseComponentsToConnect = false;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public override bool Initialize(string defaultWorldName)
        {
            if (UseSecureDriver)
                NetworkStreamReceiveSystem.DriverConstructor = new SecureDriverConstructor();

            if (UseManualConnect)
            {
                AutoConnectPort = 0;
                CreateLocalWorld(defaultWorldName);
                return true;
            }
            
            AutoConnectPort = Port;
            return base.Initialize(defaultWorldName);
        }
    }
}