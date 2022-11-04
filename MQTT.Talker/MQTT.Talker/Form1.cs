using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MQTT.Talker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            brokerTextBox.Text = "172.16.2.76";
            portTextBox.Text = "1883";
            chatGroupTextBox.Text = "Mesa0";

            userTextBox.Text = "Felipe";
        }

        private MQTTConnection conn;

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (conn != null)
                return;

            conn = new MQTTConnection();
            Broker broker = new Broker();
            broker.Server = brokerTextBox.Text;
            broker.Port = int.Parse(portTextBox.Text);

            conn.Connect(broker);

            conn.Subscribe(chatGroupTextBox.Text);

            conn.MessageReceived += Conn_MessageReceived;
        }

        private void Conn_MessageReceived(object sender, MessageInfo e)
        {
            this.Invoke(new Action(() =>
            {
                messagesTextBox.Text += e.Message + "\r\n";
            }));
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            conn.Send(userTextBox.Text + "|" + dataToSentTextBox.Text, chatGroupTextBox.Text);
            dataToSentTextBox.Text = string.Empty;
        }
    }
}
