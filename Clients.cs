using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace GameServer{
    public class TCP{
            public TcpClient socket;
            private int id;
            private NetworkStream _stream;
            private byte[] _recivedBuffer;
            public TCP(int id){
                this.id = id;
            }
            public void Connect(TcpClient _socket){
                this.socket = _socket;
                this.socket.ReceiveBufferSize = Client.dataBufferBytes;
                this._stream = this.socket.GetStream();
                this._recivedBuffer = new Byte[Client.dataBufferBytes];
                this._stream.BeginRead(this._recivedBuffer,0,Client.dataBufferBytes, Res, null);
            }
            private void Res(IAsyncResult _result){
                try{
                    int _byteLength = this._stream.EndRead(_result);
                    if(_byteLength <= 0){
                        return;
                    }
                    byte[] _data = new byte[_byteLength];
                    Array.Copy(_recivedBuffer, _data, _byteLength);
                    this._stream.BeginRead(_recivedBuffer,0,Client.dataBufferBytes,Res,null);

                }catch(Exception _ex){
                    Console.WriteLine(_ex.Message);
                }
            }
        }
    class Client{
        public static int dataBufferBytes = 4000;
        public int id;
        public TCP tcp;
        public Client(int id ){
            this.id = id;
        }
    }
}