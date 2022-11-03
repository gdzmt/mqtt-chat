using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTT.Talker
{
    internal class MQTTConnection
    {

       
        public void Connect(Broker broker, bool setEvents = true)
        {
            

        }

        public void Disconnect()
        {
            
        }

        private MqttClient CreateClient(Broker broker, bool setEvents = true)
        {
            return null;
        }

        public void Send(string message, string destinationName)
        {
            
        }

        public void Subscribe(string sourceName)
        {
            
        }

        public void Unsubscribe(string sourceName)
        {
            
        }

        private void MqttMessageReceived(object sender, MqttMsgPublishEventArgs e)
        {
            

        }

    }
}
