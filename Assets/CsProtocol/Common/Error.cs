using System;
using System.Collections.Generic;
using CsProtocol.Buffer;

namespace CsProtocol
{
    
    public class Error : IProtocol
    {
        public int module;
        public int errorCode;
        public string errorMessage;

        public static Error ValueOf(int errorCode, string errorMessage, int module)
        {
            var packet = new Error();
            packet.errorCode = errorCode;
            packet.errorMessage = errorMessage;
            packet.module = module;
            return packet;
        }


        public short ProtocolId()
        {
            return 101;
        }
    }


    public class ErrorRegistration : IProtocolRegistration
    {
        public short ProtocolId()
        {
            return 101;
        }

        public void Write(ByteBuffer buffer, IProtocol packet)
        {
            if (buffer.WritePacketFlag(packet))
            {
                return;
            }
            Error message = (Error) packet;
            buffer.WriteInt(message.errorCode);
            buffer.WriteString(message.errorMessage);
            buffer.WriteInt(message.module);
        }

        public IProtocol Read(ByteBuffer buffer)
        {
            if (!buffer.ReadBool())
            {
                return null;
            }
            Error packet = new Error();
            int result0 = buffer.ReadInt();
            packet.errorCode = result0;
            string result1 = buffer.ReadString();
            packet.errorMessage = result1;
            int result2 = buffer.ReadInt();
            packet.module = result2;
            return packet;
        }
    }
}
